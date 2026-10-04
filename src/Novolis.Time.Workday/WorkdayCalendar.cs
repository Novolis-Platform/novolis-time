using System.Collections.Frozen;

namespace Novolis.Time.Workday;

/// <summary>Immutable week-pattern and holiday workday calendar.</summary>
public sealed class WorkdayCalendar : IWorkdayCalendar
{
    private readonly FrozenSet<DayOfWeek> workdays;
    private readonly FrozenDictionary<DateOnly, string> publicHolidays;

    /// <summary>Initializes a workday calendar.</summary>
    public WorkdayCalendar(
        string id,
        WorkdaySourceMetadata source,
        IEnumerable<DayOfWeek> workdays,
        IEnumerable<PublicHolidayFact> publicHolidays)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(workdays);
        ArgumentNullException.ThrowIfNull(publicHolidays);

        Id = id;
        Source = source;
        this.workdays = workdays.ToFrozenSet();
        this.publicHolidays = publicHolidays
            .GroupBy(holiday => holiday.Date)
            .ToFrozenDictionary(group => group.Key, group => group.First().Name);
    }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public WorkdaySourceMetadata Source { get; }

    /// <inheritdoc />
    public bool IsWorkday(DateOnly date) =>
        workdays.Contains(date.DayOfWeek) && !publicHolidays.ContainsKey(date);

    /// <inheritdoc />
    public string? GetNonWorkdayReason(DateOnly date)
    {
        if (publicHolidays.TryGetValue(date, out var name))
        {
            return name;
        }

        return workdays.Contains(date.DayOfWeek) ? null : "Weekend";
    }

    /// <summary>Builds a calendar from frozen generated holidays for one year.</summary>
    public static WorkdayCalendar FromGeneratedHolidays(
        string id,
        string countryCode,
        int year,
        IEnumerable<DayOfWeek>? workdays = null,
        IEnumerable<CalendarOverride>? configuration = null) =>
        FromGeneratedHolidays(id, countryCode, [year], workdays, configuration);

    /// <summary>Builds a calendar from frozen generated holidays for selected years.</summary>
    /// <remarks>
    /// Generated facts are the baseline. <paramref name="configuration"/> overrides individual dates
    /// and leaves every other baseline rule in place.
    /// </remarks>
    public static WorkdayCalendar FromGeneratedHolidays(
        string id,
        string countryCode,
        IEnumerable<int> years,
        IEnumerable<DayOfWeek>? workdays = null,
        IEnumerable<CalendarOverride>? configuration = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        ArgumentNullException.ThrowIfNull(years);

        var selectedYears = years.Distinct().OrderBy(year => year).ToArray();
        if (selectedYears.Length == 0)
        {
            throw new ArgumentException("At least one generated year is required.", nameof(years));
        }

        var holidays = selectedYears
            .SelectMany(year => GeneratedHolidayCatalog.GetHolidays(countryCode, year))
            .ToArray();
        var calendar = new Calendar(id, countryCode, holidays, configuration);
        var catalog = GeneratedHolidayCatalog.Current;
        return new WorkdayCalendar(
            id,
            new WorkdaySourceMetadata(
                catalog.SourcePackage,
                catalog.SourcePackageVersion,
                countryCode.ToUpperInvariant(),
                null),
            workdays ?? DefaultWorkdays,
            calendar.EffectiveHolidays());
    }

    /// <summary>Builds a calendar from every generated year for a country.</summary>
    public static WorkdayCalendar FromAllGeneratedHolidays(
        string id,
        string countryCode,
        IEnumerable<DayOfWeek>? workdays = null) =>
        FromGeneratedHolidays(id, countryCode, GeneratedHolidayCatalog.GetYears(countryCode), workdays);

    /// <summary>Weekdays treated as ordinary workdays when none are supplied.</summary>
    public static IReadOnlyList<DayOfWeek> DefaultWorkdays { get; } =
    [
        DayOfWeek.Monday,
        DayOfWeek.Tuesday,
        DayOfWeek.Wednesday,
        DayOfWeek.Thursday,
        DayOfWeek.Friday,
    ];
}
