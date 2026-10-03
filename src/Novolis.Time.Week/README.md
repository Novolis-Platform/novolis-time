<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Week

ISO week boundaries for local dates. Use this package to group dates into Monday-through-Sunday weeks without worktime rules, storage, UI, payroll, or leave.

## Install

```bash
dotnet add package Novolis.Time.Week
```

Requires .NET 10 (`net10.0`) and `Novolis.Time`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Week;

var week = IsoWeek.From(new DateOnly(2026, 10, 1));
var inside = week.Contains(new DateOnly(2026, 10, 3));
```

`IsoWeek` starts on Monday and ends on Sunday.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time` | Same-day clock intervals |
| `Novolis.Time.Calendar` | Workdays that are not the same as ISO weeks |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
