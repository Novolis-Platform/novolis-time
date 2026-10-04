namespace Novolis.Time.Workday;

/// <summary>Identifies the workday data used to derive an expected workday.</summary>
public sealed record WorkdaySourceMetadata(
    string Source,
    string Version,
    string CountryCode,
    string? SubdivisionCode);
