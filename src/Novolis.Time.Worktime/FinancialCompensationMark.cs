using Novolis.Time;

namespace Novolis.Time.Worktime;

/// <summary>Marks a worked interval as requiring financial compensation without pricing it.</summary>
public sealed record FinancialCompensationMark(ClockInterval Interval, string Reason);
