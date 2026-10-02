namespace Novolis.Time.Calendar;

/// <summary>Represents a public holiday that removes expected worktime.</summary>
public sealed record CalendarPublicHoliday(DateOnly Date, string Name);
