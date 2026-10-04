namespace Novolis.Time.Month;

/// <summary>How a month-based calendar produced a day's value.</summary>
public enum MonthDayProvenance
{
    /// <summary>The calendar has no opinion for the date.</summary>
    None = 0,

    /// <summary>The value came from a single monthly pattern.</summary>
    Pattern = 1,

    /// <summary>The value came from a rotating month cycle.</summary>
    Cycle = 2,

    /// <summary>A dated override replaced the pattern or cycle.</summary>
    Override = 3,
}
