using System.Collections.Frozen;

namespace Novolis.Time.Month;

/// <summary>
/// Month-based schedule: one repeating pattern or rotating cycle, optional dated
/// overrides, and an effective local-date range. Silence means no opinion.
/// </summary>
public sealed class MonthBasedCalendar<T>
{
    private readonly FrozenDictionary<DateOnly, T> overrides;

    /// <summary>Initializes a month-based calendar.</summary>
    public MonthBasedCalendar(
        MonthModel model,
        MonthlyPattern<T>? pattern = null,
        MonthCycle<T>? cycle = null,
        DateOnly? effectiveFrom = null,
        DateOnly? effectiveTo = null,
        IEnumerable<KeyValuePair<DateOnly, T>>? overrides = null)
    {
        ArgumentNullException.ThrowIfNull(model);
        if (pattern is null == cycle is null)
        {
            throw new ArgumentException("Supply exactly one of a monthly pattern or a month cycle.");
        }

        if (cycle is not null && cycle.Model != model)
        {
            throw new ArgumentException(
                "The cycle must use the same month model as the calendar.",
                nameof(cycle));
        }

        if (effectiveTo < effectiveFrom)
        {
            throw new ArgumentException(
                "The effective end date must not precede the effective start date.",
                nameof(effectiveTo));
        }

        Model = model;
        Pattern = pattern;
        Cycle = cycle;
        EffectiveFrom = effectiveFrom;
        EffectiveTo = effectiveTo;
        this.overrides = (overrides ?? []).ToFrozenDictionary(pair => pair.Key, pair => pair.Value);
    }

    /// <summary>Gets the month-numbering snapshot.</summary>
    public MonthModel Model { get; }

    /// <summary>Gets the single repeating pattern, when this calendar is not a cycle.</summary>
    public MonthlyPattern<T>? Pattern { get; }

    /// <summary>Gets the rotating cycle, when this calendar is not a single pattern.</summary>
    public MonthCycle<T>? Cycle { get; }

    /// <summary>Gets the inclusive effective start, when constrained.</summary>
    public DateOnly? EffectiveFrom { get; }

    /// <summary>Gets the inclusive effective end, when constrained.</summary>
    public DateOnly? EffectiveTo { get; }

    /// <summary>Resolves the calendar's opinion for a local date.</summary>
    public MonthDayResolution<T> Resolve(DateOnly date)
    {
        if ((EffectiveFrom.HasValue && date < EffectiveFrom.Value) ||
            (EffectiveTo.HasValue && date > EffectiveTo.Value))
        {
            return MonthDayResolution<T>.None;
        }

        if (overrides.TryGetValue(date, out var overrideValue))
        {
            return new MonthDayResolution<T>(overrideValue, MonthDayProvenance.Override);
        }

        if (Cycle is not null)
        {
            return Cycle.PatternFor(date).TryGetValue(date.Day, out var cycleValue)
                ? new MonthDayResolution<T>(cycleValue, MonthDayProvenance.Cycle)
                : MonthDayResolution<T>.None;
        }

        return Pattern!.TryGetValue(date.Day, out var patternValue)
            ? new MonthDayResolution<T>(patternValue, MonthDayProvenance.Pattern)
            : MonthDayResolution<T>.None;
    }
}
