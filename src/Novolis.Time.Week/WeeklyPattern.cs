using System.Collections.Frozen;

namespace Novolis.Time.Week;

/// <summary>
/// One optional value per weekday. A missing day is silence, not a reset to default.
/// </summary>
public sealed class WeeklyPattern<T>
{
    private readonly FrozenDictionary<DayOfWeek, T> days;

    /// <summary>Initializes a sparse weekday pattern.</summary>
    public WeeklyPattern(IEnumerable<KeyValuePair<DayOfWeek, T>> days)
    {
        ArgumentNullException.ThrowIfNull(days);
        this.days = days.ToFrozenDictionary(pair => pair.Key, pair => pair.Value);
    }

    /// <summary>Gets the weekdays that carry an explicit value.</summary>
    public IReadOnlyCollection<DayOfWeek> AssignedDays => days.Keys;

    /// <summary>Tries to read the value assigned to a weekday.</summary>
    public bool TryGetValue(DayOfWeek day, out T value) => days.TryGetValue(day, out value!);
}
