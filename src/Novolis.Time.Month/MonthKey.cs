namespace Novolis.Time.Month;

/// <summary>Calendar year and month number under a frozen <see cref="MonthModel"/>.</summary>
public readonly record struct MonthKey
{
    /// <summary>Initializes a month key.</summary>
    public MonthKey(int year, int month, MonthModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(year);
        if (month is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month), month, "A month number must be between 1 and 12.");
        }

        Year = year;
        Month = month;
        Model = model;
    }

    /// <summary>Gets the calendar year.</summary>
    public int Year { get; }

    /// <summary>Gets the month number within <see cref="Year"/>, from 1 through 12.</summary>
    public int Month { get; }

    /// <summary>Gets the snapshot that produced this key.</summary>
    public MonthModel Model { get; }

    /// <summary>Returns the month key containing a date under <see cref="MonthModel.Gregorian"/>.</summary>
    public static MonthKey FromGregorian(DateOnly date) => MonthModel.Gregorian.GetKey(date);

    /// <summary>Returns the key that is <paramref name="months"/> away, clamping only through civil month addition.</summary>
    public MonthKey AddMonths(int months)
    {
        var shifted = Model.AddMonths(new DateOnly(Year, Month, 1), months);
        return new MonthKey(shifted.Year, shifted.Month, Model);
    }
}
