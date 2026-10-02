namespace Novolis.Time.Worktime.Legal;

/// <summary>Captures an immutable legal or agreement message emitted while retaining a worktime record.</summary>
public sealed record RuleFiring(
    string RuleId,
    WorktimeRuleSeverity Severity,
    string Message,
    string Citation,
    string PresetId,
    string PresetVersion);
