using System.Collections.Frozen;

namespace Novolis.Time.Workday;

/// <summary>
/// Location calendar of baseline holiday rules.
/// Generated rules are defaults. Sparse <see cref="CalendarOverride"/> configuration replaces or clears
/// individual dates and leaves every other baseline rule unchanged.
/// </summary>
public sealed class Calendar
{
    private readonly FrozenDictionary<DateOnly, string> baseline;
    private readonly FrozenDictionary<DateOnly, string?> configuration;

    /// <summary>Initializes a calendar from baseline rules and optional sparse configuration.</summary>
    public Calendar(
        string id,
        string countryCode,
        IEnumerable<PublicHolidayFact> baseline,
        IEnumerable<CalendarOverride>? configuration = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(countryCode);
        ArgumentNullException.ThrowIfNull(baseline);

        Id = id;
        CountryCode = countryCode.ToUpperInvariant();
        this.baseline = baseline
            .GroupBy(holiday => holiday.Date)
            .ToFrozenDictionary(group => group.Key, group => group.First().Name);
        this.configuration = (configuration ?? [])
            .GroupBy(item => item.Date)
            .ToFrozenDictionary(group => group.Key, group => group.Last().Name);
    }

    /// <summary>Gets the stable calendar identifier.</summary>
    public string Id { get; }

    /// <summary>Gets the ISO country code these baseline rules belong to.</summary>
    public string CountryCode { get; }

    /// <summary>Returns the holiday name after sparse configuration, or <see langword="null"/> when the date is not a holiday.</summary>
    public string? Resolve(DateOnly date)
    {
        if (configuration.TryGetValue(date, out var configured))
        {
            return configured;
        }

        return baseline.TryGetValue(date, out var name) ? name : null;
    }

    /// <summary>Returns the holiday facts that remain after sparse configuration.</summary>
    public IReadOnlyList<PublicHolidayFact> EffectiveHolidays()
    {
        var dates = new SortedSet<DateOnly>(baseline.Keys);
        foreach (var date in configuration.Keys)
        {
            dates.Add(date);
        }

        var holidays = new List<PublicHolidayFact>(dates.Count);
        foreach (var date in dates)
        {
            var name = Resolve(date);
            if (name is not null)
            {
                holidays.Add(new PublicHolidayFact(date, name));
            }
        }

        return holidays;
    }
}
