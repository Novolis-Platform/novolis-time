namespace Novolis.Time.Calendar;

/// <summary>Answers whether a local date has expected worktime.</summary>
public interface IWorkdayCalendar
{
    /// <summary>Gets the stable calendar identifier.</summary>
    string Id { get; }

    /// <summary>Gets the source metadata captured with derived workdays.</summary>
    CalendarSourceMetadata Source { get; }

    /// <summary>Returns whether the date is a workday.</summary>
    bool IsWorkday(DateOnly date);

    /// <summary>Returns the reason that the date is not a workday, when available.</summary>
    string? GetNonWorkdayReason(DateOnly date);
}
