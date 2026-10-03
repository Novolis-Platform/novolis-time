# Novolis.Time.Worktime

Immutable expected-versus-actual worktime primitives.

Profiles, templates, employment settings, snapshots, actual records, classifications, and flex normalization are pure values and calculations. Financial-compensation markers are duration facts only; this package does not calculate money, payroll, or leave.
# Novolis.Time.Worktime

Expected versus actual worktime, and flex normalization. It records time facts. It does not calculate pay or leave.

## Install

```powershell
dotnet add package Novolis.Time.Worktime
```

Requires .NET 10, `Novolis.Time`, and `Novolis.Time.Calendar`.

## Quick start

```csharp
using Novolis.Time.Worktime;

var expected = WorktimeCalculator.CreateExpectedSnapshot(date, settings);
var balance = WorktimeCalculator.Calculate(record, expected);
```

`settings` freezes the profile, template, calendar, and employment fraction used for that day. `record` is the actual presence for the same date.
