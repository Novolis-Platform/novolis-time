using Novolis.Time.Worktime.Legal;

namespace Novolis.Time.Unit;

public sealed class CountryPresetFeatureTests
{
    [Test]
    public async Task Preserves_legal_identity_when_the_caller_configures_the_overtime_message()
    {
        var preset = NorwayWorktimeFixture.CreateDraftPreset();
        var configured = preset.WithOvertimeAgreementMessage(
            "Record the manager agreement reference before financial compensation is processed.");

        await Assert.That(configured.Id).IsEqualTo(preset.Id);
        await Assert.That(configured.Version).IsEqualTo(preset.Version);
        await Assert.That(configured.Citation).IsEqualTo(preset.Citation);
        await Assert.That(configured.ReviewState).IsEqualTo(LegalReviewState.Draft);
        await Assert.That(configured.OvertimeAgreementMessage).Contains("manager agreement reference");
    }
}
