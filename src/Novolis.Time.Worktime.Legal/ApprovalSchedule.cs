namespace Novolis.Time.Worktime.Legal;

/// <summary>Defines non-blocking business-day clocks for a monthly review period.</summary>
public sealed record ApprovalSchedule
{
    /// <summary>Initializes a review schedule.</summary>
    public ApprovalSchedule(int employeeSubmitBusinessDays, int managerReviewBusinessDays, int hrResolutionBusinessDays)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(employeeSubmitBusinessDays);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(managerReviewBusinessDays);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(hrResolutionBusinessDays);

        EmployeeSubmitBusinessDays = employeeSubmitBusinessDays;
        ManagerReviewBusinessDays = managerReviewBusinessDays;
        HrResolutionBusinessDays = hrResolutionBusinessDays;
    }

    /// <summary>Gets employee submit business days.</summary>
    public int EmployeeSubmitBusinessDays { get; }

    /// <summary>Gets manager review business days.</summary>
    public int ManagerReviewBusinessDays { get; }

    /// <summary>Gets HR or Higher resolution business days.</summary>
    public int HrResolutionBusinessDays { get; }
}
