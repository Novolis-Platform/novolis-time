using System.Collections.Immutable;

namespace Novolis.Time.Week;

/// <summary>Rotating sequence of weekly patterns anchored on a week start.</summary>
public sealed class WeekCycle<T>
{
    /// <summary>Initializes a rotating cycle.</summary>
    public WeekCycle(
        WeekModel model,
        DateOnly anchorWeekStart,
        IEnumerable<WeeklyPattern<T>> patterns)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(patterns);
        if (anchorWeekStart.DayOfWeek != model.FirstDayOfWeek)
        {
            throw new ArgumentException(
                "The cycle anchor must fall on the model's first day of week.",
                nameof(anchorWeekStart));
        }

        var ordered = patterns.ToImmutableArray();
        if (ordered.IsDefaultOrEmpty)
        {
            throw new ArgumentException("A week cycle requires at least one pattern.", nameof(patterns));
        }

        Model = model;
        AnchorWeekStart = anchorWeekStart;
        Patterns = ordered;
    }

    /// <summary>Gets the week model used to count weeks from the anchor.</summary>
    public WeekModel Model { get; }

    /// <summary>Gets the first day of week 0 in the cycle.</summary>
    public DateOnly AnchorWeekStart { get; }

    /// <summary>Gets the rotating patterns in cycle order.</summary>
    public ImmutableArray<WeeklyPattern<T>> Patterns { get; }

    /// <summary>Selects the pattern for the week that contains <paramref name="date"/>.</summary>
    public WeeklyPattern<T> PatternFor(DateOnly date)
    {
        var weekStart = Model.GetWeekStart(date);
        var offsetWeeks = (weekStart.DayNumber - AnchorWeekStart.DayNumber) / 7;
        var index = ((offsetWeeks % Patterns.Length) + Patterns.Length) % Patterns.Length;
        return Patterns[index];
    }
}
