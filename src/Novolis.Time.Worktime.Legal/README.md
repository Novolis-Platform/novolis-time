# Novolis.Time.Worktime.Legal

Versioned legal-rule messages and starter worktime-policy presets.

Evaluators return classifications and messages rather than rejecting recorded work. Every included preset remains draft until current local law and collective-agreement material has been reviewed and approved by the adopting organisation.
# Novolis.Time.Worktime.Legal

Versioned legal messages and starter worktime presets. Presets stay in draft until a local agreement review marks them reviewed.

## Install

```powershell
dotnet add package Novolis.Time.Worktime.Legal
```

Requires .NET 10 and `Novolis.Time.Worktime`.

## Quick start

```csharp
using Novolis.Time.Worktime.Legal;

var preset = WorktimeLegalPresets.NorwayPrivate;
var draft = preset.ReviewState == LegalReviewState.Draft;
```

A draft preset can explain a rule. It is not an approval to apply that rule in production.
