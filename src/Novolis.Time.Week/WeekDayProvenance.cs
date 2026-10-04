namespace Novolis.Time.Week;

/// <summary>How a week-based calendar produced a day's value.</summary>
public enum WeekDayProvenance
{
    /// <summary>The calendar has no opinion for the date.</summary>
    None = 0,

    /// <summary>The value came from a single weekly pattern.</summary>
    Pattern = 1,

    /// <summary>The value came from a rotating week cycle.</summary>
    Cycle = 2,

    /// <summary>A dated override replaced the pattern or cycle.</summary>
    Override = 3,
}
