<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Month

Month identity and month-based calendars. Use this package to name Gregorian months, measure month length, add months, describe repeating day-of-month patterns, rotate multi-month cycles, and apply dated exceptions. It does not own holidays, pay, or leave.

Month math uses `MonthModel.Gregorian`. It never reads `CultureInfo.CurrentCulture`. Adding months clamps a day that does not exist in the destination month to that month's last day. A missing day-of-month is silence, not a reset. Day 31 assigned on a shorter month is silence.

## Install

```bash
dotnet add package Novolis.Time.Month
```

Requires .NET 10 (`net10.0`) and `Novolis.Time`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Month;

var key = MonthKey.FromGregorian(new DateOnly(2026, 10, 4));
var range = MonthRange.From(key);
var next = key.AddMonths(1);

var pattern = new MonthlyPattern<string>(
[
    new(1, "open"),
    new(15, "close"),
]);
var calendar = new MonthBasedCalendar<string>(MonthModel.Gregorian, pattern: pattern);
var middle = calendar.Resolve(new DateOnly(2026, 10, 2));
```

A missing day is silence, not a reset.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time` | Same-day clock intervals |
| `Novolis.Time.Week` | Week identity and week-based calendars |
| `Novolis.Time.Workday` | Workdays, holiday baselines, and business-day arithmetic |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
