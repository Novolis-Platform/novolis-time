using System.Globalization;

namespace Novolis.Time.Week;

/// <summary>Inclusive local-date range of one numbered week.</summary>
public readonly record struct WeekRange
{
    /// <summary>Initializes a week range from an already-resolved key.</summary>
    public WeekRange(DateOnly start, DateOnly end, WeekKey key)
    {
        if (end < start)
        {
            throw new ArgumentException("The week end must not precede the week start.", nameof(end));
        }

        if ((end.DayNumber - start.DayNumber) != 6)
        {
            throw new ArgumentException("A week range must cover exactly seven local days.", nameof(end));
        }

        if (start.DayOfWeek != key.Model.FirstDayOfWeek)
        {
            throw new ArgumentException(
                "The week start must match the week model's first day of week.",
                nameof(start));
        }

        Start = start;
        End = end;
        Key = key;
    }

    /// <summary>Gets the first local date of the week.</summary>
    public DateOnly Start { get; }

    /// <summary>Gets the last local date of the week.</summary>
    public DateOnly End { get; }

    /// <summary>Gets the week-numbering key.</summary>
    public WeekKey Key { get; }

    /// <summary>Returns whether a date falls inside this week.</summary>
    public bool Contains(DateOnly date) => date >= Start && date <= End;

    /// <summary>Resolves the range for a week key.</summary>
    public static WeekRange From(WeekKey key)
    {
        ArgumentNullException.ThrowIfNull(key.Model);
        DateOnly start;
        if (key.Model.IsIso)
        {
            start = ISOWeek.ToDateOnly(key.WeekYear, key.WeekNumber, DayOfWeek.Monday);
        }
        else
        {
            start = key.Model.GetWeek1Start(key.WeekYear).AddDays((key.WeekNumber - 1) * 7);
        }

        return new WeekRange(start, start.AddDays(6), key);
    }

    /// <summary>Resolves the range containing a date under the given model.</summary>
    public static WeekRange From(DateOnly date, WeekModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        return From(model.GetKey(date));
    }
}
