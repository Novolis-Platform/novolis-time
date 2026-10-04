namespace Novolis.Time.Workday;

/// <summary>Loads the in-code public-holiday baselines for the selected locations.</summary>
public static class GeneratedHolidayCatalog
{
    private static readonly Lazy<GeneratedHolidayDocument> catalog = new(Load);

    /// <summary>Gets the baseline catalog.</summary>
    public static GeneratedHolidayDocument Current => catalog.Value;

    /// <summary>Returns generated holidays for one country and year.</summary>
    public static IReadOnlyList<PublicHolidayFact> GetHolidays(string countryCode, int year)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        var country = countryCode.ToUpperInvariant();
        if (year < CalendarCatalog.FirstYear || year > CalendarCatalog.LastYear)
        {
            throw new InvalidOperationException(
                $"No generated public holidays exist for {country} {year}. The baseline covers {CalendarCatalog.FirstYear} through {CalendarCatalog.LastYear}.");
        }

        return CalendarCatalog.GetBaseline(country)
            .Where(holiday => holiday.Date.Year == year)
            .ToArray();
    }

    /// <summary>Returns every generated year for a country.</summary>
    public static IReadOnlyList<int> GetYears(string countryCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        _ = CalendarCatalog.GetBaseline(countryCode.ToUpperInvariant());
        return Enumerable.Range(
                CalendarCatalog.FirstYear,
                CalendarCatalog.LastYear - CalendarCatalog.FirstYear + 1)
            .ToArray();
    }

    private static GeneratedHolidayDocument Load()
    {
        var countries = new Dictionary<string, Dictionary<string, List<PublicHolidayFact>>>(StringComparer.Ordinal);
        foreach (var country in CalendarCatalog.CountryCodes)
        {
            var byYear = CalendarCatalog.GetBaseline(country)
                .GroupBy(holiday => holiday.Date.Year)
                .ToDictionary(
                    group => group.Key.ToString(),
                    group => group.ToList(),
                    StringComparer.Ordinal);
            for (var year = CalendarCatalog.FirstYear; year <= CalendarCatalog.LastYear; year++)
            {
                byYear.TryAdd(year.ToString(), []);
            }

            countries[country] = byYear;
        }

        return new GeneratedHolidayDocument
        {
            SourcePackage = GeneratedCalendarSource.Package,
            SourcePackageVersion = GeneratedCalendarSource.PackageVersion,
            GeneratorVersion = GeneratedCalendarSource.GeneratorVersion,
            Countries = countries,
        };
    }
}
