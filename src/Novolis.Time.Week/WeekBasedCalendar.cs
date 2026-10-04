using System.Collections.Frozen;

namespace Novolis.Time.Week;

/// <summary>
/// Week-based schedule: one repeating pattern or rotating cycle, optional dated
/// overrides, and an effective local-date range. Silence means no opinion.
/// </summary>
public sealed class WeekBasedCalendar<T>
{
    private readonly FrozenDictionary<DateOnly, T> overrides;

    /// <summary>Initializes a week-based calendar.</summary>
    public WeekBasedCalendar(
        WeekModel model,
        WeeklyPattern<T>? pattern = null,
        WeekCycle<T>? cycle = null,
        DateOnly? effectiveFrom = null,
        DateOnly? effectiveTo = null,
        IEnumerable<KeyValuePair<DateOnly, T>>? overrides = null)
    {
        ArgumentNullException.ThrowIfNull(model);
        if (pattern is null == cycle is null)
        {
            throw new ArgumentException("Supply exactly one of a weekly pattern or a week cycle.");
        }

        if (cycle is not null && !ReferenceEquals(cycle.Model, model) &&
            (cycle.Model.FirstDayOfWeek != model.FirstDayOfWeek ||
             cycle.Model.MinimumDaysInFirstWeek != model.MinimumDaysInFirstWeek))
        {
            throw new ArgumentException(
                "The cycle must use the same week model as the calendar.",
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

    /// <summary>Gets the week-numbering snapshot.</summary>
    public WeekModel Model { get; }

    /// <summary>Gets the single repeating pattern, when this calendar is not a cycle.</summary>
    public WeeklyPattern<T>? Pattern { get; }

    /// <summary>Gets the rotating cycle, when this calendar is not a single pattern.</summary>
    public WeekCycle<T>? Cycle { get; }

    /// <summary>Gets the inclusive effective start, when constrained.</summary>
    public DateOnly? EffectiveFrom { get; }

    /// <summary>Gets the inclusive effective end, when constrained.</summary>
    public DateOnly? EffectiveTo { get; }

    /// <summary>Resolves the calendar's opinion for a local date.</summary>
    public WeekDayResolution<T> Resolve(DateOnly date)
    {
        if ((EffectiveFrom.HasValue && date < EffectiveFrom.Value) ||
            (EffectiveTo.HasValue && date > EffectiveTo.Value))
        {
            return WeekDayResolution<T>.None;
        }

        if (overrides.TryGetValue(date, out var overrideValue))
        {
            return new WeekDayResolution<T>(overrideValue, WeekDayProvenance.Override);
        }

        if (Cycle is not null)
        {
            return Cycle.PatternFor(date).TryGetValue(date.DayOfWeek, out var cycleValue)
                ? new WeekDayResolution<T>(cycleValue, WeekDayProvenance.Cycle)
                : WeekDayResolution<T>.None;
        }

        return Pattern!.TryGetValue(date.DayOfWeek, out var patternValue)
            ? new WeekDayResolution<T>(patternValue, WeekDayProvenance.Pattern)
            : WeekDayResolution<T>.None;
    }
}
