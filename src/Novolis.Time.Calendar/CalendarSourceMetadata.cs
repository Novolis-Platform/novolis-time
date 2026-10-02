namespace Novolis.Time.Calendar;

/// <summary>Identifies the calendar data used to derive an expected workday.</summary>
public sealed record CalendarSourceMetadata(
    string Source,
    string Version,
    string CountryCode,
    string? SubdivisionCode);
