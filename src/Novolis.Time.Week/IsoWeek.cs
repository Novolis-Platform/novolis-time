namespace Novolis.Time.Week;

/// <summary>Represents an ISO week as an immutable Monday-through-Sunday date range.</summary>
public readonly record struct IsoWeek
{
    /// <summary>Initializes an ISO week.</summary>
    /// <param name="monday">The Monday that starts the week.</param>
    public IsoWeek(DateOnly monday)
    {
        if (monday.DayOfWeek != DayOfWeek.Monday)
        {
            throw new ArgumentOutOfRangeException(nameof(monday), monday, "An ISO week starts on Monday.");
        }

        Monday = monday;
    }

    /// <summary>Gets the Monday that starts this week.</summary>
    public DateOnly Monday { get; }

    /// <summary>Gets the Sunday that ends this week.</summary>
    public DateOnly Sunday => Monday.AddDays(6);

    /// <summary>Returns the ISO week containing a date.</summary>
    public static IsoWeek From(DateOnly date)
    {
        var offset = ((int)date.DayOfWeek + 6) % 7;
        return new IsoWeek(date.AddDays(-offset));
    }

    /// <summary>Returns whether a date is in this week.</summary>
    public bool Contains(DateOnly date) => date >= Monday && date <= Sunday;
}
