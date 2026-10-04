<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time

Immutable local clock intervals and duration primitives. `ClockInterval` is a non-overnight local range with deterministic duration and overlap arithmetic. This package has no calendar, legal-policy, storage, UI, payroll, or leave dependency.

## Install

```bash
dotnet add package Novolis.Time
```

Requires .NET 10 (`net10.0`). Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time;

var core = new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0));
var overlap = core.OverlapDuration(new ClockInterval(new TimeOnly(8, 0), new TimeOnly(12, 0)));
```

An interval must end after it starts on the same local day. Overnight ranges are rejected.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Week` | Week identity and week-based calendars |
| `Novolis.Time.Month` | Month identity and month-based calendars |
| `Novolis.Time.Workday` | Workdays and business-day arithmetic |
| `Novolis.Time.Worktime` | Expected versus actual worktime |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
