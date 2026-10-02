using Novolis.Time;
using Novolis.Time.Calendar;

namespace Novolis.Time.Worktime;

/// <summary>Connects one employment to an immutable worktime profile, calendar, and template.</summary>
public sealed record EmploymentSettings
{
    /// <summary>Initializes employment worktime settings.</summary>
    public EmploymentSettings(
        string employmentId,
        WorktimeProfile profile,
        IWorkdayCalendar calendar,
        ExpectedDayTemplate template,
        decimal workFraction,
        ClockInterval? expectedIntervalOverride = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(employmentId);
        ArgumentNullException.ThrowIfNull(profile);
        ArgumentNullException.ThrowIfNull(calendar);
        ArgumentNullException.ThrowIfNull(template);

        if (workFraction is <= 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(workFraction), workFraction, "Work fraction must be greater than zero and at most one.");
        }

        EmploymentId = employmentId;
        Profile = profile;
        Calendar = calendar;
        Template = template;
        WorkFraction = workFraction;
        ExpectedIntervalOverride = expectedIntervalOverride;
    }

    /// <summary>Gets the stable employment identifier.</summary>
    public string EmploymentId { get; }

    /// <summary>Gets the profile used to classify the employment.</summary>
    public WorktimeProfile Profile { get; }

    /// <summary>Gets the workday calendar.</summary>
    public IWorkdayCalendar Calendar { get; }

    /// <summary>Gets the expected-day template.</summary>
    public ExpectedDayTemplate Template { get; }

    /// <summary>Gets the employment fraction.</summary>
    public decimal WorkFraction { get; }

    /// <summary>Gets the optional person-specific expected interval.</summary>
    public ClockInterval? ExpectedIntervalOverride { get; }
}
