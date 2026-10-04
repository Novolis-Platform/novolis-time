<!-- novolis-marketing:start -->
<p align="center">
  <a href="https://github.com/Novolis-Platform">
    <img src="https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/logo-brand-transparent.svg" width="360" alt="Novolis"/>
  </a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/Novolis-Platform/.github/main/brand/banners/novolis-time.svg" width="100%" alt="novolis-time"/>
</p>

<p align="center">
  <strong>Calendar and worktime primitives</strong><br/>
  Clock intervals, workday calendars, and worktime facts. These libraries do not calculate money, payroll, or leave.
</p>

<p align="center">
  <a href="https://novolis-platform.github.io/.github/novolis-time/"><img src="https://img.shields.io/badge/docs-portfolio-0a7ea3" alt="docs"/></a>
  <a href="https://github.com/Novolis-Platform/novolis-time/actions"><img src="https://img.shields.io/github/actions/workflow/status/Novolis-Platform/novolis-time/merge.yml?branch=main&label=merge&logo=github" alt="merge"/></a>
  <a href="https://github.com/orgs/Novolis-Platform/packages?repo_name=novolis-time"><img src="https://img.shields.io/badge/packages-GitHub%20Packages-0a7ea3?logo=nuget" alt="packages"/></a>
  <a href="https://github.com/Novolis-Platform"><img src="https://img.shields.io/badge/org-Novolis--Platform-111827" alt="org"/></a>
</p>

<p align="center">
  <a href="https://novolis-platform.github.io/.github/novolis-time/">Docs</a>
  ·
  <a href="https://nuget.pkg.github.com/Novolis-Platform/index.json"><code>https://nuget.pkg.github.com/Novolis-Platform/index.json</code></a>
  ·
  <a href="https://github.com/Novolis-Platform/.github/blob/main/profile/README.md">Org landing</a>
  ·
  <a href="https://github.com/Novolis-Platform/novolis-governance">Governance</a>
</p>

---
<!-- novolis-marketing:end -->
<!-- novolis-package-index:start -->
> **GitHub Packages shows this repository README on every package page** (upstream limitation).
> Open the **package README** for install and quick start — embedded in each .nupkg and linked below.

## Published packages

| Package | Install | Package README |
|---------|---------|----------------|
| `Novolis.Time` | `dotnet add package Novolis.Time` | [README](https://github.com/Novolis-Platform/novolis-time/blob/main/src/Novolis.Time/README.md) |
| `Novolis.Time.Week` | `dotnet add package Novolis.Time.Week` | [README](https://github.com/Novolis-Platform/novolis-time/blob/main/src/Novolis.Time.Week/README.md) |
| `Novolis.Time.Workday` | `dotnet add package Novolis.Time.Workday` | [README](https://github.com/Novolis-Platform/novolis-time/blob/main/src/Novolis.Time.Workday/README.md) |
| `Novolis.Time.Worktime` | `dotnet add package Novolis.Time.Worktime` | [README](https://github.com/Novolis-Platform/novolis-time/blob/main/src/Novolis.Time.Worktime/README.md) |
| `Novolis.Time.Worktime.Legal` | `dotnet add package Novolis.Time.Worktime.Legal` | [README](https://github.com/Novolis-Platform/novolis-time/blob/main/src/Novolis.Time.Worktime.Legal/README.md) |

For NuGet.org and Visual Studio, the **embedded** README.md inside each package is authoritative.

<!-- novolis-package-index:end -->

# novolis-time

Immutable time, workday-calendar, and worktime primitives for Novolis products.

## Documentation

| Doc | Topic |
| --- | --- |
| [docs/README.md](docs/README.md) | Doc index |
| [docs/getting-started.md](docs/getting-started.md) | Install, first calculations, build and test |
| [docs/design.md](docs/design.md) | Package stack, goals, and non-goals |
| [docs/release.md](docs/release.md) | CalVer and publish |

## Build

```powershell
dotnet test d:\novolis\novolis-time\tests\Novolis.Time.Unit\Novolis.Time.Unit.csproj -p:NovolisUseProjectReferences=true
```

Packages publish to GitHub Packages at `2026.1.*`. Consumers use NuGet sources only. Local multi-repository builds use `LibraryReference` or the platform solution.
