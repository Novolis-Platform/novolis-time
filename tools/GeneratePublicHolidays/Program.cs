using System.Text.Json;
using GeneratePublicHolidays;
using PublicHoliday;

var countries = new[] { "NO", "BE", "GB", "FR", "PL", "FI" };
var years = Enumerable.Range(2021, 6).ToArray();
var outputPath = args.Length > 0
    ? Path.GetFullPath(args[0])
    : Path.GetFullPath(Path.Combine(
        AppContext.BaseDirectory,
        "..",
        "..",
        "..",
        "..",
        "..",
        "src",
        "Novolis.Time.Workday",
        "GeneratedHolidays.json"));

var packageVersion = typeof(PublicHolidayFactory).Assembly.GetName().Version?.ToString() ?? "unknown";
var countriesMap = new Dictionary<string, Dictionary<string, List<HolidayRow>>>(StringComparer.Ordinal);
foreach (var country in countries)
{
    var provider = PublicHolidayFactory.GetPublicHolidayForCountry(country);
    var yearMap = new Dictionary<string, List<HolidayRow>>(StringComparer.Ordinal);
    foreach (var year in years)
    {
        var names = provider.PublicHolidayNames(year);
        var holidays = names
            .Select(pair => new HolidayRow(
                DateOnly.FromDateTime(pair.Key).ToString("yyyy-MM-dd"),
                string.IsNullOrWhiteSpace(pair.Value) ? "Public holiday" : pair.Value))
            .GroupBy(holiday => holiday.Date)
            .Select(group => group.First())
            .OrderBy(holiday => holiday.Date)
            .ToList();
        yearMap[year.ToString()] = holidays;
    }

    countriesMap[country] = yearMap;
}

var document = new CatalogDocument(
    "PublicHoliday",
    packageVersion,
    "novolis-time-holiday-generator-1",
    countriesMap);
var json = JsonSerializer.Serialize(document, new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
});
Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
File.WriteAllText(outputPath, json + Environment.NewLine);
Console.WriteLine($"Wrote {outputPath}");
