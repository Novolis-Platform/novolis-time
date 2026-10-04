namespace GeneratePublicHolidays;

internal sealed record LocationSelection(string CountryCode, string TypeName, string DisplayName)
{
    /// <summary>Every ISO country the PublicHoliday factory can build. <c>PO</c> is omitted; it is not a valid ISO code.</summary>
    internal static LocationSelection[] Selected { get; } =
    [
        new("AT", "AustriaCalendar", "Austria"),
        new("AU", "AustraliaCalendar", "Australia"),
        new("BE", "BelgiumCalendar", "Belgium"),
        new("BR", "BrazilCalendar", "Brazil"),
        new("CA", "CanadaCalendar", "Canada"),
        new("CH", "SwitzerlandCalendar", "Switzerland"),
        new("CZ", "CzechRepublicCalendar", "Czech Republic"),
        new("DE", "GermanyCalendar", "Germany"),
        new("DK", "DenmarkCalendar", "Denmark"),
        new("EE", "EstoniaCalendar", "Estonia"),
        new("ES", "SpainCalendar", "Spain"),
        new("FI", "FinlandCalendar", "Finland"),
        new("FR", "FranceCalendar", "France"),
        new("GB", "UnitedKingdomCalendar", "United Kingdom"),
        new("GR", "GreeceCalendar", "Greece"),
        new("HR", "CroatiaCalendar", "Croatia"),
        new("HU", "HungaryCalendar", "Hungary"),
        new("IE", "IrelandCalendar", "Ireland"),
        new("IT", "ItalyCalendar", "Italy"),
        new("JP", "JapanCalendar", "Japan"),
        new("KZ", "KazakhstanCalendar", "Kazakhstan"),
        new("LT", "LithuaniaCalendar", "Lithuania"),
        new("LU", "LuxembourgCalendar", "Luxembourg"),
        new("MX", "MexicoCalendar", "Mexico"),
        new("NL", "NetherlandsCalendar", "Netherlands"),
        new("NO", "NorwayCalendar", "Norway"),
        new("NZ", "NewZealandCalendar", "New Zealand"),
        new("PL", "PolandCalendar", "Poland"),
        new("PT", "PortugalCalendar", "Portugal"),
        new("RO", "RomaniaCalendar", "Romania"),
        new("RS", "SerbiaCalendar", "Serbia"),
        new("SE", "SwedenCalendar", "Sweden"),
        new("SI", "SloveniaCalendar", "Slovenia"),
        new("SK", "SlovakiaCalendar", "Slovakia"),
        new("TR", "TurkeyCalendar", "Turkey"),
        new("US", "UnitedStatesCalendar", "United States"),
        new("ZA", "SouthAfricaCalendar", "South Africa"),
    ];
}
