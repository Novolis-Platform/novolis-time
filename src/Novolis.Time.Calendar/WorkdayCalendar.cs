using System.Collections.Frozen;

namespace Novolis.Time.Calendar;

/// <summary>Immutable week-pattern and holiday calendar.</summary>
public sealed class WorkdayCalendar : IWorkdayCalendar
{
    private readonly FrozenSet<DayOfWeek> workdays;
    private readonly FrozenDictionary<DateOnly, string> publicHolidays;

    /// <summary>Initializes a workday calendar.</summary>
    /// <param name="id">The stable calendar identifier.</param>
    /// <param name="source">The source data metadata.</param>
    /// <param name="workdays">The normally worked weekdays.</param>
    /// <param name="publicHolidays">The public holidays that override the week pattern.</param>
    public WorkdayCalendar(
        string id,
        CalendarSourceMetadata source,
        IEnumerable<DayOfWeek> workdays,
        IEnumerable<CalendarPublicHoliday> publicHolidays)
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
    public CalendarSourceMetadata Source { get; }

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
}
