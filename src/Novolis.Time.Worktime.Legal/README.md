<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Worktime.Legal

Versioned legal messages and starter worktime presets. Evaluators return classifications and messages rather than rejecting recorded work. Every included preset remains draft until current local law and the collective agreement have been reviewed and approved by the adopting organisation.

## Install

```bash
dotnet add package Novolis.Time.Worktime.Legal
```

Requires .NET 10 (`net10.0`) and `Novolis.Time.Worktime`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Worktime.Legal;

var preset = WorktimeLegalPresets.NorwayPrivate;
var draft = preset.ReviewState == LegalReviewState.Draft;
```

A draft preset can explain a rule. It is not an approval to apply that rule in production.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Worktime` | Record expected and actual time before any legal message |
| `Novolis.Time.Calendar.PublicHoliday` | The calendar those records are classified against |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Presets ship as `LegalReviewState.Draft`. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
