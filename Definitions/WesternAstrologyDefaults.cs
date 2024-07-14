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
    public static readonly IReadOnlyList<Planets> WesternDefaultPlanets =  [
        Planets.Sun, Planets.Earth, Planets.Mars, Planets.Mercury,
        Planets.Venus, Planets.Moon, Planets.Jupiter, Planets.Saturn, 
        Planets.Uranus, Planets.Neptune, Planets.Pluto, 
        Planets.NorthNode, Planets.SouthNode
    ];

    internal static readonly Dictionary<Aspects, Dictionary<Planets, int>> WesternDefaultOrbits = new()
    {
        {
            Aspects.Conjunction, new()
            {
                { Planets.Sun, 8 },
                { Planets.Moon, 10 },
                { Planets.Mercury, 6 },
                { Planets.Venus, 6 },
                { Planets.Mars, 6 },
                { Planets.Jupiter, 6 },
                { Planets.Saturn, 6 },
                { Planets.Uranus, 6 },
                { Planets.Neptune, 6 },
                { Planets.Pluto, 6 },
                { Planets.NorthNode, 6 },
                { Planets.SouthNode, 6 },
                { Planets.Chiron, 6 },
                { Planets.Earth, 8 }
            }
        },
        {
            Aspects.Opposition, new()
            {
                { Planets.Sun, 8 },
                { Planets.Moon, 10 },
                { Planets.Mercury, 6 },
                { Planets.Venus, 6 },
                { Planets.Mars, 6 },
                { Planets.Jupiter, 6 },
                { Planets.Saturn, 6 },
                { Planets.Uranus, 6 },
                { Planets.Neptune, 6 },
                { Planets.Pluto, 6 },
                { Planets.NorthNode, 6 },
                { Planets.SouthNode, 6 },
                { Planets.Chiron, 6 },
                { Planets.Earth, 8 }
            }
        },
        {
            Aspects.Square, new()
            {
                { Planets.Sun, 8 },
                { Planets.Moon, 10 },
                { Planets.Mercury, 6 },
                { Planets.Venus, 6 },
                { Planets.Mars, 6 },
                { Planets.Jupiter, 6 },
                { Planets.Saturn, 6 },
                { Planets.Uranus, 6 },
                { Planets.Neptune, 6 },
                { Planets.Pluto, 6 },
                { Planets.NorthNode, 6 },
                { Planets.SouthNode, 6 },
                { Planets.Chiron, 6 },
                { Planets.Earth, 8 }
            }
        },
        {
            Aspects.Sextile, new()
            {
                { Planets.Sun, 8 },
                { Planets.Moon, 10 },
                { Planets.Mercury, 6 },
                { Planets.Venus, 6 },
                { Planets.Mars, 6 },
                { Planets.Jupiter, 6 },
                { Planets.Saturn, 6 },
                { Planets.Uranus, 6 },
                { Planets.Neptune, 6 },
                { Planets.Pluto, 6 },
                { Planets.NorthNode, 6 },
                { Planets.SouthNode, 6 },
                { Planets.Chiron, 6 },
                { Planets.Earth, 8 }
            }
        },
        {
            Aspects.Trine, new()
            {
                { Planets.Sun, 8 },
                { Planets.Moon, 10 },
                { Planets.Mercury, 6 },
                { Planets.Venus, 6 },
                { Planets.Mars, 6 },
                { Planets.Jupiter, 6 },
                { Planets.Saturn, 6 },
                { Planets.Uranus, 6 },
                { Planets.Neptune, 6 },
                { Planets.Pluto, 6 },
                { Planets.NorthNode, 6 },
                { Planets.SouthNode, 6 },
                { Planets.Chiron, 6 },
                { Planets.Earth, 8 }
            }
        },
        {
            Aspects.SemiSextile, new()
            {
                { Planets.Sun, 3 },
                { Planets.Moon, 3 },
                { Planets.Mercury, 3 },
                { Planets.Venus, 3 },
                { Planets.Mars, 3 },
                { Planets.Jupiter, 3 },
                { Planets.Saturn, 3 },
                { Planets.Uranus, 3 },
                { Planets.Neptune, 3 },
                { Planets.Pluto, 3 },
                { Planets.NorthNode, 3 },
                { Planets.SouthNode, 3 },
                { Planets.Chiron, 3 },
                { Planets.Earth, 3 }
            }
        },
        {
            Aspects.Quincunx, new()
            {
                { Planets.Sun, 3 },
                { Planets.Moon, 3 },
                { Planets.Mercury, 3 },
                { Planets.Venus, 3 },
                { Planets.Mars, 3 },
                { Planets.Jupiter, 3 },
                { Planets.Saturn, 3 },
                { Planets.Uranus, 3 },
                { Planets.Neptune, 3 },
                { Planets.Pluto, 3 },
                { Planets.NorthNode, 3 },
                { Planets.SouthNode, 3 },
                { Planets.Chiron, 3 },
                { Planets.Earth, 3 }
            }
        },
        {
            Aspects.Quintile, new()
            {
                { Planets.Sun, 3 },
                { Planets.Moon, 3 },
                { Planets.Mercury, 3 },
                { Planets.Venus, 3 },
                { Planets.Mars, 3 },
                { Planets.Jupiter, 3 },
                { Planets.Saturn, 3 },
                { Planets.Uranus, 3 },
                { Planets.Neptune, 3 },
                { Planets.Pluto, 3 },
                { Planets.NorthNode, 3 },
                { Planets.SouthNode, 3 },
                { Planets.Chiron, 3 },
                { Planets.Earth, 3 }
            }
        }
    };


}

public class ReadOnlyList<T>
{
}