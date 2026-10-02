namespace Novolis.Time.Worktime;

/// <summary>Describes an append-only normalization of unused positive flex, with no pay amount.</summary>
public sealed record FlexNormalization(
    string PolicyId,
    TimeSpan PreviousSaldo,
    TimeSpan OpeningSaldo,
    TimeSpan NormalizedUnusedFlex,
    TimeSpan FinanciallyCompensated)
{
    /// <summary>Returns a positive-flex normalization for a closing saldo.</summary>
    public static FlexNormalization Create(FlexCarryPolicy policy, TimeSpan closingSaldo)
    {
        ArgumentNullException.ThrowIfNull(policy);

        var normalized = closingSaldo > policy.PositiveCarryCap
            ? closingSaldo - policy.PositiveCarryCap
            : TimeSpan.Zero;
        var opening = closingSaldo - normalized;

        return new FlexNormalization(
            policy.Id,
            closingSaldo,
            opening,
            normalized,
            TimeSpan.Zero);
    }
}
