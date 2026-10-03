# Design

## Position

`novolis-time` is an **orthogonal island**. It is not on the Math → Physics → Simulation → Gaming → Avalonia spine. It has no Avalonia, MAUI, Simulation, or payroll dependency.

```text
Novolis.Time
    ├── Novolis.Time.Week
    └── Novolis.Time.Calendar
            ├── Novolis.Time.Calendar.PublicHoliday   (PublicHoliday data set)
            └── Novolis.Time.Worktime
                    └── Novolis.Time.Worktime.Legal
```

## Packages

| Package | Role |
| --- | --- |
| `Novolis.Time` | `ClockInterval`: same-day local ranges, duration, containment, and overlap |
| `Novolis.Time.Week` | `IsoWeek`: Monday-through-Sunday boundaries |
| `Novolis.Time.Calendar` | `IWorkdayCalendar`, `WorkdayCalendar`, `BusinessDayCalculator` |
| `Novolis.Time.Calendar.PublicHoliday` | Offline country calendars from the PublicHoliday package |
| `Novolis.Time.Worktime` | Expected snapshots, actual records, day balance, and flex normalization |
| `Novolis.Time.Worktime.Legal` | Versioned rule messages and starter presets that remain draft until a local review |

Public-holiday types stay inside `Novolis.Time.Calendar.PublicHoliday`. Calendar and worktime public APIs see only `IWorkdayCalendar`.

## Goals

- Immutable values a product can store and explain later
- Calendar decisions that keep source metadata (data set, version, country)
- Worktime math that separates expected time, actual time, flex, and financial-compensation duration
- Legal presets that stay in `LegalReviewState.Draft` until the adopting organisation marks them reviewed

## Non-goals

- Money, payroll, tax, or leave balances
- Overnight clock intervals
- Calling a holiday web service
- Treating a draft legal preset as permission to enforce a rule
- UI, storage, or Avalonia types

## Consumers

Products such as Novolis Hours compose these packages at the app layer. They own persistence, identity, and any payment rules.
