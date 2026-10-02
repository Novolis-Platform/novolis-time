# novolis-time

Immutable time, workday-calendar, and worktime primitives for Novolis products.

## Packages

| Package | Purpose |
|---|---|
| `Novolis.Time` | Local clock intervals and duration primitives |
| `Novolis.Time.Week` | ISO week boundaries |
| `Novolis.Time.Calendar` | Workday calendars and business-day arithmetic |
| `Novolis.Time.Calendar.PublicHoliday` | Offline public-holiday calendar construction through PublicHoliday |
| `Novolis.Time.Worktime` | Expected versus actual worktime and flex normalization |
| `Novolis.Time.Worktime.Legal` | Versioned legal messages and worktime presets |

The libraries record time facts and explanations. They do not calculate money, payroll, or leave.

## Build

```powershell
dotnet test d:\novolis\novolis-time\Novolis.Time.slnx
```

Packages publish to GitHub Packages at `2026.1.*`. Consumers use NuGet sources only; local multi-repository builds use `LibraryReference` or the platform solution.
