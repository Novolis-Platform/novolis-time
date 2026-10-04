# Design

## Position

`novolis-time` is an **orthogonal island**. It is not on the Math → Physics → Simulation → Gaming → Avalonia spine. It has no Avalonia, MAUI, Simulation, or payroll dependency.

```text
Novolis.Time
    ├── Novolis.Time.Week
    └── Novolis.Time.Workday
            └── Novolis.Time.Worktime
                    └── Novolis.Time.Worktime.Legal
```

Holiday generation is a private tool (`tools/GeneratePublicHolidays`). It may reference the third-party `PublicHoliday` package. Runtime assemblies consume only the frozen catalog embedded in `Novolis.Time.Workday`.

## Packages

| Package | Role |
| --- | --- |
| `Novolis.Time` | `ClockInterval`: same-day local ranges, duration, containment, and overlap |
| `Novolis.Time.Week` | `WeekModel`, `WeekKey`, `WeekRange`, `WeeklyPattern<T>`, `WeekCycle<T>`, `WeekBasedCalendar<T>` |
| `Novolis.Time.Workday` | `IWorkdayCalendar`, `WorkdayCalendar`, `BusinessDayCalculator`, frozen public-holiday facts |
| `Novolis.Time.Worktime` | Expected snapshots, actual records, day balance, and flex normalization |
| `Novolis.Time.Worktime.Legal` | Versioned rule messages and starter presets that remain draft until a local review |

Workday and worktime public APIs see only `IWorkdayCalendar`. They are not a general calendar.

## Goals

- Immutable values a product can store and explain later
- Workday decisions that keep source metadata (data set, version, country)
- Week-based schedules whose missing days are silence, not a reset
- Worktime math that separates expected time, actual time, flex, and financial-compensation duration
- Legal presets that stay in `LegalReviewState.Draft` until the adopting organisation marks them reviewed

## Non-goals

- Money, payroll, tax, or leave balances
- Overnight clock intervals
- Calling a holiday web service or the third-party holiday library at runtime
- Treating a draft legal preset as permission to enforce a rule
- UI, storage, or Avalonia types
- Hours `DayShape` stacking (that stays in Novolis Hours)

## Consumers

Products such as Novolis Hours compose these packages at the app layer. They own persistence, identity, and any payment rules.
