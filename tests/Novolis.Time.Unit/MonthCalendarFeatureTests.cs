using Novolis.Time.Month;

namespace Novolis.Time.Unit;

public sealed class MonthCalendarFeatureTests
{
    [Test]
    public async Task Gregorian_month_range_follows_the_civil_length()
    {
        var october = MonthRange.From(new DateOnly(2026, 10, 4), MonthModel.Gregorian);
        var february = MonthRange.From(MonthKey.FromGregorian(new DateOnly(2026, 2, 1)));
        var leapFebruary = MonthRange.From(MonthKey.FromGregorian(new DateOnly(2028, 2, 11)));

        await Assert.That(october.Start).IsEqualTo(new DateOnly(2026, 10, 1));
        await Assert.That(october.End).IsEqualTo(new DateOnly(2026, 10, 31));
        await Assert.That(october.Length).IsEqualTo(31);
        await Assert.That(october.Contains(new DateOnly(2026, 10, 4))).IsTrue();
        await Assert.That(february.Length).IsEqualTo(28);
        await Assert.That(leapFebruary.Length).IsEqualTo(29);
    }

    [Test]
    public async Task Adding_months_clamps_to_the_last_day_of_the_destination()
    {
        var shifted = MonthModel.Gregorian.AddMonths(new DateOnly(2026, 1, 31), 1);
        var key = MonthKey.FromGregorian(new DateOnly(2026, 1, 31)).AddMonths(13);

        await Assert.That(shifted).IsEqualTo(new DateOnly(2026, 2, 28));
        await Assert.That(key.Year).IsEqualTo(2027);
        await Assert.That(key.Month).IsEqualTo(2);
        await Assert.That(MonthModel.Gregorian.MonthsBetween(new DateOnly(2026, 10, 31), new DateOnly(2027, 1, 1)))
            .IsEqualTo(3);
    }

    [Test]
    public async Task Missing_day_of_month_is_silence()
    {
        var pattern = new MonthlyPattern<string>(
        [
            new(1, "open"),
            new(15, "close"),
        ]);
        var calendar = new MonthBasedCalendar<string>(MonthModel.Gregorian, pattern: pattern);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Value).IsEqualTo("open");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 2)).HasValue).IsFalse();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 2)).Provenance)
            .IsEqualTo(MonthDayProvenance.None);
    }

    [Test]
    public async Task Day_31_is_silence_in_a_shorter_month()
    {
        var pattern = new MonthlyPattern<string>([new(31, "payroll")]);
        var calendar = new MonthBasedCalendar<string>(MonthModel.Gregorian, pattern: pattern);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 31)).Value).IsEqualTo("payroll");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 11, 30)).HasValue).IsFalse();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 2, 28)).HasValue).IsFalse();
    }

    [Test]
    public async Task Cycle_rotates_from_the_anchor_month()
    {
        var monthA = new MonthlyPattern<string>([new(1, "A")]);
        var monthB = new MonthlyPattern<string>([new(1, "B")]);
        var cycle = new MonthCycle<string>(
            MonthModel.Gregorian,
            new DateOnly(2026, 9, 1),
            [monthA, monthB]);
        var calendar = new MonthBasedCalendar<string>(MonthModel.Gregorian, cycle: cycle);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 9, 1)).Value).IsEqualTo("A");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Value).IsEqualTo("B");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 11, 1)).Value).IsEqualTo("A");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Provenance)
            .IsEqualTo(MonthDayProvenance.Cycle);
    }

    [Test]
    public async Task Dated_override_beats_the_pattern()
    {
        var pattern = new MonthlyPattern<string>([new(1, "routine")]);
        var calendar = new MonthBasedCalendar<string>(
            MonthModel.Gregorian,
            pattern: pattern,
            overrides: [new(new DateOnly(2026, 10, 1), "exception")]);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Value).IsEqualTo("exception");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Provenance)
            .IsEqualTo(MonthDayProvenance.Override);
        await Assert.That(calendar.Resolve(new DateOnly(2026, 11, 1)).Value).IsEqualTo("routine");
    }

    [Test]
    public async Task Effective_range_silence_does_not_reset_other_days()
    {
        var pattern = new MonthlyPattern<string>([new(1, "v2")]);
        var calendar = new MonthBasedCalendar<string>(
            MonthModel.Gregorian,
            pattern: pattern,
            effectiveFrom: new DateOnly(2026, 5, 15));

        await Assert.That(calendar.Resolve(new DateOnly(2026, 5, 1)).HasValue).IsFalse();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 6, 1)).Value).IsEqualTo("v2");
    }
}
