using Novolis.Time.Workday;

namespace Novolis.Time.Unit;

public sealed class CalendarBaselineFeatureTests
{
    [Test]
    public async Task Baseline_covers_this_year_and_the_next_five()
    {
        string[] locales =
        [
            "AT", "AU", "BE", "BR", "CA", "CH", "CZ", "DE", "DK", "EE", "ES", "FI", "FR", "GB", "GR",
            "HR", "HU", "IE", "IT", "JP", "KZ", "LT", "LU", "MX", "NL", "NO", "NZ", "PL", "PT", "RO",
            "RS", "SE", "SI", "SK", "TR", "US", "ZA",
        ];
        await Assert.That(CalendarCatalog.CountryCodes.Order().ToArray()).IsEquivalentTo(locales);

        var years = NorwayCalendar.Baseline.Select(holiday => holiday.Date.Year).Distinct().Order().ToArray();
        await Assert.That(years.Length).IsEqualTo(6);
        await Assert.That(years[0]).IsEqualTo(NorwayCalendar.FirstYear);
        await Assert.That(years[^1]).IsEqualTo(NorwayCalendar.LastYear);
        await Assert.That(years[^1] - years[0]).IsEqualTo(5);
    }

    [Test]
    public async Task Sparse_configuration_clears_one_holiday_and_leaves_the_baseline()
    {
        var labourDay = new DateOnly(NorwayCalendar.FirstYear, 5, 1);
        var calendar = WorkdayCalendar.FromGeneratedHolidays(
            "no-workdays",
            "NO",
            NorwayCalendar.FirstYear,
            configuration: [new CalendarOverride(labourDay, null)]);

        await Assert.That(calendar.IsWorkday(labourDay)).IsTrue();
        await Assert.That(calendar.IsWorkday(new DateOnly(NorwayCalendar.FirstYear, 5, 17))).IsFalse();
        await Assert.That(calendar.GetNonWorkdayReason(new DateOnly(NorwayCalendar.FirstYear, 1, 1)))
            .IsEqualTo("Første nyttårsdag");
    }

    [Test]
    public async Task Sparse_configuration_can_name_an_extra_date()
    {
        var extra = new DateOnly(NorwayCalendar.FirstYear, 6, 2);
        var calendar = new Calendar(
            "no-baseline",
            NorwayCalendar.CountryCode,
            NorwayCalendar.Baseline,
            [new CalendarOverride(extra, "Local observance")]);

        await Assert.That(calendar.Resolve(extra)).IsEqualTo("Local observance");
        await Assert.That(calendar.Resolve(new DateOnly(NorwayCalendar.FirstYear, 5, 17)))
            .IsEqualTo("Grunnlovsdagen");
    }
}
