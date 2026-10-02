namespace Novolis.Time.Worktime;

/// <summary>Defines the positive and negative flex saldo bounds for one settlement policy.</summary>
public sealed record FlexCarryPolicy
{
    /// <summary>Initializes a flex carry policy.</summary>
    public FlexCarryPolicy(string id, TimeSpan positiveCarryCap, TimeSpan negativeCarryFloor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        if (positiveCarryCap < TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(positiveCarryCap));
        }

        if (negativeCarryFloor > TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(negativeCarryFloor));
        }

        Id = id;
        PositiveCarryCap = positiveCarryCap;
        NegativeCarryFloor = negativeCarryFloor;
    }

    /// <summary>Gets the policy identifier.</summary>
    public string Id { get; }

    /// <summary>Gets the maximum positive flex saldo carried into the next period.</summary>
    public TimeSpan PositiveCarryCap { get; }

    /// <summary>Gets the minimum negative flex saldo carried into the next period.</summary>
    public TimeSpan NegativeCarryFloor { get; }
}
