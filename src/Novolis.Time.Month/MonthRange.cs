namespace Novolis.Time.Month;

/// <summary>Inclusive local-date range of one numbered month.</summary>
public readonly record struct MonthRange
{
    /// <summary>Initializes a month range from an already-resolved key.</summary>
    public MonthRange(DateOnly start, DateOnly end, MonthKey key)
    {
        if (end < start)
        {
            throw new ArgumentException("The month end must not precede the month start.", nameof(end));
        }

        if (start.Year != key.Year || start.Month != key.Month || start.Day != 1)
        {
            throw new ArgumentException(
                "The month start must be the first day of the month key.",
                nameof(start));
        }

        var length = key.Model.GetLength(key.Year, key.Month);
        if (end.Year != key.Year || end.Month != key.Month || end.Day != length)
        {
            throw new ArgumentException(
                "The month end must be the last day of the month key.",
                nameof(end));
        }

        Start = start;
        End = end;
        Key = key;
    }

    /// <summary>Gets the first local date of the month.</summary>
    public DateOnly Start { get; }

    /// <summary>Gets the last local date of the month.</summary>
    public DateOnly End { get; }

    /// <summary>Gets the month key.</summary>
    public MonthKey Key { get; }

    /// <summary>Gets the number of local days in the month.</summary>
    public int Length => End.DayNumber - Start.DayNumber + 1;

    /// <summary>Returns whether a date falls inside this month.</summary>
    public bool Contains(DateOnly date) => date >= Start && date <= End;

    /// <summary>Resolves the range for a month key.</summary>
    public static MonthRange From(MonthKey key)
    {
        ArgumentNullException.ThrowIfNull(key.Model);
        var start = new DateOnly(key.Year, key.Month, 1);
        var end = new DateOnly(key.Year, key.Month, key.Model.GetLength(key.Year, key.Month));
        return new MonthRange(start, end, key);
    }

    /// <summary>Resolves the range containing a date under the given model.</summary>
    public static MonthRange From(DateOnly date, MonthModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return From(model.GetKey(date));
    }
}
