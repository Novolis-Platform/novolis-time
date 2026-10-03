# Novolis.Time.Calendar.PublicHoliday

Offline public-holiday workday calendars backed by the `PublicHoliday` NuGet package.

The calendar caches each requested year, records package and country source metadata, and remains a calendar utility only. It does not decide pay, leave, or legal entitlement.
# Novolis.Time.Calendar.PublicHoliday

Offline public-holiday calendars built from the PublicHoliday data set.

## Install

```powershell
dotnet add package Novolis.Time.Calendar.PublicHoliday
```

Requires .NET 10, `Novolis.Time.Calendar`, and the PublicHoliday package.

## Quick start

```csharp
using Novolis.Time.Calendar.PublicHoliday;

var calendar = PublicHolidayWorkdayCalendarFactory.Create(
    "no-2026",
    2026,
    "NO");
var workday = calendar.IsWorkday(new DateOnly(2026, 5, 17));
```

`Create` builds one calendar year from the bundled data set. `PublicHolidayWorkdayCalendar` fills later years when a date falls outside the years already built. This package does not call a holiday service.
