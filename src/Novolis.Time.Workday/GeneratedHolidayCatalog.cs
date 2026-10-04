using System.Reflection;
using System.Text.Json;

namespace Novolis.Time.Workday;

/// <summary>Loads the frozen public-holiday catalog embedded in this assembly.</summary>
public static class GeneratedHolidayCatalog
{
    private static readonly Lazy<GeneratedHolidayDocument> catalog = new(Load);

    /// <summary>Gets the embedded catalog.</summary>
    public static GeneratedHolidayDocument Current => catalog.Value;

    /// <summary>Returns generated holidays for one country and year.</summary>
    public static IReadOnlyList<PublicHolidayFact> GetHolidays(string countryCode, int year)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        var country = countryCode.ToUpperInvariant();
        if (!Current.Countries.TryGetValue(country, out var years) ||
            !years.TryGetValue(year.ToString(), out var holidays))
        {
            throw new InvalidOperationException(
                $"No generated public holidays exist for {country} {year}. Regenerate with the private GeneratePublicHolidays tool.");
        }

        return holidays;
    }

    /// <summary>Returns every generated year for a country.</summary>
    public static IReadOnlyList<int> GetYears(string countryCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        var country = countryCode.ToUpperInvariant();
        if (!Current.Countries.TryGetValue(country, out var years))
        {
            throw new InvalidOperationException(
                $"No generated public holidays exist for {country}. Regenerate with the private GeneratePublicHolidays tool.");
        }

        return years.Keys
            .Select(int.Parse)
            .OrderBy(year => year)
            .ToArray();
    }

    private static GeneratedHolidayDocument Load()
    {
        var assembly = typeof(GeneratedHolidayCatalog).Assembly;
        using var stream = assembly.GetManifestResourceStream("Novolis.Time.Workday.GeneratedHolidays.json")
            ?? throw new InvalidOperationException("The generated holiday catalog is missing from Novolis.Time.Workday.");
        return JsonSerializer.Deserialize<GeneratedHolidayDocument>(stream, JsonOptions)
            ?? throw new InvalidOperationException("The generated holiday catalog could not be read.");
    }

    private static JsonSerializerOptions JsonOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
    };
}
