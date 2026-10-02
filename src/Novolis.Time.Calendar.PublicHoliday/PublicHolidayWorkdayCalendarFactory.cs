using Novolis.Time.Calendar;
using PublicHoliday;

namespace Novolis.Time.Calendar.PublicHoliday;

/// <summary>Builds immutable workday calendars from the offline PublicHoliday data set.</summary>
public static class PublicHolidayWorkdayCalendarFactory
{
    /// <summary>Builds one year of a country calendar.</summary>
    public static WorkdayCalendar Create(
        string id,
        int year,
        string countryCode,
        IEnumerable<DayOfWeek>? workdays = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);

        var provider = PublicHolidayFactory.GetPublicHolidayForCountry(countryCode);
        var packageVersion = typeof(PublicHolidayFactory).Assembly.GetName().Version?.ToString() ?? "unknown";
        var source = new CalendarSourceMetadata(
            "PublicHoliday",
            packageVersion,
            countryCode.ToUpperInvariant(),
            null);
        var holidays = provider
            .PublicHolidaysInformation(year)
            .Where(holiday => holiday.IsPublic)
            .Select(holiday => new CalendarPublicHoliday(
                DateOnly.FromDateTime(holiday.ObservedDate),
                holiday.Name))
            .ToArray();

        return new WorkdayCalendar(
            id,
            source,
            workdays ?? [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday],
            holidays);
    }
}
