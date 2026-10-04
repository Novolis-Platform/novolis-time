using System.Globalization;

namespace Novolis.Time.Week;

/// <summary>
/// Immutable week-numbering rules. Values are snapshots; never read
/// <see cref="CultureInfo.CurrentCulture"/> at resolution time.
/// </summary>
public sealed record WeekModel
{
    /// <summary>ISO 8601 weeks: Monday start, four days required in the first week.</summary>
    public static WeekModel Iso { get; } = new(DayOfWeek.Monday, 4);

    /// <summary>Initializes a week-numbering snapshot.</summary>
    public WeekModel(DayOfWeek firstDayOfWeek, int minimumDaysInFirstWeek)
    {
        if (minimumDaysInFirstWeek is < 1 or > 7)
        {
            throw new ArgumentOutOfRangeException(
                nameof(minimumDaysInFirstWeek),
                minimumDaysInFirstWeek,
                "The first week must contain between one and seven days of the week-numbering year.");
        }

        FirstDayOfWeek = firstDayOfWeek;
        MinimumDaysInFirstWeek = minimumDaysInFirstWeek;
    }

    /// <summary>Gets the first local weekday of a week.</summary>
    public DayOfWeek FirstDayOfWeek { get; }

    /// <summary>Gets how many days of the new year a week must contain to be week 1.</summary>
    public int MinimumDaysInFirstWeek { get; }

    /// <summary>Creates a snapshot from explicit values rather than a live culture.</summary>
    public static WeekModel FromSnapshot(DayOfWeek firstDayOfWeek, int minimumDaysInFirstWeek) =>
        new(firstDayOfWeek, minimumDaysInFirstWeek);

    /// <summary>Returns whether this model matches ISO 8601 week numbering.</summary>
    public bool IsIso =>
        FirstDayOfWeek == DayOfWeek.Monday && MinimumDaysInFirstWeek == 4;

    /// <summary>Returns the start of the week that contains <paramref name="date"/>.</summary>
    public DateOnly GetWeekStart(DateOnly date)
    {
        var offset = ((int)date.DayOfWeek - (int)FirstDayOfWeek + 7) % 7;
        return date.AddDays(-offset);
    }

    /// <summary>Returns the week-numbering key for a local date.</summary>
    public WeekKey GetKey(DateOnly date)
    {
        if (IsIso)
        {
            return new WeekKey(
                ISOWeek.GetYear(date),
                ISOWeek.GetWeekOfYear(date),
                this);
        }

        var weekStart = GetWeekStart(date);
        var weekYear = date.Year;
        var week1Start = GetWeek1Start(weekYear);
        if (weekStart < week1Start)
        {
            weekYear--;
            week1Start = GetWeek1Start(weekYear);
        }
        else
        {
            var nextWeek1Start = GetWeek1Start(weekYear + 1);
            if (weekStart >= nextWeek1Start)
            {
                weekYear++;
                week1Start = nextWeek1Start;
            }
        }

        var weekNumber = ((weekStart.DayNumber - week1Start.DayNumber) / 7) + 1;
        return new WeekKey(weekYear, weekNumber, this);
    }

    /// <summary>Returns the first day of week 1 for a week-numbering year.</summary>
    public DateOnly GetWeek1Start(int weekYear)
    {
        if (IsIso)
        {
            return ISOWeek.ToDateOnly(weekYear, 1, DayOfWeek.Monday);
        }

        var januaryFirst = new DateOnly(weekYear, 1, 1);
        var weekStart = GetWeekStart(januaryFirst);
        var daysInNewYear = 7 - (januaryFirst.DayNumber - weekStart.DayNumber);
        return daysInNewYear >= MinimumDaysInFirstWeek
            ? weekStart
            : weekStart.AddDays(7);
    }
}
