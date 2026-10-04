using System.Collections.Immutable;

namespace Novolis.Time.Month;

/// <summary>Rotating sequence of monthly patterns anchored on a month start.</summary>
public sealed class MonthCycle<T>
{
    /// <summary>Initializes a rotating cycle.</summary>
    public MonthCycle(
        MonthModel model,
        DateOnly anchorMonthStart,
        IEnumerable<MonthlyPattern<T>> patterns)
    {
        ArgumentNullException.ThrowIfNull(model);
        ArgumentNullException.ThrowIfNull(patterns);
        if (anchorMonthStart.Day != 1)
        {
            throw new ArgumentException(
                "The cycle anchor must be the first day of a month.",
                nameof(anchorMonthStart));
        }

        var ordered = patterns.ToImmutableArray();
        if (ordered.IsDefaultOrEmpty)
        {
            throw new ArgumentException("A month cycle requires at least one pattern.", nameof(patterns));
        }

        Model = model;
        AnchorMonthStart = anchorMonthStart;
        Patterns = ordered;
    }

    /// <summary>Gets the month model used to count months from the anchor.</summary>
    public MonthModel Model { get; }

    /// <summary>Gets the first day of month 0 in the cycle.</summary>
    public DateOnly AnchorMonthStart { get; }

    /// <summary>Gets the rotating patterns in cycle order.</summary>
    public ImmutableArray<MonthlyPattern<T>> Patterns { get; }

    /// <summary>Selects the pattern for the month that contains <paramref name="date"/>.</summary>
    public MonthlyPattern<T> PatternFor(DateOnly date)
    {
        var monthStart = Model.GetMonthStart(date);
        var offsetMonths = Model.MonthsBetween(AnchorMonthStart, monthStart);
        var index = ((offsetMonths % Patterns.Length) + Patterns.Length) % Patterns.Length;
        return Patterns[index];
    }
}
