using Novolis.Time;
using Novolis.Time.Workday;
using Novolis.Time.Worktime;
using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Unit;

internal static class NorwayWorktimeFixture
{
    internal static readonly DateOnly Thursday = new(2026, 10, 1);

    internal static WorktimeProfile CreateProfile() =>
        new(
            "oslo-office",
            "Oslo office",
            new ClockInterval(new TimeOnly(7, 0), new TimeOnly(17, 0)),
            new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0)),
            new ClockInterval(new TimeOnly(11, 30), new TimeOnly(12, 0)),
            TimeSpan.FromHours(37.5),
            TimeSpan.FromHours(7.5),
            PresenceClassification.Flex);

    internal static EmploymentSettings CreateSettings()
    {
        var profile = CreateProfile();
        var calendar = WorkdayCalendar.FromGeneratedHolidays("no-workdays-2026", "NO", 2026);
        var template = new ExpectedDayTemplate(
            "office-day",
            "Office day",
            Enum.GetValues<DayOfWeek>()
                .Where(day => day is >= DayOfWeek.Monday and <= DayOfWeek.Friday)
                .Select(day => new KeyValuePair<DayOfWeek, ClockInterval>(
                    day,
                    new ClockInterval(new TimeOnly(8, 0), new TimeOnly(16, 0)))));

        return new EmploymentSettings("employee-1", profile, calendar, template, 1m);
    }

    internal static WorktimeLegalPreset CreateDraftPreset(
        TimeSpan? positiveCarryCap = null,
        TimeSpan? negativeCarryFloor = null) =>
        new(
            "draft.local",
            "test",
            "NO",
            "Local test citation",
            TimeSpan.FromHours(9),
            new FlexCarryPolicy(
                "draft.local",
                positiveCarryCap ?? TimeSpan.FromHours(40),
                negativeCarryFloor ?? TimeSpan.FromHours(-10)),
            new ApprovalSchedule(5, 5, 10),
            true,
            "The agreement is a stored caution.",
            LegalReviewState.Draft);

    internal static ActualWorkRecord CreateThursdayRecord(
        IEnumerable<FinancialCompensationMark>? compensationMarks = null,
        bool hasManagerAgreement = false) =>
        new(
            Guid.NewGuid(),
            Thursday,
            new ClockInterval(new TimeOnly(9, 30), new TimeOnly(21, 45)),
            new ClockInterval(new TimeOnly(11, 30), new TimeOnly(12, 0)),
            compensationMarks,
            "Traffic. Stayed for an emergency. Not OT.",
            hasManagerAgreement);
}
