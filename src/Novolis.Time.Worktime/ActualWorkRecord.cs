using System.Collections.Immutable;
using Novolis.Time;

namespace Novolis.Time.Worktime;

/// <summary>Records the actual local-time interval worked on one date.</summary>
public sealed record ActualWorkRecord
{
    /// <summary>Initializes an actual work record.</summary>
    public ActualWorkRecord(
        Guid id,
        DateOnly date,
        ClockInterval presence,
        ClockInterval? takenBreak,
        IEnumerable<FinancialCompensationMark>? financialCompensationMarks,
        string comment,
        bool hasManagerAgreement = false)
    {
        ArgumentNullException.ThrowIfNull(comment);

        if (takenBreak is { } breakInterval && !presence.Contains(breakInterval))
        {
            throw new ArgumentException("A taken break must be inside the presence interval.", nameof(takenBreak));
        }

        var marks = financialCompensationMarks?.ToImmutableArray() ?? ImmutableArray<FinancialCompensationMark>.Empty;
        if (marks.Any(mark => !presence.Contains(mark.Interval)))
        {
            throw new ArgumentException("A financial-compensation mark must be inside the presence interval.", nameof(financialCompensationMarks));
        }

        if (marks.OrderBy(mark => mark.Interval.Start)
            .Zip(marks.OrderBy(mark => mark.Interval.Start).Skip(1))
            .Any(pair => pair.First.Interval.OverlapDuration(pair.Second.Interval) > TimeSpan.Zero))
        {
            throw new ArgumentException("Financial-compensation marks must not overlap.", nameof(financialCompensationMarks));
        }

        Id = id;
        Date = date;
        Presence = presence;
        TakenBreak = takenBreak;
        FinancialCompensationMarks = marks;
        Comment = comment;
        HasManagerAgreement = hasManagerAgreement;
    }

    /// <summary>Gets the immutable record identifier.</summary>
    public Guid Id { get; }

    /// <summary>Gets the local work date.</summary>
    public DateOnly Date { get; }

    /// <summary>Gets the full clock interval during which the employee was present.</summary>
    public ClockInterval Presence { get; }

    /// <summary>Gets the break that was actually taken, if any.</summary>
    public ClockInterval? TakenBreak { get; }

    /// <summary>Gets the non-overlapping intervals marked for financial compensation.</summary>
    public ImmutableArray<FinancialCompensationMark> FinancialCompensationMarks { get; }

    /// <summary>Gets the employee comment saved with the record.</summary>
    public string Comment { get; }

    /// <summary>Gets whether a manager agreement was recorded for compensation-marked hours.</summary>
    public bool HasManagerAgreement { get; }
}
