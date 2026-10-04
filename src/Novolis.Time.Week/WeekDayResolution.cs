namespace Novolis.Time.Week;

/// <summary>Resolved weekday value plus the source that produced it.</summary>
public readonly record struct WeekDayResolution<T>
{
    /// <summary>Initializes a resolution.</summary>
    public WeekDayResolution(T? value, WeekDayProvenance provenance)
    {
        if (provenance == WeekDayProvenance.None)
        {
            Value = default;
            Provenance = WeekDayProvenance.None;
            return;
        }

        Value = value;
        Provenance = provenance;
    }

    /// <summary>Gets the resolved value when the calendar had an opinion.</summary>
    public T? Value { get; }

    /// <summary>Gets how the value was selected.</summary>
    public WeekDayProvenance Provenance { get; }

    /// <summary>Gets whether the calendar contributed a value.</summary>
    public bool HasValue => Provenance != WeekDayProvenance.None;

    /// <summary>A silent resolution with no opinion.</summary>
    public static WeekDayResolution<T> None { get; } = new(default, WeekDayProvenance.None);
}
