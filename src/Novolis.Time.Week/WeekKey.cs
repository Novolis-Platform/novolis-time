namespace Novolis.Time.Week;

/// <summary>Week-numbering year and week number under a frozen <see cref="WeekModel"/>.</summary>
public readonly record struct WeekKey
{
    /// <summary>Initializes a week key.</summary>
    public WeekKey(int weekYear, int weekNumber, WeekModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(weekYear);
        if (weekNumber is < 1 or > 53)
        {
            throw new ArgumentOutOfRangeException(
                nameof(weekNumber),
                weekNumber,
                "A week number must be between 1 and 53.");
        }

        WeekYear = weekYear;
        WeekNumber = weekNumber;
        Model = model;
    }

    /// <summary>Gets the week-numbering year.</summary>
    public int WeekYear { get; }

    /// <summary>Gets the week number within <see cref="WeekYear"/>.</summary>
    public int WeekNumber { get; }

    /// <summary>Gets the snapshot that produced this key.</summary>
    public WeekModel Model { get; }

    /// <summary>Returns the week key containing a date under <see cref="WeekModel.Iso"/>.</summary>
    public static WeekKey FromIso(DateOnly date) => WeekModel.Iso.GetKey(date);
}
