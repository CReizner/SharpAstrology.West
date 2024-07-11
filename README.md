# SharpAstrology.West
${\color{red}This \space package \space is \space still \space experimental. \space Interfaces \space can \space change \space before \space version \space 1.0.0.}$

## SharpAstrology Packages
| Package                                                                                                                | Version | Description                                   | Licence  |
|:-----------------------------------------------------------------------------------------------------------------------|:-------:|:----------------------------------------------|:--------:|
| [SharpAstrology.Base](https://github.com/CReizner/SharpAstrology.Base)                                                 | 0.10.0  | Base library                                  |   MIT    |
| [SharpAstrology.SwissEph](https://github.com/CReizner/SharpAstrology.SwissEph)                                         |  0.2.2  | Ephemerides package based on SwissEphNet      | AGPL-3.0 |
| [SharpAstrology.HumanDesign](https://github.com/CReizner/SharpAstrology.HumanDesign)                                   |  1.1.0  | Extensions for the Human Design system        |   MIT    |
| [SharpAstrology.HumanDesign.BlazorComponents](https://github.com/CReizner/SharpAstrology.HumanDesign.BlazorComponents) |  0.2.1  | Human Design charts as Blazor components      |   MIT    |
| [SharpAstrology.Vedic](https://github.com/CReizner/SharpAstrology.Vedic)                                               |  0.1.0  | Extensions for Vedic astrology systems        |   MIT    |
| [SharpAstrology.West](https://github.com/CReizner/SharpAstrology.West)                                                 |  0.1.0-preview.2  | Extensions for western astrology systems      |   MIT    |
| [SharpAstrology.West.BlazorComponents](https://github.com/CReizner/SharpAstrology.West.BlazorComponents)               |  0.1.0-preview.1  | Western astrology charts as Blazor components |   MIT    |

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
|           Planets | json      |        Cross | json   |             Aspects | json        |
|------------------:|:----------|-------------:|:-------|--------------------:|:------------|
|       Planets.Sun | sun       |    Cross.Asc | asc    | Aspects.Conjunction | conjunction |
|      Planets.Moon | moon      |     Cross.Mc | nc     |  Aspects.Opposition | opposition  |
|   Planets.Mercury | mercury   |     Cross.Ic | ic     |      Aspects.Square | square      |
|     Planets.Venus | venus     |     Cross.Dc | dc     |       Aspects.Trine | trine       |
|      Planets.Mars | mars      | Cross.Vertex | vertex | Aspects.SemiSextile | semisextile |
|   Planets.Jupiter | jupiter   |              |        |    Aspects.Quincunx | quincunx    |
|    Planets.Saturn | saturn    |              |        |    Aspects.Quintile | quintile    |
|    Planets.Uranus | uranus    |              |        |                     |             |
|   Planets.Neptune | neptune   |              |        |                     |             |
|     Planets.Pluto | pluto     |              |        |                     |             |
| Planets.NorthNode | northnode |              |        |                     |             |
| Planets.SouthNode | southnode |              |        |                     |             |
|    Planets.Chiron | chiron    |              |        |                     |             |
|     Planets.Earth | earth     |              |        |                     |             |

## Visualizing a chart

SharpAstrology offers a package that allows you to visualize your AstrologyChart via a Blazor component. 
See the project [SharpAstrology.West.BlazorComponents](https://github.com/CReizner/SharpAstrology.West.BlazorComponents).

![Astro Chart](https://github.com/CReizner/SharpAstrology.West.BlazorComponents/blob/main/.github_assets/astro_chart_with_transits.png)