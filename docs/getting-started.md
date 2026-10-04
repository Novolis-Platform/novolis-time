# Getting started

## Prerequisites

- .NET 10 (`net10.0`)
- NuGet sources: **nuget.org** + **GitHub Packages** (`https://nuget.pkg.github.com/Novolis-Platform/index.json`)
- Local multi-repo work: ProjectReference mode via `-p:NovolisUseProjectReferences=true` (see [platform-project-ref-mode](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/platform-project-ref-mode.md))

## Install

```bash
dotnet add package Novolis.Time
dotnet add package Novolis.Time.Workday
dotnet add package Novolis.Time.Worktime
```

Add `Novolis.Time.Week` for week-based calendars and `Novolis.Time.Month` for month identity and month arithmetic. Add `Novolis.Time.Worktime.Legal` when a product supplies its own legal preset values.

## Clock interval

```csharp
using Novolis.Time;

var core = new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0));
var overlap = core.OverlapDuration(new ClockInterval(new TimeOnly(8, 0), new TimeOnly(12, 0)));
```

An interval must end after it starts on the same local day. Overnight ranges are rejected.

## Months

```csharp
using Novolis.Time.Month;

var october = MonthRange.From(MonthKey.FromGregorian(new DateOnly(2026, 10, 4)));
var clamped = MonthModel.Gregorian.AddMonths(new DateOnly(2026, 1, 31), 1);
```

`october` is 1 October through 31 October. `clamped` is 28 February 2026, because that month has no day 31. A missing day in a `MonthlyPattern<T>` is silence.

## Workdays

```csharp
using Novolis.Time.Workday;

var calendar = WorkdayCalendar.FromGeneratedHolidays("no-workdays-2026", "NO", 2026);
var constitutionDay = calendar.IsWorkday(new DateOnly(2026, 5, 17));
var due = BusinessDayCalculator.AddBusinessDays(new DateOnly(2026, 5, 15), 1, calendar);
```

Public holidays are in-code baselines in `Novolis.Time.Workday`. Sparse configuration overrides one date:

```csharp
using Novolis.Time.Workday;

var calendar = WorkdayCalendar.FromGeneratedHolidays(
    "no-workdays-2026",
    "NO",
    2026,
    configuration: [new CalendarOverride(new DateOnly(2026, 5, 1), null)]);
```

Regenerate the baselines for this year and the next five:

```powershell
dotnet run --project d:\novolis\novolis-time\tools\GeneratePublicHolidays\GeneratePublicHolidays.csproj -- d:\novolis\novolis-time\src\Novolis.Time.Workday\Calendars
```

## Expected versus actual

```csharp
using Novolis.Time;
using Novolis.Time.Workday;
using Novolis.Time.Worktime;

var profile = new WorktimeProfile(
    "oslo-office",
    "Oslo office",
    new ClockInterval(new TimeOnly(7, 0), new TimeOnly(17, 0)),
    new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0)),
    new ClockInterval(new TimeOnly(11, 30), new TimeOnly(12, 0)),
    TimeSpan.FromHours(37.5),
    TimeSpan.FromHours(7.5),
    PresenceClassification.Flex);
var template = new ExpectedDayTemplate(
    "office-day",
    "Office day",
    [new KeyValuePair<DayOfWeek, ClockInterval>(DayOfWeek.Thursday, new ClockInterval(new TimeOnly(8, 0), new TimeOnly(16, 0)))]);
var settings = new EmploymentSettings(
    "employee-1",
    profile,
    WorkdayCalendar.FromGeneratedHolidays("no-workdays-2026", "NO", 2026),
    template,
    1m);
var expected = WorktimeCalculator.CreateExpectedSnapshot(new DateOnly(2026, 10, 1), settings);
```

`settings` freezes the profile, template, workday calendar, and employment fraction used for that day.

## Build and test

```powershell
dotnet test d:\novolis\novolis-time\tests\Novolis.Time.Unit\Novolis.Time.Unit.csproj -p:NovolisUseProjectReferences=true
```
