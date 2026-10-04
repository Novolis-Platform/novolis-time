# Release

## Versioning

CalVer via `d:\novolis\novolis-time\build\version.json` (`2026.1.*`). Packable projects share the repo stamp.

## Publish path

Maintainers ship on **`main`** (no maintainer PR for normal work):

1. Commit and `git push origin main`
2. `merge.yml` builds, tests, packs, and publishes to **GitHub Packages**
3. Consumers restore from nuget.org + `https://nuget.pkg.github.com/Novolis-Platform/index.json`

Do not publish via a local folder feed. See [nuget-only-policy](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/nuget-only-policy.md).

## Local verification before push

```powershell
dotnet test d:\novolis\novolis-time\tests\Novolis.Time.Unit\Novolis.Time.Unit.csproj -p:NovolisUseProjectReferences=true
dotnet run --file d:\novolis\novolis-governance\scripts\doc-audit.cs -- --repo d:\novolis\novolis-time
```

## Packages

| Package | Project |
| --- | --- |
| `Novolis.Time` | `src/Novolis.Time` |
| `Novolis.Time.Week` | `src/Novolis.Time.Week` |
| `Novolis.Time.Workday` | `src/Novolis.Time.Workday` |
| `Novolis.Time.Worktime` | `src/Novolis.Time.Worktime` |
| `Novolis.Time.Worktime.Legal` | `src/Novolis.Time.Worktime.Legal` |

## Governance

- [release.md](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/release.md)
- [contribution-policy.md](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/contribution-policy.md)
- [documentation-policy.md](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/documentation-policy.md)
