using System;
using System.Collections.Generic;
using SharpAstrology.Enums;
using SharpAstrology.Utility;

namespace SharpAstrology.Definitions;

/// <summary>
/// Contains static lists defaults for astrological related subjects.
/// </summary>
public static class WesternAstrologyDefaults
{
    /// <summary>
    /// The major and minor celestial bodies used by the default western astrology system.
    /// </summary>
    public static readonly Planets[] WesternDefaultPlanets = [
        Planets.Sun, Planets.Earth, Planets.Mars, Planets.Mercury,
        Planets.Venus, Planets.Moon, Planets.Jupiter, Planets.Saturn, 
        Planets.Uranus, Planets.Neptune, Planets.Pluto, 
        Planets.NorthNode, Planets.SouthNode
    ];

    public static readonly Dictionary<Enum, Dictionary<Aspects, int>> PlanetsDefaultOrbits =
        OrbitBuilder.WithWesternDefaultOrbits().ToOrbits();

    // public static readonly Dictionary<Aspects, int> DefaultOrbits = new()
    // {
    //     [Aspects.Conjunction] = 6,
    //     [Aspects.Opposition] = 6,
    //     [Aspects.Square] = 6,
    //     [Aspects.Sextile] = 6,
    //     [Aspects.Trine] = 6,
    //     [Aspects.SemiSextile] = 3,
    //     [Aspects.Quincunx] = 3,
    //     [Aspects.Quintile] = 3,
    // };
    //
    // public static readonly Dictionary<Enum, Dictionary<Aspects, int>> PlanetsDefaultOrbits = new()
    // {
    //     [Planets.Sun] = new()
    //     {
    //         [Aspects.Conjunction] = 8,
    //         [Aspects.Opposition] = 8,
    //         [Aspects.Square] = 8,
    //         [Aspects.Sextile] = 8,
    //         [Aspects.Trine] = 8,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Planets.Moon] = new()
    //     {
    //         [Aspects.Conjunction] = 10,
    //         [Aspects.Opposition] = 10,
    //         [Aspects.Square] = 10,
    //         [Aspects.Sextile] = 10,
    //         [Aspects.Trine] = 10,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Planets.Mercury] = DefaultOrbits,
    //     [Planets.Venus] = DefaultOrbits,
    //     [Planets.Mars] = DefaultOrbits,
    //     [Planets.Jupiter] = DefaultOrbits,
    //     [Planets.Saturn] = DefaultOrbits,
    //     [Planets.Uranus] = DefaultOrbits,
    //     [Planets.Neptune] = DefaultOrbits,
    //     [Planets.Pluto] = DefaultOrbits,
    //     [Planets.NorthNode] = DefaultOrbits,
    //     [Planets.SouthNode] = DefaultOrbits,
    //     [Planets.Chiron] = DefaultOrbits,
    //     [Planets.Earth] = new()
    //     {
    //         [Aspects.Conjunction] = 8,
    //         [Aspects.Opposition] = 8,
    //         [Aspects.Square] = 8,
    //         [Aspects.Sextile] = 8,
    //         [Aspects.Trine] = 8,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Cross.Asc] = new()
    //     {
    //         [Aspects.Conjunction] = 10,
    //         [Aspects.Opposition] = 10,
    //         [Aspects.Square] = 10,
    //         [Aspects.Sextile] = 10,
    //         [Aspects.Trine] = 10,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Cross.Ic] = new()
    //     {
    //         [Aspects.Conjunction] = 10,
    //         [Aspects.Opposition] = 10,
    //         [Aspects.Square] = 10,
    //         [Aspects.Sextile] = 10,
    //         [Aspects.Trine] = 10,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Cross.Dc] = new()
    //     {
    //         [Aspects.Conjunction] = 10,
    //         [Aspects.Opposition] = 10,
    //         [Aspects.Square] = 10,
    //         [Aspects.Sextile] = 10,
    //         [Aspects.Trine] = 10,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Cross.Mc] = new()
    //     {
    //         [Aspects.Conjunction] = 10,
    //         [Aspects.Opposition] = 10,
    //         [Aspects.Square] = 10,
    //         [Aspects.Sextile] = 10,
    //         [Aspects.Trine] = 10,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     },
    //     [Cross.Vertex] = new()
    //     {
    //         [Aspects.Conjunction] = 8,
    //         [Aspects.Opposition] = 8,
    //         [Aspects.Square] = 8,
    //         [Aspects.Sextile] = 8,
    //         [Aspects.Trine] = 8,
    //         [Aspects.SemiSextile] = 3,
    //         [Aspects.Quincunx] = 3,
    //         [Aspects.Quintile] = 3,
    //     }
    // };


}