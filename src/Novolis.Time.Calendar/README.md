# Novolis.Time.Calendar

Immutable workday-calendar abstractions and business-day arithmetic for Novolis products.

Calendars answer whether a date is a workday and why a non-workday is excluded. They carry source metadata so an application can retain the calendar version behind a decision.
# Novolis.Time.Calendar

Immutable workday calendars and business-day arithmetic.

## Install

```powershell
dotnet add package Novolis.Time.Calendar
```

Requires .NET 10 and `Novolis.Time`.

## Quick start

```csharp
using Novolis.Time.Calendar;

var due = BusinessDayCalculator.AddBusinessDays(start, 5, calendar);
```

`calendar` is an `IWorkdayCalendar`. Public holidays override the weekday pattern. The calculator does not load holiday data itself.
