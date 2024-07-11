# SharpAstrology.West
${\color{red}This \space package \space is \space still \space experimental. \space Interfaces \space can \space change \space before \space version \space 1.0.0.}$

<--PACKAGES-->

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
