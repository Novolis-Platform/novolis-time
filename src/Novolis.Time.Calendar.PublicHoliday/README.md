<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Calendar.PublicHoliday

Offline public-holiday workday calendars backed by the PublicHoliday NuGet package. The calendar caches each requested year, records package and country source metadata, and stays a calendar utility. It does not decide pay, leave, or legal entitlement.

## Install

```bash
dotnet add package Novolis.Time.Calendar.PublicHoliday
```

Requires .NET 10 (`net10.0`), `Novolis.Time.Calendar`, and the PublicHoliday package. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Calendar.PublicHoliday;

var calendar = new PublicHolidayWorkdayCalendar("no-workdays", "NO");
var workday = calendar.IsWorkday(new DateOnly(2026, 5, 17));
```

`PublicHolidayWorkdayCalendar` fills a year when a date falls outside the years already built. `PublicHolidayWorkdayCalendarFactory.Create` freezes one year into a `WorkdayCalendar`. This package does not call a holiday service.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Calendar` | Calendar contract and business-day arithmetic without holiday data |
| `Novolis.Time.Worktime` | Use the calendar as the employment calendar |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
