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

    public static readonly Dictionary<Aspects, Dictionary<Enum, int>> PlanetsDefaultOrbits =
        OrbitBuilder.WithWesternDefaultOrbits().Build();


}