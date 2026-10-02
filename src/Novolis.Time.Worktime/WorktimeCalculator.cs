using Novolis.Time;

namespace Novolis.Time.Worktime;

/// <summary>Pure calculations for expected and actual worktime.</summary>
public static class WorktimeCalculator
{
    /// <summary>Captures the expected worktime for a date from current employment settings.</summary>
    public static ExpectedDaySnapshot CreateExpectedSnapshot(DateOnly date, EmploymentSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var isWorkday = settings.Calendar.IsWorkday(date);
        var interval = isWorkday
            ? settings.ExpectedIntervalOverride ?? settings.Template.GetExpectedInterval(date)
            : null;
        var expected = interval is { } expectedInterval
            ? Scale(expectedInterval.Duration - expectedInterval.OverlapDuration(settings.Profile.Lunch), settings.WorkFraction)
            : TimeSpan.Zero;

        return new ExpectedDaySnapshot(
            date,
            isWorkday && interval is not null,
            interval,
            settings.Profile.Lunch,
            expected,
            settings.Profile.Id,
            settings.Template.Id,
            settings.Calendar.Id,
            settings.Calendar.Source);
    }

    /// <summary>Calculates the immutable balance for an actual record and frozen expected snapshot.</summary>
    public static DayBalance Calculate(ActualWorkRecord record, ExpectedDaySnapshot expected)
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(expected);

        if (record.Date != expected.Date)
        {
            throw new ArgumentException("The record date must match the expected snapshot date.", nameof(expected));
        }

        var actual = WorkedDuration(record.Presence, record.TakenBreak);
        var financiallyCompensated = record.FinancialCompensationMarks
            .Aggregate(TimeSpan.Zero, (total, mark) => total + WorkedDuration(mark.Interval, record.TakenBreak));
        var flexDelta = actual - expected.ExpectedDuration - financiallyCompensated;

        return new DayBalance(
            expected.ExpectedDuration,
            actual,
            flexDelta,
            financiallyCompensated);
    }

    /// <summary>Gets missing core coverage after accounting for actual breaks.</summary>
    public static TimeSpan GetMissingCoreCoverage(ActualWorkRecord record, WorktimeProfile profile)
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(profile);

        var coreExpected = profile.CoreHours.Duration - profile.CoreHours.OverlapDuration(profile.Lunch);
        var covered = record.Presence.OverlapDuration(profile.CoreHours) -
            (record.TakenBreak?.OverlapDuration(profile.CoreHours) ?? TimeSpan.Zero);
        return coreExpected > covered ? coreExpected - covered : TimeSpan.Zero;
    }

    /// <summary>Gets actual worked time that falls outside the profile working-day envelope.</summary>
    public static TimeSpan GetWorkedOutsideEnvelope(ActualWorkRecord record, WorktimeProfile profile)
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(profile);

        var insideEnvelope = record.Presence.OverlapDuration(profile.WorkingDayEnvelope) -
            (record.TakenBreak?.OverlapDuration(profile.WorkingDayEnvelope) ?? TimeSpan.Zero);
        var actual = WorkedDuration(record.Presence, record.TakenBreak);
        return actual - insideEnvelope;
    }

    private static TimeSpan WorkedDuration(ClockInterval interval, ClockInterval? takenBreak) =>
        interval.Duration - (takenBreak?.OverlapDuration(interval) ?? TimeSpan.Zero);

    private static TimeSpan Scale(TimeSpan duration, decimal fraction) =>
        TimeSpan.FromTicks(decimal.ToInt64(decimal.Round(duration.Ticks * fraction, 0, MidpointRounding.AwayFromZero)));
}
