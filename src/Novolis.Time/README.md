# Novolis.Time

Immutable local clock intervals and duration primitives for Novolis products.

`ClockInterval` represents a non-overnight local interval and provides deterministic duration and overlap arithmetic. The package has no calendar, legal-policy, storage, UI, payroll, or leave dependency.
# Novolis.Time

Immutable local clock intervals and duration primitives.

## Install

```powershell
dotnet add package Novolis.Time
```

Requires .NET 10.

## Quick start

```csharp
using Novolis.Time;

var core = new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0));
var overlap = core.OverlapDuration(new ClockInterval(new TimeOnly(8, 0), new TimeOnly(12, 0)));
```

An interval must end after it starts on the same local day. Overnight ranges are rejected.
