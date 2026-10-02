namespace Novolis.Time.Worktime.Legal;

/// <summary>Identifies whether a legal preset may be enabled for production use.</summary>
public enum LegalReviewState
{
    /// <summary>The preset is a development draft.</summary>
    Draft,

    /// <summary>The preset source was reviewed but awaits production approval.</summary>
    Reviewed,

    /// <summary>The preset is approved for production use.</summary>
    Approved,
}
