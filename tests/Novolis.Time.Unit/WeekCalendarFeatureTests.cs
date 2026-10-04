using System.Globalization;
using Novolis.Time.Week;

namespace Novolis.Time.Unit;

public sealed class WeekCalendarFeatureTests
{
    [Test]
    public async Task Iso_week_uses_bcl_year_boundaries()
    {
        var thursday = new DateOnly(2026, 12, 31);
        var friday = new DateOnly(2027, 1, 1);
        var key = WeekKey.FromIso(thursday);
        var range = WeekRange.From(key);

        await Assert.That(ISOWeek.GetYear(thursday)).IsEqualTo(2026);
        await Assert.That(key.WeekYear).IsEqualTo(2026);
        await Assert.That(key.WeekNumber).IsEqualTo(53);
        await Assert.That(range.Contains(thursday)).IsTrue();
        await Assert.That(range.Contains(friday)).IsTrue();
        await Assert.That(WeekModel.Iso.GetKey(friday).WeekYear).IsEqualTo(2026);
    }

    [Test]
    public async Task Snapshot_model_does_not_follow_process_locale()
    {
        var sundayStart = WeekModel.FromSnapshot(DayOfWeek.Sunday, 1);
        var saturday = new DateOnly(2026, 10, 3);
        var range = WeekRange.From(saturday, sundayStart);

        await Assert.That(range.Start).IsEqualTo(new DateOnly(2026, 9, 27));
        await Assert.That(range.Start.DayOfWeek).IsEqualTo(DayOfWeek.Sunday);
        await Assert.That(WeekModel.Iso.GetWeekStart(saturday)).IsEqualTo(new DateOnly(2026, 9, 28));
    }

    [Test]
    public async Task Missing_weekday_is_silence()
    {
        var pattern = new WeeklyPattern<string>(
        [
            new(DayOfWeek.Monday, "office"),
            new(DayOfWeek.Tuesday, "office"),
        ]);
        var calendar = new WeekBasedCalendar<string>(WeekModel.Iso, pattern: pattern);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 5)).HasValue).IsTrue();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 7)).HasValue).IsFalse();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 7)).Provenance)
            .IsEqualTo(WeekDayProvenance.None);
    }

    [Test]
    public async Task Cycle_rotates_from_the_anchor_week()
    {
        var weekA = new WeeklyPattern<string>([new(DayOfWeek.Monday, "A")]);
        var weekB = new WeeklyPattern<string>([new(DayOfWeek.Monday, "B")]);
        var cycle = new WeekCycle<string>(
            WeekModel.Iso,
            new DateOnly(2026, 9, 28),
            [weekA, weekB]);
        var calendar = new WeekBasedCalendar<string>(WeekModel.Iso, cycle: cycle);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 9, 28)).Value).IsEqualTo("A");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 5)).Value).IsEqualTo("B");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 12)).Value).IsEqualTo("A");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 5)).Provenance)
            .IsEqualTo(WeekDayProvenance.Cycle);
    }

    [Test]
    public async Task Dated_override_beats_the_pattern()
    {
        var pattern = new WeeklyPattern<string>([new(DayOfWeek.Thursday, "routine")]);
        var calendar = new WeekBasedCalendar<string>(
            WeekModel.Iso,
            pattern: pattern,
            overrides: [new(new DateOnly(2026, 10, 1), "exception")]);

        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Value).IsEqualTo("exception");
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 1)).Provenance)
            .IsEqualTo(WeekDayProvenance.Override);
        await Assert.That(calendar.Resolve(new DateOnly(2026, 10, 8)).Value).IsEqualTo("routine");
    }

    [Test]
    public async Task Effective_range_silence_does_not_reset_other_days()
    {
        var pattern = new WeeklyPattern<string>([new(DayOfWeek.Thursday, "v2")]);
        var calendar = new WeekBasedCalendar<string>(
            WeekModel.Iso,
            pattern: pattern,
            effectiveFrom: new DateOnly(2026, 5, 15));

        await Assert.That(calendar.Resolve(new DateOnly(2026, 5, 14)).HasValue).IsFalse();
        await Assert.That(calendar.Resolve(new DateOnly(2026, 5, 21)).Value).IsEqualTo("v2");
    }
}
