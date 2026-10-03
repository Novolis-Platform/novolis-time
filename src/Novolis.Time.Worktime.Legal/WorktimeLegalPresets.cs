using Novolis.Time.Worktime;

namespace Novolis.Time.Worktime.Legal;

/// <summary>Provides versioned starter presets that require source review before production activation.</summary>
public static class WorktimeLegalPresets
{
    /// <summary>Gets the Norway private-agreement starter preset pending local legal and agreement review.</summary>
    public static WorktimeLegalPreset NorwayPrivate { get; } = Create(
        "norway.private.flex",
        "2026.1",
        "NO",
        "arbeidsmiljøloven §§ 10-4, 10-6, 10-7 and 10-8; private flex agreement",
        TimeSpan.FromHours(9),
        TimeSpan.FromHours(40),
        TimeSpan.FromHours(-10),
        "Overtime must have been agreed by the manager. The hours are retained and this caution is stored.",
        LegalReviewState.Draft);

    /// <summary>Gets the Norway state-handbook starter preset pending confirmation against the active agreement.</summary>
    public static WorktimeLegalPreset NorwayState { get; } = Create(
        "norway.state.flex",
        "2026.1",
        "NO",
        "Særavtale om fleksibel arbeidstid i staten, 2026–2027; arbeidsmiljøloven §§ 10-4, 10-6, 10-7 and 10-8",
        TimeSpan.FromHours(9),
        TimeSpan.FromHours(50),
        TimeSpan.FromHours(-10),
        "Ordered overtime must be recorded separately. Unused positive flex normalizes from the saldo and is not a payment.",
        LegalReviewState.Draft);

    /// <summary>Gets a Belgium starter preset that requires local review.</summary>
    public static WorktimeLegalPreset Belgium { get; } = Create(
        "belgium.office.flex",
        "2026.1-draft",
        "BE",
        "Belgian Labour Act 16 March 1971; local agreement required",
        TimeSpan.FromHours(8),
        TimeSpan.FromHours(12),
        TimeSpan.FromHours(-8),
        "Overtime must have been agreed by the manager. The hours are retained and the missing agreement is an anomaly.",
        LegalReviewState.Draft);

    /// <summary>Gets an England starter preset that requires local review.</summary>
    public static WorktimeLegalPreset England { get; } = Create(
        "england.office.flex",
        "2026.1-draft",
        "GB",
        "Working Time Regulations 1998; employment contract required",
        null,
        TimeSpan.FromHours(15),
        TimeSpan.FromHours(-7.5),
        "The contract decides whether manager agreement is required. The hours are retained either way.",
        LegalReviewState.Draft);

    /// <summary>Gets a France starter preset that requires local review.</summary>
    public static WorktimeLegalPreset France { get; } = Create(
        "france.annualisation",
        "2026.1-draft",
        "FR",
        "Code du travail, durée légale 35 heures; collective agreement required",
        null,
        TimeSpan.Zero,
        TimeSpan.Zero,
        "The collective agreement determines the compensation treatment. The hours are retained and the counter is informational.",
        LegalReviewState.Draft);

    /// <summary>Gets a Poland starter preset that requires local review.</summary>
    public static WorktimeLegalPreset Poland { get; } = Create(
        "poland.okres-rozliczeniowy",
        "2026.1-draft",
        "PL",
        "Kodeks pracy art. 151; local settlement agreement required",
        TimeSpan.FromHours(8),
        TimeSpan.FromHours(16),
        TimeSpan.FromHours(-8),
        "Nadgodziny need a reason and manager mark. The hours are retained and the missing mark is an anomaly.",
        LegalReviewState.Draft);

    /// <summary>Gets a Finland starter preset that requires local review.</summary>
    public static WorktimeLegalPreset Finland { get; } = Create(
        "finland.liukuva-tyoaika",
        "2026.1-draft",
        "FI",
        "Työaikalaki 872/2019 § 12; confirm agreement-specific saldo caps",
        TimeSpan.FromHours(8),
        TimeSpan.FromHours(60),
        TimeSpan.FromHours(-20),
        "Ylityö requires the recorded consent where the agreement requires it. The hours are retained and the missing consent is an anomaly.",
        LegalReviewState.Draft);

    private static WorktimeLegalPreset Create(
        string id,
        string version,
        string countryCode,
        string citation,
        TimeSpan? dailyOrdinaryLimit,
        TimeSpan positiveCarryCap,
        TimeSpan negativeCarryFloor,
        string overtimeAgreementMessage,
        LegalReviewState reviewState) =>
        new(
            id,
            version,
            countryCode,
            citation,
            dailyOrdinaryLimit,
            new FlexCarryPolicy(id, positiveCarryCap, negativeCarryFloor),
            new ApprovalSchedule(5, 5, 10),
            true,
            overtimeAgreementMessage,
            reviewState);
}
