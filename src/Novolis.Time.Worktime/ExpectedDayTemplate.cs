using System.Collections.Frozen;
using Novolis.Time;

namespace Novolis.Time.Worktime;

/// <summary>Maps weekdays to reusable expected clock intervals.</summary>
public sealed class ExpectedDayTemplate
{
    private readonly FrozenDictionary<DayOfWeek, ClockInterval> expectedIntervals;

    /// <summary>Initializes a day template.</summary>
    public ExpectedDayTemplate(string id, string name, IEnumerable<KeyValuePair<DayOfWeek, ClockInterval>> expectedIntervals)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(expectedIntervals);

        Id = id;
        Name = name;
        this.expectedIntervals = expectedIntervals.ToFrozenDictionary();
    }

    /// <summary>Gets the stable template identifier.</summary>
    public string Id { get; }

    /// <summary>Gets the user-facing template name.</summary>
    public string Name { get; }

    /// <summary>Returns the expected interval for a weekday, if the template has one.</summary>
    public ClockInterval? GetExpectedInterval(DateOnly date) =>
        expectedIntervals.TryGetValue(date.DayOfWeek, out var interval) ? interval : null;
}
