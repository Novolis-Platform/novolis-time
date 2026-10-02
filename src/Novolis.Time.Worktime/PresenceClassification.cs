namespace Novolis.Time.Worktime;

/// <summary>Identifies how a worked presence slice contributes to the worktime book.</summary>
public enum PresenceClassification
{
    /// <summary>Expected ordinary presence.</summary>
    Ordinary,

    /// <summary>Uncompensated surplus or shortfall that changes the flex saldo.</summary>
    Flex,

    /// <summary>Presence marked for financial compensation.</summary>
    FinancialCompensation,
}
