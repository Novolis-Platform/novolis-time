using System.Collections.Frozen;

namespace Novolis.Time.Month;

/// <summary>
/// One optional value per day-of-month. A missing day is silence, not a reset to default.
/// A day number that does not exist in a shorter month is also silence.
/// </summary>
public sealed class MonthlyPattern<T>
{
    private readonly FrozenDictionary<int, T> days;

    /// <summary>Initializes a sparse day-of-month pattern.</summary>
    public MonthlyPattern(IEnumerable<KeyValuePair<int, T>> days)
    {
        ArgumentNullException.ThrowIfNull(days);
        var assigned = new Dictionary<int, T>();
        foreach (var pair in days)
        {
            if (pair.Key is < 1 or > 31)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(days),
                    pair.Key,
                    "A day of month must be between 1 and 31.");
            }

            assigned[pair.Key] = pair.Value;
        }

        this.days = assigned.ToFrozenDictionary();
    }

    /// <summary>Gets the day numbers that carry an explicit value.</summary>
    public IReadOnlyCollection<int> AssignedDays => days.Keys;

    /// <summary>Tries to read the value assigned to a day of the month.</summary>
    public bool TryGetValue(int dayOfMonth, out T value) => days.TryGetValue(dayOfMonth, out value!);
}
