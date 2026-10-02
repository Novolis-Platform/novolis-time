using Novolis.Time.Calendar;
using Novolis.Time.Calendar.PublicHoliday;
using Novolis.Time.Worktime;
using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Worktime.FeatureTests;

public sealed class CalendarAndSettlementFeatureTests
{
    [Test]
    public async Task Uses_norwegian_holidays_for_zero_expected_worktime()
    {
        var settings = NorwayWorktimeFixture.CreateSettings();
        var newYearsDay = new DateOnly(2026, 1, 1);

        var expected = WorktimeCalculator.CreateExpectedSnapshot(newYearsDay, settings);

        await Assert.That(expected.IsWorkday).IsFalse();
        await Assert.That(expected.ExpectedDuration).IsEqualTo(TimeSpan.Zero);
        await Assert.That(expected.CalendarSource.CountryCode).IsEqualTo("NO");
    }

    [Test]
    public async Task Counts_five_business_days_after_october_2026_closes()
    {
        var calendar = PublicHolidayWorkdayCalendarFactory.Create("no-workdays-2026", 2026, "NO");
        var deadline = BusinessDayCalculator.AddBusinessDays(new DateOnly(2026, 10, 31), 5, calendar);

        await Assert.That(deadline).IsEqualTo(new DateOnly(2026, 11, 6));
    }

    [Test]
    public async Task Normalizes_unused_positive_flex_without_financial_compensation()
    {
        var normalization = FlexNormalization.Create(
            WorktimeLegalPresets.NorwayPrivate.FlexCarryPolicy,
            TimeSpan.FromHours(55));

        await Assert.That(normalization.NormalizedUnusedFlex).IsEqualTo(TimeSpan.FromHours(15));
        await Assert.That(normalization.OpeningSaldo).IsEqualTo(TimeSpan.FromHours(40));
        await Assert.That(normalization.FinanciallyCompensated).IsEqualTo(TimeSpan.Zero);
    }

    [Test]
    public async Task Keeps_state_handbook_normalization_separate_from_payment()
    {
        var normalization = FlexNormalization.Create(
            WorktimeLegalPresets.NorwayState.FlexCarryPolicy,
            TimeSpan.FromHours(55));

        await Assert.That(normalization.NormalizedUnusedFlex).IsEqualTo(TimeSpan.FromHours(5));
        await Assert.That(normalization.FinanciallyCompensated).IsEqualTo(TimeSpan.Zero);
    }
}
