# Getting started

## Prerequisites

- .NET 10 (`net10.0`)
- NuGet sources: **nuget.org** + **GitHub Packages** (`https://nuget.pkg.github.com/Novolis-Platform/index.json`)
- Local multi-repo work: ProjectReference mode via `-p:NovolisUseProjectReferences=true` (see [platform-project-ref-mode](https://github.com/Novolis-Platform/novolis-governance/blob/main/docs/platform-project-ref-mode.md))

## Install

```bash
dotnet add package Novolis.Time
dotnet add package Novolis.Time.Calendar.PublicHoliday
dotnet add package Novolis.Time.Worktime
```

`Novolis.Time.Calendar` is pulled in by the public-holiday and worktime packages. Add `Novolis.Time.Week` for ISO weeks, and `Novolis.Time.Worktime.Legal` only when a product needs the draft presets.

## Clock interval

```csharp
using Novolis.Time;

var core = new ClockInterval(new TimeOnly(9, 0), new TimeOnly(15, 0));
var overlap = core.OverlapDuration(new ClockInterval(new TimeOnly(8, 0), new TimeOnly(12, 0)));
```

An interval must end after it starts on the same local day. Overnight ranges are rejected.

## Public-holiday workdays

```csharp
using Novolis.Time.Calendar;
using Novolis.Time.Calendar.PublicHoliday;

var calendar = new PublicHolidayWorkdayCalendar("no-workdays", "NO");
var constitutionDay = calendar.IsWorkday(new DateOnly(2026, 5, 17));
var due = BusinessDayCalculator.AddBusinessDays(new DateOnly(2026, 5, 15), 1, calendar);
```

`PublicHolidayWorkdayCalendar` fills a year from the bundled PublicHoliday data set the first time a date in that year is asked. `PublicHolidayWorkdayCalendarFactory.Create` freezes one year into a `WorkdayCalendar` instead.

## Expected versus actual

```csharp
using Novolis.Time;
using Novolis.Time.Calendar.PublicHoliday;
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
    PublicHolidayWorkdayCalendarFactory.Create("no-workdays-2026", 2026, "NO"),
    template,
    1m);
var expected = WorktimeCalculator.CreateExpectedSnapshot(new DateOnly(2026, 10, 1), settings);
```

`settings` freezes the profile, template, calendar, and employment fraction used for that day.

## Build and test

```powershell
dotnet test d:\novolis\novolis-time\tests\Novolis.Time.Unit\Novolis.Time.Unit.csproj -p:NovolisUseProjectReferences=true
```
