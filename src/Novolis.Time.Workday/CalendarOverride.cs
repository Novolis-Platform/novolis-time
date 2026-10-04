namespace Novolis.Time.Workday;

/// <summary>
/// Sparse configuration for one local date on a <see cref="Calendar"/>.
/// A null <see cref="Name"/> clears a baseline holiday. A name adds or replaces the holiday for that date.
/// Every other baseline rule stays in place.
/// </summary>
/// <param name="Date">The local date this configuration applies to.</param>
/// <param name="Name">The holiday name, or <see langword="null"/> to clear the baseline holiday.</param>
public readonly record struct CalendarOverride(DateOnly Date, string? Name);
