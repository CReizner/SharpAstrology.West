using System;
using System.Collections.Generic;
using SharpAstrology.Enums;

namespace SharpAstrology.Definitions;

/// <summary>
/// Contains static lists defaults for astrological related subjects.
/// </summary>
public static class WesternAstrologyDefaults
{
    /// <summary>
    /// The major and minor celestial bodies used by the default western astrology system.
    /// </summary>
    public static readonly Planets[] DefaultPlanets = {
        Planets.Sun, Planets.Earth, Planets.Mars, Planets.Mercury,
        Planets.Venus, Planets.Moon, Planets.Jupiter, Planets.Saturn, 
        Planets.Uranus, Planets.Neptune, Planets.Pluto, 
        Planets.NorthNode
    };
    
    public static Dictionary<Aspects, ushort> DefaultOrbits { get; set; } = new()
    {
        [Aspects.Conjunction] = 6,
        [Aspects.Opposition] = 6,
        [Aspects.Square] = 6,
        [Aspects.Sextile] = 6,
        [Aspects.Trine] = 6,
        [Aspects.SemiSextile] = 3,
        [Aspects.Quincunx] = 3,
        [Aspects.Quintile] = 3,
    };

    public static Dictionary<Enum, Dictionary<Aspects, ushort>> PlanetsDefaultOrbits = new()
    {
        [Planets.Sun] = new()
        {
            [Aspects.Conjunction] = 8,
            [Aspects.Opposition] = 8,
            [Aspects.Square] = 8,
            [Aspects.Sextile] = 8,
            [Aspects.Trine] = 8,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Planets.Moon] = new()
        {
            [Aspects.Conjunction] = 10,
            [Aspects.Opposition] = 10,
            [Aspects.Square] = 10,
            [Aspects.Sextile] = 10,
            [Aspects.Trine] = 10,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Planets.Mercury] = DefaultOrbits,
        [Planets.Venus] = DefaultOrbits,
        [Planets.Mars] = DefaultOrbits,
        [Planets.Jupiter] = DefaultOrbits,
        [Planets.Saturn] = DefaultOrbits,
        [Planets.Uranus] = DefaultOrbits,
        [Planets.Neptune] = DefaultOrbits,
        [Planets.Pluto] = DefaultOrbits,
        [Planets.NorthNode] = DefaultOrbits,
        [Planets.SouthNode] = DefaultOrbits,
        [Planets.Chiron] = DefaultOrbits,
        [Planets.Earth] = new()
        {
            [Aspects.Conjunction] = 8,
            [Aspects.Opposition] = 8,
            [Aspects.Square] = 8,
            [Aspects.Sextile] = 8,
            [Aspects.Trine] = 8,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Cross.Asc] = new()
        {
            [Aspects.Conjunction] = 10,
            [Aspects.Opposition] = 10,
            [Aspects.Square] = 10,
            [Aspects.Sextile] = 10,
            [Aspects.Trine] = 10,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Cross.Ic] = new()
        {
            [Aspects.Conjunction] = 10,
            [Aspects.Opposition] = 10,
            [Aspects.Square] = 10,
            [Aspects.Sextile] = 10,
            [Aspects.Trine] = 10,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Cross.Dc] = new()
        {
            [Aspects.Conjunction] = 10,
            [Aspects.Opposition] = 10,
            [Aspects.Square] = 10,
            [Aspects.Sextile] = 10,
            [Aspects.Trine] = 10,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Cross.Mc] = new()
        {
            [Aspects.Conjunction] = 10,
            [Aspects.Opposition] = 10,
            [Aspects.Square] = 10,
            [Aspects.Sextile] = 10,
            [Aspects.Trine] = 10,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        },
        [Cross.Vertex] = new()
        {
            [Aspects.Conjunction] = 8,
            [Aspects.Opposition] = 8,
            [Aspects.Square] = 8,
            [Aspects.Sextile] = 8,
            [Aspects.Trine] = 8,
            [Aspects.SemiSextile] = 3,
            [Aspects.Quincunx] = 3,
            [Aspects.Quintile] = 3,
        }
    };
        
    public static PlanetStates GetState(Planets planet, ZodiacSigns sign)
    {
        return planet switch
        {
            Planets.Sun when sign is ZodiacSigns.Leo => PlanetStates.Dominion,
            Planets.Sun when sign is ZodiacSigns.Aries => PlanetStates.Exalted,
            Planets.Sun when sign is ZodiacSigns.Aquarius => PlanetStates.Detriment,
            Planets.Sun when sign is ZodiacSigns.Libra => PlanetStates.Fall,
            Planets.Moon when sign is ZodiacSigns.Cancer => PlanetStates.Dominion,
            Planets.Moon when sign is ZodiacSigns.Taurus => PlanetStates.Exalted,
            Planets.Moon when sign is ZodiacSigns.Capricorn => PlanetStates.Detriment,
            Planets.Moon when sign is ZodiacSigns.Scorpio => PlanetStates.Fall,
            Planets.Mercury when sign is ZodiacSigns.Gemini => PlanetStates.Dominion,
            Planets.Mercury when sign is ZodiacSigns.Virgo => PlanetStates.Exalted,
            Planets.Mercury when sign is ZodiacSigns.Sagittarius => PlanetStates.Detriment,
            Planets.Mercury when sign is ZodiacSigns.Pisces => PlanetStates.Fall,
            Planets.Venus when sign is ZodiacSigns.Taurus => PlanetStates.Dominion,
            Planets.Venus when sign is ZodiacSigns.Libra => PlanetStates.Dominion,
            Planets.Venus when sign is ZodiacSigns.Pisces => PlanetStates.Exalted,
            Planets.Venus when sign is ZodiacSigns.Aries or ZodiacSigns.Scorpio => PlanetStates.Detriment,
            Planets.Venus when sign is ZodiacSigns.Virgo => PlanetStates.Fall,
            Planets.Mars when sign is ZodiacSigns.Aries => PlanetStates.Dominion,
            Planets.Mars when sign is ZodiacSigns.Capricorn => PlanetStates.Exalted,
            Planets.Mars when sign is ZodiacSigns.Taurus or ZodiacSigns.Libra => PlanetStates.Detriment,
            Planets.Jupiter when sign is ZodiacSigns.Sagittarius or ZodiacSigns.Pisces => PlanetStates.Dominion,
            Planets.Jupiter when sign is ZodiacSigns.Cancer => PlanetStates.Exalted,
            Planets.Jupiter when sign is ZodiacSigns.Gemini or ZodiacSigns.Virgo => PlanetStates.Detriment,
            Planets.Jupiter when sign is ZodiacSigns.Capricorn => PlanetStates.Fall,
            Planets.Saturn when sign is ZodiacSigns.Capricorn => PlanetStates.Dominion,
            Planets.Saturn when sign is ZodiacSigns.Libra => PlanetStates.Exalted,
            Planets.Saturn when sign is ZodiacSigns.Cancer or ZodiacSigns.Leo => PlanetStates.Detriment,
            Planets.Saturn when sign is ZodiacSigns.Aries => PlanetStates.Fall,
            Planets.Uranus when sign is ZodiacSigns.Aquarius => PlanetStates.Dominion,
            Planets.Uranus when sign is ZodiacSigns.Scorpio => PlanetStates.Exalted,
            Planets.Uranus when sign is ZodiacSigns.Leo => PlanetStates.Detriment,
            Planets.Uranus when sign is ZodiacSigns.Taurus => PlanetStates.Fall,
            Planets.Neptune when sign is ZodiacSigns.Pisces => PlanetStates.Dominion,
            Planets.Neptune when sign is ZodiacSigns.Cancer => PlanetStates.Exalted,
            Planets.Neptune when sign is ZodiacSigns.Virgo => PlanetStates.Detriment,
            Planets.Neptune when sign is ZodiacSigns.Capricorn => PlanetStates.Fall,
            _ => PlanetStates.None
        };
    }
}