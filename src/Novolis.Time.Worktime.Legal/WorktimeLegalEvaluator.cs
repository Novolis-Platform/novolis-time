using System.Collections.Immutable;
using Novolis.Time.Worktime;

namespace Novolis.Time.Worktime.Legal;

/// <summary>Evaluates worktime notices without rejecting the underlying record.</summary>
public static class WorktimeLegalEvaluator
{
    /// <summary>Returns messages for an immutable worktime record.</summary>
    public static ImmutableArray<RuleFiring> Evaluate(
        ActualWorkRecord record,
        DayBalance balance,
        WorktimeProfile profile,
        WorktimeLegalPreset preset)
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(balance);
        ArgumentNullException.ThrowIfNull(profile);
        ArgumentNullException.ThrowIfNull(preset);

        var firings = ImmutableArray.CreateBuilder<RuleFiring>();
        AddCoreCoverageFiring(firings, record, profile, preset);
        AddEnvelopeFiring(firings, record, profile, preset);
        AddDailyLimitFiring(firings, balance, preset);
        AddManagerAgreementFiring(firings, record, preset);
        return firings.ToImmutable();
    }

    private static void AddCoreCoverageFiring(
        ImmutableArray<RuleFiring>.Builder firings,
        ActualWorkRecord record,
        WorktimeProfile profile,
        WorktimeLegalPreset preset)
    {
        var missing = WorktimeCalculator.GetMissingCoreCoverage(record, profile);
        if (missing <= TimeSpan.Zero)
        {
            return;
        }

        firings.Add(Create(
            "core.coverage",
            $"Core coverage is short by {FormatHours(missing)}.",
            preset));
    }

    private static void AddEnvelopeFiring(
        ImmutableArray<RuleFiring>.Builder firings,
        ActualWorkRecord record,
        WorktimeProfile profile,
        WorktimeLegalPreset preset)
    {
        var outside = WorktimeCalculator.GetWorkedOutsideEnvelope(record, profile);
        if (outside <= TimeSpan.Zero)
        {
            return;
        }

        firings.Add(Create(
            "working-day.envelope",
            $"{FormatHours(outside)} was worked outside the configured working-day envelope.",
            preset));
    }

    private static void AddDailyLimitFiring(
        ImmutableArray<RuleFiring>.Builder firings,
        DayBalance balance,
        WorktimeLegalPreset preset)
    {
        if (preset.DailyOrdinaryLimit is not { } limit || balance.Actual <= limit)
        {
            return;
        }

        firings.Add(Create(
            "ordinary.daily-limit",
            $"{FormatHours(balance.Actual)} was recorded against a daily ordinary-hours notice of {FormatHours(limit)}.",
            preset));
    }

    private static void AddManagerAgreementFiring(
        ImmutableArray<RuleFiring>.Builder firings,
        ActualWorkRecord record,
        WorktimeLegalPreset preset)
    {
        if (!preset.RequiresManagerAgreementForFinancialCompensation ||
            record.FinancialCompensationMarks.IsEmpty ||
            record.HasManagerAgreement)
        {
            return;
        }

        firings.Add(Create("financial-compensation.manager-agreement", preset.OvertimeAgreementMessage, preset));
    }

    private static RuleFiring Create(string ruleId, string message, WorktimeLegalPreset preset) =>
        new(
            ruleId,
            WorktimeRuleSeverity.Caution,
            message,
            preset.Citation,
            preset.Id,
            preset.Version);

    private static string FormatHours(TimeSpan duration) =>
        FormattableString.Invariant($"{duration.TotalHours:0.##} h");
}
