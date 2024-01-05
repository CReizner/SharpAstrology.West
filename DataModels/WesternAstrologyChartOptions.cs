using SharpAstrology.Definitions;
using SharpAstrology.Enums;

namespace SharpAstrology.DataModels;

public sealed class AstrologyChartOptions
{
    public Planets[] IncludeObjects { get; set; } = WesternAstrologyDefaults.DefaultPlanets;
    
    public Planets[] IncludeAspectsTo { get; set; } = [
        Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars,
        Planets.Jupiter, Planets.Saturn, Planets.Uranus, Planets.Neptune, Planets.Pluto
    ];

    public Dictionary<Enum, Dictionary<Aspects, ushort>> Orbits { get; set; } = WesternAstrologyDefaults.PlanetsDefaultOrbits;
}