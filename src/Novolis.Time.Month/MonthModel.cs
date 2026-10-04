namespace Novolis.Time.Month;

/// <summary>
/// Immutable month-numbering rules. Values are snapshots; month math never reads
/// <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.
/// </summary>
public sealed record MonthModel
{
    /// <summary>Gregorian civil months: January is month 1 and lengths follow the Gregorian calendar.</summary>
    public static MonthModel Gregorian { get; } = new("Gregorian");

    /// <summary>Initializes a month-numbering snapshot.</summary>
    public MonthModel(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    /// <summary>Gets the snapshot name.</summary>
    public string Name { get; }

    /// <summary>Returns whether this model is the Gregorian civil calendar.</summary>
    public bool IsGregorian => Name == Gregorian.Name;

    /// <summary>Returns the first local date of the month that contains <paramref name="date"/>.</summary>
    public DateOnly GetMonthStart(DateOnly date) => new(date.Year, date.Month, 1);

    /// <summary>Returns the last local date of the month that contains <paramref name="date"/>.</summary>
    public DateOnly GetMonthEnd(DateOnly date) =>
        new(date.Year, date.Month, GetLength(date.Year, date.Month));

    /// <summary>Returns the month key for a local date.</summary>
    public MonthKey GetKey(DateOnly date) => new(date.Year, date.Month, this);

    /// <summary>Returns the number of local days in a calendar month.</summary>
    public int GetLength(int year, int month)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
        if (month is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month), month, "A month number must be between 1 and 12.");
        }

        return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    /// Adds civil months to a local date.
    /// When the source day does not exist in the destination month, the result is the last day of that month.
    /// </summary>
    public DateOnly AddMonths(DateOnly date, int months) => date.AddMonths(months);

    /// <summary>Returns the signed number of month boundaries from <paramref name="start"/> to <paramref name="end"/>.</summary>
    public int MonthsBetween(DateOnly start, DateOnly end) =>
        ((end.Year - start.Year) * 12) + (end.Month - start.Month);
}
