namespace Novolis.Time.Month;

/// <summary>Resolved day-of-month value plus the source that produced it.</summary>
public readonly record struct MonthDayResolution<T>
{
    /// <summary>Initializes a resolution.</summary>
    public MonthDayResolution(T? value, MonthDayProvenance provenance)
    {
        if (provenance == MonthDayProvenance.None)
        {
            Value = default;
            Provenance = MonthDayProvenance.None;
            return;
        }

        Value = value;
        Provenance = provenance;
    }

    /// <summary>Gets the resolved value when the calendar had an opinion.</summary>
    public T? Value { get; }

    /// <summary>Gets how the value was selected.</summary>
    public MonthDayProvenance Provenance { get; }

    /// <summary>Gets whether the calendar contributed a value.</summary>
    public bool HasValue => Provenance != MonthDayProvenance.None;

    /// <summary>A silent resolution with no opinion.</summary>
    public static MonthDayResolution<T> None { get; } = new(default, MonthDayProvenance.None);
}
