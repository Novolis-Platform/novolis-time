namespace GeneratePublicHolidays;

internal sealed record CatalogDocument(
    string SourcePackage,
    string SourcePackageVersion,
    string GeneratorVersion,
    Dictionary<string, Dictionary<string, List<HolidayRow>>> Countries);
