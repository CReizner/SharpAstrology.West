using System.Diagnostics;
using SharpAstrology.Enums;
using SharpAstrology.DataModels;
using SharpAstrology.Definitions;
using SharpAstrology.Exceptions;
using SharpAstrology.Utility;
using Orbits = System.Collections.Generic.Dictionary<SharpAstrology.Enums.Aspects, System.Collections.Generic.Dictionary<SharpAstrology.Enums.Planets, int>>;

namespace SharpAstrology.ExtensionMethods;


public static class WesternAstrologyChartExtensionMethods
{
    public static Decanates DecanateOf(this AstrologyChart chart, Planets planet)
    {
        return (chart.PositionOf(planet).Longitude % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third,
            _ => throw new UnreachableException()
        };
    }
    
    public static Decanates DecanateOf(this AstrologyChart chart, Cross direction)
    {
        if (chart.HousePositions is null) throw new HousesNotAvailableException();
        return (chart.HousePositions.Cross[direction] % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third,
            _ => throw new UnreachableException()
        };
    }
    
    public static Decanates DecanateOf(this AstrologyChart chart, Houses house)
    {
        if (chart.HousePositions is null) throw new HousesNotAvailableException();
        return (chart.HousePositions.HouseCusps[house] % 30) switch
        {
            < 10 => Decanates.First,
            < 20 and >= 10 => Decanates.Second,
            < 30 and >= 20 => Decanates.Third,
            _ => throw new UnreachableException()
        };
    }

    public static Aspects AspectBetween(this AstrologyChart chart, Planets planet1, Planets planet2, Orbits? orbits=null)
    {
        orbits ??= WesternAstrologyDefaults.WesternDefaultOrbits;
        if (planet1 == planet2) return Aspects.None;

        var angle = Math.Abs(AstrologyUtility.DifferenceDegreesSigned(
            chart.PositionOf(planet1).Longitude, chart.PositionOf(planet2).Longitude));
        foreach (var aspect in orbits.Keys)
        {
            var orbs = orbits[aspect];
            if (!orbs.TryGetValue(planet1, out var orb1))
            {
                throw new NotSupportedException($"Object {planet1} not supported in orbit table for aspect {aspect}.");
            }
            if (!orbs.TryGetValue(planet2, out var orb2))
            {
                throw new NotSupportedException($"Object {planet2} not supported in orbit table for aspect {aspect}.");
            }
            var maxOrbit = Math.Max(orb1, orb2);
            var deg = aspect.ToAngle();
            var inAspect = deg - maxOrbit <= angle && angle <= deg + maxOrbit;
            if (inAspect) return aspect;
        }
        return Aspects.None;
    }

    public static Dictionary<Planets, Dictionary<Planets, Aspects>> CalculateAspects(this AstrologyChart chart, Orbits? orbits=null)
    {
        orbits ??= WesternAstrologyDefaults.WesternDefaultOrbits;
        var result = new Dictionary<Planets, Dictionary<Planets, Aspects>>();
        foreach (var planet in chart.SupportedObjects)
        {
            result[planet] = chart.SupportedObjects.ToDictionary(p => p, p => chart.AspectBetween(planet, p, orbits));
        }

        return result;
    }

    /// <summary>
    /// Determines the aspect between an object of this chart and an object of another chart,
    /// for example between a natal position and a transit position.
    /// </summary>
    /// <param name="chart">The chart the first object belongs to.</param>
    /// <param name="planet">The object of this chart.</param>
    /// <param name="other">The chart the second object belongs to.</param>
    /// <param name="otherPlanet">The object of the other chart.</param>
    /// <param name="orbits">Orbit table. Defaults to the western default orbits.</param>
    /// <returns>The first matching aspect in the order of the orbit table, or <c>Aspects.None</c>.</returns>
    /// <exception cref="NotSupportedException">
    /// Thrown if one of the objects is missing in the orbit table of an aspect.
    /// </exception>
    public static Aspects AspectBetween(this AstrologyChart chart, Planets planet,
        AstrologyChart other, Planets otherPlanet, Orbits? orbits = null)
    {
        orbits ??= WesternAstrologyDefaults.WesternDefaultOrbits;

        // Two charts are not symmetric, so there is no counterpart call that would
        // catch a negative difference. The unsigned distance folded into [0, 180]
        // detects an aspect regardless of which object leads.
        var angle = Math.Abs(AstrologyUtility.DifferenceDegreesSigned(
            chart.PositionOf(planet).Longitude, other.PositionOf(otherPlanet).Longitude));

        foreach (var aspect in orbits.Keys)
        {
            var orbs = orbits[aspect];
            if (!orbs.TryGetValue(planet, out var orb1))
            {
                throw new NotSupportedException($"Object {planet} not supported in orbit table for aspect {aspect}.");
            }
            if (!orbs.TryGetValue(otherPlanet, out var orb2))
            {
                throw new NotSupportedException($"Object {otherPlanet} not supported in orbit table for aspect {aspect}.");
            }
            var maxOrbit = Math.Max(orb1, orb2);
            var deg = aspect.ToAngle();
            if (deg - maxOrbit <= angle && angle <= deg + maxOrbit) return aspect;
        }
        return Aspects.None;
    }

    /// <summary>
    /// Calculates the aspects between all objects of this chart and all objects of another chart.
    /// The outer key is an object of this chart, the inner key an object of the other chart.
    /// </summary>
    /// <param name="chart">The chart of the outer keys.</param>
    /// <param name="other">The chart of the inner keys.</param>
    /// <param name="orbits">Orbit table. Defaults to the western default orbits.</param>
    /// <remarks>
    /// Unlike <see cref="CalculateAspects"/> the identity pairs are kept. The same object at two
    /// points in time is a meaningful transit and not a trivial conjunction with itself.
    /// </remarks>
    public static Dictionary<Planets, Dictionary<Planets, Aspects>> AspectsBetween(
        this AstrologyChart chart, AstrologyChart other, Orbits? orbits = null)
    {
        orbits ??= WesternAstrologyDefaults.WesternDefaultOrbits;
        var result = new Dictionary<Planets, Dictionary<Planets, Aspects>>();
        foreach (var planet in chart.SupportedObjects)
        {
            result[planet] = other.SupportedObjects
                .ToDictionary(p => p, p => chart.AspectBetween(planet, other, p, orbits));
        }

        return result;
    }
}