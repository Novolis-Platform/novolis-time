using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Worktime.FeatureTests;

public sealed class CountryPresetFeatureTests
{
    [Test]
    [Arguments("NO-private")]
    [Arguments("NO-state")]
    [Arguments("BE")]
    [Arguments("GB")]
    [Arguments("FR")]
    [Arguments("PL")]
    [Arguments("FI")]
    public async Task Keeps_all_starter_presets_in_draft_until_their_local_sources_are_reviewed(string countryCode)
    {
        var preset = countryCode switch
        {
            "NO-private" => WorktimeLegalPresets.NorwayPrivate,
            "NO-state" => WorktimeLegalPresets.NorwayState,
            "BE" => WorktimeLegalPresets.Belgium,
            "GB" => WorktimeLegalPresets.England,
            "FR" => WorktimeLegalPresets.France,
            "PL" => WorktimeLegalPresets.Poland,
            "FI" => WorktimeLegalPresets.Finland,
            _ => throw new ArgumentOutOfRangeException(nameof(countryCode)),
        };

        var expectedCountry = countryCode.StartsWith("NO-", StringComparison.Ordinal) ? "NO" : countryCode;
        await Assert.That(preset.CountryCode).IsEqualTo(expectedCountry);
        await Assert.That(preset.ReviewState).IsEqualTo(LegalReviewState.Draft);
        await Assert.That(preset.OvertimeAgreementMessage).IsNotEmpty();
    }

    [Test]
    public async Task Preserves_legal_identity_when_an_employer_configures_the_overtime_message()
    {
        var configured = WorktimeLegalPresets.NorwayPrivate
            .WithOvertimeAgreementMessage("Record the manager agreement reference before financial compensation is processed.");

        await Assert.That(configured.Id).IsEqualTo(WorktimeLegalPresets.NorwayPrivate.Id);
        await Assert.That(configured.Version).IsEqualTo(WorktimeLegalPresets.NorwayPrivate.Version);
        await Assert.That(configured.Citation).IsEqualTo(WorktimeLegalPresets.NorwayPrivate.Citation);
        await Assert.That(configured.OvertimeAgreementMessage).Contains("manager agreement reference");
    }
}
