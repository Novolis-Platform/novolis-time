using Novolis.Time;

namespace Novolis.Time.Worktime;

/// <summary>Defines the shared local worktime rules for an employment.</summary>
public sealed record WorktimeProfile
{
    /// <summary>Initializes a worktime profile.</summary>
    public WorktimeProfile(
        string id,
        string name,
        ClockInterval workingDayEnvelope,
        ClockInterval coreHours,
        ClockInterval lunch,
        TimeSpan weekHours,
        TimeSpan dayHours,
        PresenceClassification unmarkedSurplusClassification)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (weekHours <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(weekHours), weekHours, "Week hours must be positive.");
        }

        if (dayHours <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(dayHours), dayHours, "Day hours must be positive.");
        }

        if (!workingDayEnvelope.Contains(coreHours) || !workingDayEnvelope.Contains(lunch))
        {
            throw new ArgumentException("Core hours and lunch must be inside the working-day envelope.");
        }

        Id = id;
        Name = name;
        WorkingDayEnvelope = workingDayEnvelope;
        CoreHours = coreHours;
        Lunch = lunch;
        WeekHours = weekHours;
        DayHours = dayHours;
        UnmarkedSurplusClassification = unmarkedSurplusClassification;
    }

    /// <summary>Gets the stable profile identifier.</summary>
    public string Id { get; }

    /// <summary>Gets the user-facing profile name.</summary>
    public string Name { get; }

    /// <summary>Gets the broad allowed working-day interval.</summary>
    public ClockInterval WorkingDayEnvelope { get; }

    /// <summary>Gets the interval expected to be covered, excluding lunch.</summary>
    public ClockInterval CoreHours { get; }

    /// <summary>Gets the usual lunch interval.</summary>
    public ClockInterval Lunch { get; }

    /// <summary>Gets the weekly expected duration.</summary>
    public TimeSpan WeekHours { get; }

    /// <summary>Gets the daily expected duration for a full-time workday.</summary>
    public TimeSpan DayHours { get; }

    /// <summary>Gets the classification applied to surplus that has no explicit mark.</summary>
    public PresenceClassification UnmarkedSurplusClassification { get; }
}
