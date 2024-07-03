using SharpAstrology.Enums;
using SharpAstrology.DataModels;
using SharpAstrology.Definitions;
using SharpAstrology.Exceptions;
using SharpAstrology.Utility;
using Orbits = System.Collections.Generic.Dictionary<System.Enum, System.Collections.Generic.Dictionary<SharpAstrology.Enums.Aspects, int>>;

namespace SharpAstrology.ExtensionMethods;


public static class WesternAstrologyChartExtensionMethods
{
    public static Decanates DecanateOf(this AstrologyChart chart, Planets planet)
    {
        return (chart.PositionOf(planet).Longitude % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third
        };
    }
    
    public static Decanates DecanateOf(this AstrologyChart chart, Cross direction)
    {
        if (chart.HousePositions is null) throw new HousesNotAvailableException();
        return (chart.HousePositions.Cross[direction] % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third
        };
    }
    
    public static Decanates DecanateOf(this AstrologyChart chart, Houses house)
    {
        if (chart.HousePositions is null) throw new HousesNotAvailableException();
        return (chart.HousePositions.HouseCusps[house] % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third
        };
    }

    public static Aspects AspectBetween(this AstrologyChart chart, Planets planet1, Planets planet2, Orbits? orbits=null)
    {
        orbits ??= WesternAstrologyDefaults.PlanetsDefaultOrbits;
        if (planet1 == planet2) return Aspects.None;
        var angle = AstrologyUtility.AngleDifference(chart.PositionOf(planet1).Longitude, chart.PositionOf(planet2).Longitude);
        foreach (var aspect in Enum.GetValues<Aspects>())
        {
            if (aspect == Aspects.None) continue;
            var maxOrbit = Math.Max(orbits[planet1][aspect], orbits[planet2][aspect]);
            var deg = aspect.ToAngle();
            var inAspect = deg - maxOrbit <= angle && angle <= deg + maxOrbit;
            if (inAspect) return aspect;
        }
        return Aspects.None;
    }

    public static Dictionary<Planets, Dictionary<Planets, Aspects>> CalculateAspects(this AstrologyChart chart, Orbits? orbits=null)
    {
        orbits ??= WesternAstrologyDefaults.PlanetsDefaultOrbits;
        var result = new Dictionary<Planets, Dictionary<Planets, Aspects>>();
        foreach (var planet in chart.SupportedObjects)
        {
            result[planet] = chart.SupportedObjects.ToDictionary(p => p, p => chart.AspectBetween(p, planet, orbits));
        }

        return result;
    }
}