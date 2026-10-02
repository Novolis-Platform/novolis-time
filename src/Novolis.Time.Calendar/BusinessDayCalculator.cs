namespace Novolis.Time.Calendar;

/// <summary>Calculates dates using an immutable workday calendar.</summary>
public static class BusinessDayCalculator
{
    /// <summary>Returns the date that is a number of workdays after an exclusive start date.</summary>
    public static DateOnly AddBusinessDays(
        DateOnly exclusiveStart,
        int businessDays,
        IWorkdayCalendar calendar)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(businessDays);
        ArgumentNullException.ThrowIfNull(calendar);

        var current = exclusiveStart;
        var remaining = businessDays;

        while (remaining > 0)
        {
            current = current.AddDays(1);
            if (calendar.IsWorkday(current))
            {
                remaining--;
            }
        }

        return current;
    }
}
