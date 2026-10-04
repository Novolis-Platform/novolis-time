namespace Novolis.Time.Workday;

/// <summary>Holiday catalog assembled from the in-code calendar baselines.</summary>
public sealed class GeneratedHolidayDocument
{
    /// <summary>Gets the source package identity recorded at generation.</summary>
    public string SourcePackage { get; init; } = string.Empty;

    /// <summary>Gets the source package version recorded at generation.</summary>
    public string SourcePackageVersion { get; init; } = string.Empty;

    /// <summary>Gets the generator version that froze the catalog.</summary>
    public string GeneratorVersion { get; init; } = string.Empty;

    /// <summary>Gets holidays keyed by country code, then calendar year.</summary>
    public Dictionary<string, Dictionary<string, List<PublicHolidayFact>>> Countries { get; init; } = [];
}
