<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Calendar

Immutable workday calendars and business-day arithmetic. A calendar answers whether a date is a workday and why a non-workday is excluded, and it keeps source metadata so an application can retain the calendar version behind a decision.

## Install

```bash
dotnet add package Novolis.Time.Calendar
```

Requires .NET 10 (`net10.0`) and `Novolis.Time`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Calendar;

var due = BusinessDayCalculator.AddBusinessDays(start, 5, calendar);
```

`calendar` is an `IWorkdayCalendar`. Public holidays override the weekday pattern. This package does not load holiday data.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Calendar.PublicHoliday` | Build calendars from the offline PublicHoliday data set |
| `Novolis.Time.Worktime` | Apply a calendar to expected worktime |
| `Novolis.Time` | Clock intervals inside a workday |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
