# SharpAstrology.West
${\color{red}This \space package \space is \space still \space experimental. \space Interfaces \space can \space change \space before \space version \space 1.0.0.}$

## SharpAstrology Packages
| Package                                                                                                                | Description                                            | Licence  |
|:-----------------------------------------------------------------------------------------------------------------------|:-------------------------------------------------------|:--------:|
| [SharpAstrology.Base](https://github.com/CReizner/SharpAstrology.Base)                                                 | Base library                                           |   MIT    |
| [SharpAstrology.SwissEph](https://github.com/CReizner/SharpAstrology.SwissEph)                                         | Ephemerides package based on SwissEphNet               | AGPL-3.0 |
| [SharpAstrology.Symbols.BlazorComponents](https://github.com/CReizner/SharpAstrology.Symbols.BlazorComponents)         | Astrological symbols as Blazor components              |   MIT    |
| [SharpAstrology.HumanDesign](https://github.com/CReizner/SharpAstrology.HumanDesign)                                   | Extensions for the Human Design system                 |   MIT    |
| [SharpAstrology.HumanDesign.BlazorComponents](https://github.com/CReizner/SharpAstrology.HumanDesign.BlazorComponents) | Human Design charts as Blazor components               |   MIT    |
| [SharpAstrology.Vedic](https://github.com/CReizner/SharpAstrology.Vedic)                                               | Extensions for Vedic astrology systems                 |   MIT    |
| [SharpAstrology.Vedic.BlazorComponents](https://github.com/CReizner/SharpAstrology.Vedic.BlazorComponents)             | Vedic astrology charts as Blazor components            |   MIT    |
| [SharpAstrology.West](https://github.com/CReizner/SharpAstrology.West)                                                 | Extensions for western astrology systems               |   MIT    |
| [SharpAstrology.West.BlazorComponents](https://github.com/CReizner/SharpAstrology.West.BlazorComponents)               | Western astrology charts as Blazor components          |   MIT    |
| [SharpAstrology.WebApp](https://github.com/CReizner/SharpAstrology.WebApp)                                             | Blazor Server app built on the SharpAstrology packages | AGPL-3.0 |

## Calculate aspects with defined orbits

In Western astrology, the distances between planets are assigned certain aspects. Each planet is assigned a certain range, an orbit. This package provides an OrbitBuilder to define these orbits freely.

### You can use default orbits
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
var orbits = orbitBuilder.Build();
aspect = chart.AspectBetween(Planets.Sun, Planets.SouthNode, orbits);
Console.WriteLine(aspect);
// Output: Conjunction

// Change specific orbit definitions
orbitBuilder.SetRule(Aspects.Conjunction, Planets.Sun, 1);
orbitBuilder.SetRule(Aspects.Conjunction, Planets.SouthNode, 1);
orbits = orbitBuilder.Build()
aspect = chart.AspectBetween(Planets.Sun, Planets.SouthNode, orbits);
Console.WriteLine(aspect);
// Output: None
```

### You can load orbits from json
````C#
using SharpAstrology.DataModels;
using SharpAstrology.Enums;
using SharpAstrology.Ephemerides;
using SharpAstrology.ExtensionMethods;
using SharpAstrology.Utility;


var date = new DateTime(1988, 9, 4, 1, 15, 0, DateTimeKind.Utc);
using var eph = new SwissEphemeridesService("[PATH_TO_EPHEMERIDES_FILES]")
    .CreateContext();

// create chart
var chart = new AstrologyChart(date, eph);

// Define orbits with json string
var orbitBuilder = OrbitBuilder.FromJsonString("""
{
    "conjunction": {
        "sun": 10,
        "southnode": 8,
        "moon": 10
    }
}
""");
var orbits = orbitBuilder.Build();

Console.WriteLine(chart.AspectBetween(Planets.Sun, Planets.SouthNode, orbits));
//Output: Conjunction

Console.WriteLine(chart.AspectBetween(Planets.Moon, Planets.SouthNode, orbits));
//Moon: None

// Will throw NotSupportedException, because Jupiter is not given in orbit table.
Console.WriteLine(chart.AspectBetween(Planets.Jupiter, Planets.Sun, orbits));
````
#### The structure of the json needs to look like this:
```json
{
  "aspect": {
    "planet": 123,
    "planet": 123,
    ...
  },
  "aspect": {
    "planet": 123,
    "planet": 123,
    ...
  },
  ...
}
```
#### The mapping to the enums is listed in the table below:
|           Planets | json      |             Aspects | json        |
|------------------:|:----------|--------------------:|:------------|
|       Planets.Sun | sun       | Aspects.Conjunction | conjunction |
|      Planets.Moon | moon      |  Aspects.Opposition | opposition  |
|   Planets.Mercury | mercury   |      Aspects.Square | square      |
|     Planets.Venus | venus     |       Aspects.Trine | trine       |
|      Planets.Mars | mars      | Aspects.SemiSextile | semisextile |
|   Planets.Jupiter | jupiter   |    Aspects.Quincunx | quincunx    |
|    Planets.Saturn | saturn    |    Aspects.Quintile | quintile    |
|    Planets.Uranus | uranus    |                     |             |
|   Planets.Neptune | neptune   |                     |             |
|     Planets.Pluto | pluto     |                     |             |
| Planets.NorthNode | northnode |                     |             |
| Planets.SouthNode | southnode |                     |             |
|    Planets.Chiron | chiron    |                     |             |
|     Planets.Earth | earth     |                     |             |

## Which zodiac do the decanates use?
`DecanateOf` divides the division a planet, an axis or a house cusp stands in into three parts. It
counts in the zodiac of the chart. A chart stands in exactly one zodiac and `chart.CalculationMode`
says which one, so a chart built with `EphCalculationMode.Sidereal` gives thirds of a constellation
and a tropically calculated chart gives thirds of a sign.

```C#
using SharpAstrology.ExtensionMethods;
...
var chart = new AstrologyChart(pointInTime, eph, 51.0, 11.0, mode: EphCalculationMode.Sidereal);
var decanate = chart.DecanateOf(Planets.Sun);
```

## Visualizing a chart

SharpAstrology offers a package that allows you to visualize your AstrologyChart via a Blazor component. 
See the project [SharpAstrology.West.BlazorComponents](https://github.com/CReizner/SharpAstrology.West.BlazorComponents).

![Astro Chart](https://github.com/CReizner/SharpAstrology.West.BlazorComponents/blob/main/.github_assets/astro_chart_with_transits.png)
