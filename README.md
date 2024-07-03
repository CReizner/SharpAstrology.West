# SharpAstrology.West

## SharpAstrology Packages
| Package                                                                                                                | Version | Description                              | Licence  |
|:-----------------------------------------------------------------------------------------------------------------------|:-------:|:-----------------------------------------|:--------:|
| [SharpAstrology.Base](https://github.com/CReizner/SharpAstrology.Base)                                                 | 0.10.0  | Base library                             |   MIT    |
| [SharpAstrology.SwissEph](https://github.com/CReizner/SharpAstrology.SwissEph)                                         |  0.2.1  | Ephemerides package based on SwissEphNet | AGPL-3.0 |
| [SharpAstrology.HumanDesign](https://github.com/CReizner/SharpAstrology.HumanDesign)                                   |  1.1.0  | Extensions for the Human Design system   |   MIT    |
| [SharpAstrology.HumanDesign.BlazorComponents](https://github.com/CReizner/SharpAstrology.HumanDesign.BlazorComponents) |  0.2.1  | Human Design charts as Blazor components |   MIT    |
| [SharpAstrology.Vedic](https://github.com/CReizner/SharpAstrology.Vedic)                                               |  0.1.0  | Extensions for Vedic astrology systems   |   MIT    |
| [SharpAstrology.West](https://github.com/CReizner/SharpAstrology.West)                                                 |  0.1.0  | Extensions for western astrology systems |   MIT    |

## Calculate aspects with defined orbits

In Western astrology, the distances between planets are assigned certain aspects. Each planet is assigned a certain range, an orbit. This package provides an OrbitBuilder to define these orbits freely.

```C#
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Ephemerides;
using SharpAstrology.ExtensionMethods;
using SharpAstrology.Utility;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService("[PATH_TO_EPHEMERIDES_FILES]").CreateContext();

// create chart
var chart = new AstrologyChart(date, eph);

// Aspects with default western orbits
var aspect = chart.AspectBetween(Planets.Sun, Planets.SouthNode);
Console.WriteLine(aspect);
// Output: Conjunction

// Use default western orbits explicitly
var orbitBuilder = OrbitBuilder.WithWesternDefaultOrbits();
var orbits = orbitBuilder.ToOrbits();
aspect = chart.AspectBetween(Planets.Sun, Planets.SouthNode, orbits);
Console.WriteLine(aspect);
// Output: Conjunction

// Change specific orbit definitions
orbitBuilder.SetRule(Planets.Sun, Aspects.Conjunction, 1);
orbitBuilder.SetRule(Planets.SouthNode, Aspects.Conjunction, 1);
orbits = orbitBuilder.ToOrbits()
aspect = chart.AspectBetween(Planets.Sun, Planets.SouthNode, orbits);
Console.WriteLine(aspect);
// Output: None
```