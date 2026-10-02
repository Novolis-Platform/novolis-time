using Novolis.Time.Worktime;

namespace Novolis.Time.Worktime.Legal;

/// <summary>Versioned legal and agreement configuration used to classify and explain worktime.</summary>
public sealed record WorktimeLegalPreset
{
    /// <summary>Initializes a legal preset.</summary>
    public WorktimeLegalPreset(
        string id,
        string version,
        string countryCode,
        string citation,
        TimeSpan? dailyOrdinaryLimit,
        FlexCarryPolicy flexCarryPolicy,
        ApprovalSchedule approvalSchedule,
        bool requiresManagerAgreementForFinancialCompensation,
        string overtimeAgreementMessage,
        LegalReviewState reviewState)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(citation);
        ArgumentNullException.ThrowIfNull(flexCarryPolicy);
        ArgumentNullException.ThrowIfNull(approvalSchedule);
        ArgumentException.ThrowIfNullOrWhiteSpace(overtimeAgreementMessage);

        Id = id;
        Version = version;
        CountryCode = countryCode;
        Citation = citation;
        DailyOrdinaryLimit = dailyOrdinaryLimit;
        FlexCarryPolicy = flexCarryPolicy;
        ApprovalSchedule = approvalSchedule;
        RequiresManagerAgreementForFinancialCompensation = requiresManagerAgreementForFinancialCompensation;
        OvertimeAgreementMessage = overtimeAgreementMessage;
        ReviewState = reviewState;
    }

    /// <summary>Gets the stable preset identifier.</summary>
    public string Id { get; }

    /// <summary>Gets the immutable reviewed preset version.</summary>
    public string Version { get; }

    /// <summary>Gets the ISO country code.</summary>
    public string CountryCode { get; }

    /// <summary>Gets the legal or agreement citation displayed with messages.</summary>
    public string Citation { get; }

    /// <summary>Gets the daily ordinary-hours notice threshold, when configured.</summary>
    public TimeSpan? DailyOrdinaryLimit { get; }

    /// <summary>Gets the flex carry policy.</summary>
    public FlexCarryPolicy FlexCarryPolicy { get; }

    /// <summary>Gets the review-clock policy.</summary>
    public ApprovalSchedule ApprovalSchedule { get; }

    /// <summary>Gets whether compensation marks require a manager agreement warning.</summary>
    public bool RequiresManagerAgreementForFinancialCompensation { get; }

    /// <summary>Gets the rendered overtime-agreement message.</summary>
    public string OvertimeAgreementMessage { get; }

    /// <summary>Gets the production-review state of the legal source material.</summary>
    public LegalReviewState ReviewState { get; }
}
