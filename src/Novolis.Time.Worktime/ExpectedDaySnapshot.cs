using Novolis.Time;
using Novolis.Time.Workday;

namespace Novolis.Time.Worktime;

/// <summary>Freezes the expected worktime that was in force when an actual record was posted.</summary>
public sealed record ExpectedDaySnapshot(
    DateOnly Date,
    bool IsWorkday,
    ClockInterval? ExpectedInterval,
    ClockInterval Lunch,
    TimeSpan ExpectedDuration,
    string ProfileId,
    string TemplateId,
    string CalendarId,
    WorkdaySourceMetadata CalendarSource);
