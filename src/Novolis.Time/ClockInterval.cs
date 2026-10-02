namespace Novolis.Time;

/// <summary>Represents a non-overnight local clock interval.</summary>
public readonly record struct ClockInterval
{
    /// <summary>Initializes a clock interval.</summary>
    /// <param name="start">The inclusive start time.</param>
    /// <param name="end">The exclusive end time.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the interval is empty or overnight.</exception>
    public ClockInterval(TimeOnly start, TimeOnly end)
    {
        if (end <= start)
        {
            throw new ArgumentOutOfRangeException(nameof(end), end, "A clock interval must end after it starts.");
        }

        Start = start;
        End = end;
    }

    /// <summary>Gets the inclusive start time.</summary>
    public TimeOnly Start { get; }

    /// <summary>Gets the exclusive end time.</summary>
    public TimeOnly End { get; }

    /// <summary>Gets the interval duration.</summary>
    public TimeSpan Duration => End - Start;

    /// <summary>Returns whether this interval fully contains another interval.</summary>
    public bool Contains(ClockInterval other) =>
        Start <= other.Start && End >= other.End;

    /// <summary>Gets the overlap duration with another interval.</summary>
    public TimeSpan OverlapDuration(ClockInterval other)
    {
        var overlapStart = Start > other.Start ? Start : other.Start;
        var overlapEnd = End < other.End ? End : other.End;
        return overlapEnd <= overlapStart ? TimeSpan.Zero : overlapEnd - overlapStart;
    }
}
