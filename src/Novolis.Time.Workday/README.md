<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Workday

Immutable workday calendars and business-day arithmetic. A workday calendar answers whether a date is a workday and why a non-workday is excluded, and it keeps source metadata so an application can retain the calendar version behind a decision.

This is not a general calendar API. It does not own weekly patterns, Hours `DayShape` stacking, pay, or leave.

Public holidays come from frozen generated facts embedded in this package. Runtime code does not call the third-party holiday library.

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

Regenerate the embedded catalog from the repository root of novolis-time:

```powershell
dotnet run --project d:\novolis\novolis-time\tools\GeneratePublicHolidays\GeneratePublicHolidays.csproj
```

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Week` | Week identity, weekly patterns, and rotating cycles |
| `Novolis.Time.Worktime` | Apply a workday calendar to expected worktime |
| `Novolis.Time` | Clock intervals inside a workday |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
