using Novolis.Time;
using Novolis.Time.Worktime;
using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Unit;

public sealed class NorwayThursdayFeatureTests
{
    [Test]
    public async Task Posts_the_actual_day_and_keeps_core_and_envelope_cautions()
    {
        var settings = NorwayWorktimeFixture.CreateSettings();
        var profile = settings.Profile;
        var expected = WorktimeCalculator.CreateExpectedSnapshot(NorwayWorktimeFixture.Thursday, settings);
        var record = NorwayWorktimeFixture.CreateThursdayRecord();

        var balance = WorktimeCalculator.Calculate(record, expected);
        var firings = WorktimeLegalEvaluator.Evaluate(record, balance, profile, NorwayWorktimeFixture.CreateDraftPreset());

        await Assert.That(expected.IsWorkday).IsTrue();
        await Assert.That(expected.ExpectedDuration).IsEqualTo(TimeSpan.FromHours(7.5));
        await Assert.That(balance.Actual).IsEqualTo(TimeSpan.FromHours(11.75));
        await Assert.That(balance.FlexDelta).IsEqualTo(TimeSpan.FromHours(4.25));
        await Assert.That(balance.FinanciallyCompensated).IsEqualTo(TimeSpan.Zero);
        await Assert.That(firings.Select(firing => firing.RuleId))
            .Contains("core.coverage")
            .And.Contains("working-day.envelope")
            .And.Contains("ordinary.daily-limit");
    }

    [Test]
    public async Task Moving_late_work_to_financial_compensation_changes_flex_but_not_presence()
    {
        var settings = NorwayWorktimeFixture.CreateSettings();
        var expected = WorktimeCalculator.CreateExpectedSnapshot(NorwayWorktimeFixture.Thursday, settings);
        var lateEmergency = new FinancialCompensationMark(
            new ClockInterval(new TimeOnly(17, 0), new TimeOnly(21, 45)),
            "Manager-approved emergency compensation.");
        var record = NorwayWorktimeFixture.CreateThursdayRecord([lateEmergency], hasManagerAgreement: true);

        var balance = WorktimeCalculator.Calculate(record, expected);

        await Assert.That(balance.Actual).IsEqualTo(TimeSpan.FromHours(11.75));
        await Assert.That(balance.FinanciallyCompensated).IsEqualTo(TimeSpan.FromHours(4.75));
        await Assert.That(balance.FlexDelta).IsEqualTo(TimeSpan.FromHours(-0.5));
    }

    [Test]
    public async Task Missing_manager_agreement_is_a_stored_caution_not_a_rejection()
    {
        var settings = NorwayWorktimeFixture.CreateSettings();
        var expected = WorktimeCalculator.CreateExpectedSnapshot(NorwayWorktimeFixture.Thursday, settings);
        var record = NorwayWorktimeFixture.CreateThursdayRecord(
            [
                new FinancialCompensationMark(
                    new ClockInterval(new TimeOnly(17, 0), new TimeOnly(21, 45)),
                    "Emergency"),
            ]);

        var balance = WorktimeCalculator.Calculate(record, expected);
        var firings = WorktimeLegalEvaluator.Evaluate(record, balance, settings.Profile, NorwayWorktimeFixture.CreateDraftPreset());

        await Assert.That(balance.Actual).IsEqualTo(TimeSpan.FromHours(11.75));
        await Assert.That(firings.Select(firing => firing.RuleId))
            .Contains("financial-compensation.manager-agreement");
    }

    [Test]
    public async Task Captures_the_expected_day_before_template_changes()
    {
        var settings = NorwayWorktimeFixture.CreateSettings();
        var firstSnapshot = WorktimeCalculator.CreateExpectedSnapshot(NorwayWorktimeFixture.Thursday, settings);
        var changedTemplate = new ExpectedDayTemplate(
            "office-day-v2",
            "Office day changed",
            [
                new KeyValuePair<DayOfWeek, ClockInterval>(
                    DayOfWeek.Thursday,
                    new ClockInterval(new TimeOnly(9, 0), new TimeOnly(17, 0))),
            ]);
        var changedSettings = new EmploymentSettings(
            settings.EmploymentId,
            settings.Profile,
            settings.Calendar,
            changedTemplate,
            settings.WorkFraction);

        var laterSnapshot = WorktimeCalculator.CreateExpectedSnapshot(NorwayWorktimeFixture.Thursday, changedSettings);

        await Assert.That(firstSnapshot.ExpectedDuration).IsEqualTo(TimeSpan.FromHours(7.5));
        await Assert.That(firstSnapshot.ExpectedInterval).IsEqualTo(new ClockInterval(new TimeOnly(8, 0), new TimeOnly(16, 0)));
        await Assert.That(laterSnapshot.ExpectedInterval).IsEqualTo(new ClockInterval(new TimeOnly(9, 0), new TimeOnly(17, 0)));
    }
}
