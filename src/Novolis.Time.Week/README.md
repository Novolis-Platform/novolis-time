# Novolis.Time.Week

ISO and culture-aware week boundaries for immutable Novolis time values.

Use this package to group dates into week-based projections without introducing worktime rules, storage, UI, payroll, or leave calculations.
# Novolis.Time.Week

ISO week boundaries for local dates.

## Install

```powershell
dotnet add package Novolis.Time.Week
```

Requires .NET 10 and `Novolis.Time`.

## Quick start

```csharp
using Novolis.Time.Week;

var week = IsoWeek.From(new DateOnly(2026, 10, 1));
var inside = week.Contains(new DateOnly(2026, 10, 3));
```

`IsoWeek` starts on Monday and ends on Sunday.
