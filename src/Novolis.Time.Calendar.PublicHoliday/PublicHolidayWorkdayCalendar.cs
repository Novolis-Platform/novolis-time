using System.Collections.Concurrent;
using Novolis.Time.Calendar;
using PublicHoliday;

namespace Novolis.Time.Calendar.PublicHoliday;

/// <summary>Thread-safe multi-year workday calendar backed by the offline PublicHoliday data set.</summary>
public sealed class PublicHolidayWorkdayCalendar : IWorkdayCalendar
{
    private readonly ConcurrentDictionary<int, WorkdayCalendar> years = new();
    private readonly DayOfWeek[] workdays;
    private readonly string countryCode;

    /// <summary>Initializes a calendar that lazily resolves the relevant calendar year for each date.</summary>
    public PublicHolidayWorkdayCalendar(
        string id,
        string countryCode,
        IEnumerable<DayOfWeek>? workdays = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);

        Id = id;
        this.countryCode = countryCode.ToUpperInvariant();
        this.workdays = (workdays ?? [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday])
            .Distinct()
            .ToArray();
        var packageVersion = typeof(PublicHolidayFactory).Assembly.GetName().Version?.ToString() ?? "unknown";
        Source = new CalendarSourceMetadata(
            "PublicHoliday",
            packageVersion,
            this.countryCode,
            null);
    }

    /// <inheritdoc />
    public string Id { get; }

    /// <inheritdoc />
    public CalendarSourceMetadata Source { get; }

    /// <inheritdoc />
    public bool IsWorkday(DateOnly date) => CalendarFor(date).IsWorkday(date);

    /// <inheritdoc />
    public string? GetNonWorkdayReason(DateOnly date) => CalendarFor(date).GetNonWorkdayReason(date);

    private WorkdayCalendar CalendarFor(DateOnly date) =>
        years.GetOrAdd(
            date.Year,
            year => PublicHolidayWorkdayCalendarFactory.Create(
                $"{Id}.{year}",
                year,
                countryCode,
                workdays));
}
