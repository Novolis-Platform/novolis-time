<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Workday

Immutable workday calendars and business-day arithmetic. A workday calendar answers whether a date is a workday and why a non-workday is excluded, and it keeps source metadata so an application can retain the calendar version behind a decision.

This is not a general calendar API. It does not own weekly or monthly patterns, pay, or leave.

Public holidays are in-code baseline collections for the selected locations, covering the generation year and the next five years. Runtime code does not call the third-party holiday library. Sparse `CalendarOverride` configuration replaces or clears one date and leaves the other baseline rules in place.

## Install

```bash
dotnet add package Novolis.Time.Workday
```

Requires .NET 10 (`net10.0`) and `Novolis.Time`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Workday;

var calendar = WorkdayCalendar.FromGeneratedHolidays("no-workdays-2026", "NO", 2026);
var due = BusinessDayCalculator.AddBusinessDays(new DateOnly(2026, 10, 1), 5, calendar);
```

Regenerate the in-code baselines:

```powershell
dotnet run --project d:\novolis\novolis-time\tools\GeneratePublicHolidays\GeneratePublicHolidays.csproj -- d:\novolis\novolis-time\src\Novolis.Time.Workday\Calendars
```

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Week` | Week identity, weekly patterns, and rotating cycles |
| `Novolis.Time.Month` | Month identity, monthly patterns, and month arithmetic |
| `Novolis.Time.Worktime` | Apply a workday calendar to expected worktime |
| `Novolis.Time` | Clock intervals inside a workday |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
