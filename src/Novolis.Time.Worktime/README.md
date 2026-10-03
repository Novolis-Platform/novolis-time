<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Worktime

Expected versus actual worktime, and flex normalization. Profiles, templates, employment settings, snapshots, actual records, and classifications are pure values. Financial-compensation markers are duration facts only. This package does not calculate money, payroll, or leave.

## Install

```bash
dotnet add package Novolis.Time.Worktime
```

Requires .NET 10 (`net10.0`), `Novolis.Time`, and `Novolis.Time.Calendar`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Worktime;

var expected = WorktimeCalculator.CreateExpectedSnapshot(date, settings);
var balance = WorktimeCalculator.Calculate(record, expected);
```

`settings` freezes the profile, template, calendar, and employment fraction used for that day. `record` is the actual presence for the same date.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Calendar` | Supply the `IWorkdayCalendar` |
| `Novolis.Time.Calendar.PublicHoliday` | Build that calendar from offline holidays |
| `Novolis.Time.Worktime.Legal` | Attach draft legal messages to a recorded day |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
