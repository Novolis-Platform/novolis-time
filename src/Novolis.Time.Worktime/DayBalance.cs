namespace Novolis.Time.Worktime;

/// <summary>Decomposes actual presence into expected time, flex movement, and compensation-marked hours.</summary>
public sealed record DayBalance
{
    /// <summary>Initializes a validated day balance.</summary>
    public DayBalance(
        TimeSpan expected,
        TimeSpan actual,
        TimeSpan flexDelta,
        TimeSpan financiallyCompensated)
    {
        if (actual != expected + flexDelta + financiallyCompensated)
        {
            throw new ArgumentException("Actual presence must equal expected time plus flex delta plus financially compensated time.");
        }

        Expected = expected;
        Actual = actual;
        FlexDelta = flexDelta;
        FinanciallyCompensated = financiallyCompensated;
    }

    /// <summary>Gets the expected work duration.</summary>
    public TimeSpan Expected { get; }

    /// <summary>Gets the actual worked presence duration.</summary>
    public TimeSpan Actual { get; }

    /// <summary>Gets the signed flex movement for the day.</summary>
    public TimeSpan FlexDelta { get; }

    /// <summary>Gets the worked duration marked for financial compensation.</summary>
    public TimeSpan FinanciallyCompensated { get; }
}
