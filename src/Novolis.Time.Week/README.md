<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Week

Week identity and week-based calendars. Use this package to number weeks, describe repeating weekday patterns, rotate multi-week cycles, and apply dated exceptions. It does not own holidays, pay, or leave.

ISO week numbers delegate to `System.Globalization.ISOWeek`. Culture-specific weeks are snapshots (`WeekModel.FromSnapshot`); they never read `CultureInfo.CurrentCulture`.

## Install

```bash
dotnet add package Novolis.Time.Week
```

Requires .NET 10 (`net10.0`) and `Novolis.Time`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Week;

var key = WeekKey.FromIso(new DateOnly(2026, 10, 1));
var range = WeekRange.From(key);

var pattern = new WeeklyPattern<string>(
[
    new(DayOfWeek.Monday, "office"),
    new(DayOfWeek.Tuesday, "office"),
]);
var calendar = new WeekBasedCalendar<string>(WeekModel.Iso, pattern: pattern);
var thursday = calendar.Resolve(new DateOnly(2026, 10, 1));
```

A missing weekday is silence, not a reset.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time` | Same-day clock intervals |
| `Novolis.Time.Workday` | Workdays and business-day arithmetic |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
