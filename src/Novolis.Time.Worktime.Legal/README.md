<!-- novolis-pkg-brand:start -->
[![Novolis](https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-icon.png)](https://novolis-platform.github.io/.github/novolis-time/)

[Novolis](https://github.com/Novolis-Platform) · [Docs](https://novolis-platform.github.io/.github/novolis-time/) · [Source](https://github.com/Novolis-Platform/novolis-time)
<!-- novolis-pkg-brand:end -->

# Novolis.Time.Worktime.Legal

Versioned legal messages for worktime values. Evaluators return classifications and messages rather than rejecting recorded work. The adopting product supplies preset values. A preset remains draft until that organisation reviews the local source and marks it approved.

## Install

```bash
dotnet add package Novolis.Time.Worktime.Legal
```

Requires .NET 10 (`net10.0`) and `Novolis.Time.Worktime`. Restore from nuget.org + GitHub Packages (`https://nuget.pkg.github.com/Novolis-Platform/index.json`).

## Quick start

```csharp
using Novolis.Time.Worktime;
using Novolis.Time.Worktime.Legal;

var preset = new WorktimeLegalPreset(
    "local.agreement",
    "2026.1",
    "NO",
    "Local agreement",
    TimeSpan.FromHours(9),
    new FlexCarryPolicy("local.agreement", TimeSpan.FromHours(40), TimeSpan.FromHours(-10)),
    new ApprovalSchedule(5, 5, 10),
    true,
    "Manager agreement is recorded with the day.",
    LegalReviewState.Draft);
```

The caller supplies the preset. A draft preset can explain a rule. It is not an approval to apply that rule in production.

## Related packages

| Package | When to use |
|---------|-------------|
| `Novolis.Time.Worktime` | Record expected and actual time before any legal message |
| `Novolis.Time.Workday` | The workday calendar those records are classified against |

## More documentation

- [Getting started](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/getting-started.md)
- [Design](https://github.com/Novolis-Platform/novolis-time/blob/main/docs/design.md)

## Support

Pre-release `2026.1.*` on GitHub Packages. Presets ship as `LegalReviewState.Draft`. Issues: [novolis-time](https://github.com/Novolis-Platform/novolis-time/issues).
