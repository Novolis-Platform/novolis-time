using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Worktime.FeatureTests;

public sealed class CountryPresetFeatureTests
{
    [Test]
    [Arguments("BE")]
    [Arguments("GB")]
    [Arguments("FR")]
    [Arguments("PL")]
    [Arguments("FI")]
    public async Task Keeps_non_norway_presets_in_draft_until_their_local_sources_are_reviewed(string countryCode)
    {
        var preset = countryCode switch
        {
            "BE" => WorktimeLegalPresets.Belgium,
            "GB" => WorktimeLegalPresets.England,
            "FR" => WorktimeLegalPresets.France,
            "PL" => WorktimeLegalPresets.Poland,
            "FI" => WorktimeLegalPresets.Finland,
            _ => throw new ArgumentOutOfRangeException(nameof(countryCode)),
        };

        await Assert.That(preset.CountryCode).IsEqualTo(countryCode);
        await Assert.That(preset.ReviewState).IsEqualTo(LegalReviewState.Draft);
        await Assert.That(preset.OvertimeAgreementMessage).IsNotEmpty();
    }
}
