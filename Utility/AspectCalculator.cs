using System;
using System.Collections.Generic;
using SharpAstrology.Definitions;
using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

/// <summary>
/// Class responsible for calculating astrological aspects between planets based on their positions.
/// It contains information about the orbits to be used.
/// </summary>
public sealed class AspectCalculator
{
    private Dictionary<Enum, Dictionary<Aspects, ushort>> _orbits;

    /// <summary>
    /// Default constructor that initializes the AspectCalculator with default planet orbits.
    /// </summary>
    public AspectCalculator()
    {
        _orbits = WesternAstrologyDefaults.PlanetsDefaultOrbits;
    }
    
    /// <summary>
    /// Constructor that allows customizing planet orbits.
    /// </summary>
    /// <param name="orbits">Custom planet orbits dictionary.</param>
    public AspectCalculator(Dictionary<Enum, Dictionary<Aspects, ushort>> orbits)
    {
        _orbits = orbits;
    }
    
    /// <summary>
    /// Determines the astrological aspect between two celestial objects or points based on their positions.
    /// </summary>
    /// <param name="obj1">The first celestial object.</param>
    /// <param name="longitude1">Longitude of the first celestial object.</param>
    /// <param name="obj2">The second celestial object.</param>
    /// <param name="longitude2">Longitude of the second celestial object.</param>
    /// <returns>The astrological aspect as enum <see cref="SharpAstrology.Enums.Aspects"/> between the two planets.</returns>
    public Aspects RelationBetween(
        Enum obj1, double longitude1, Enum obj2, double longitude2)
    {
        if (obj1.Equals(obj2))
        {
            return Aspects.None;
        }
        var angle = AstrologyUtility.AngleDifference(longitude1, longitude2);
        foreach (var aspect in Enum.GetValues<Aspects>())
        {
            if (aspect == Aspects.None)
            {
                continue;
            }
            if (_isInAspect(angle, aspect, obj1, obj2))
            {
                return aspect;
            }
        }

        return Aspects.None;
    }
    
    /// <summary>
    /// Determines the astrological aspects between two sets of celestial objects or points based on their positions.
    /// </summary>
    /// <param name="objects1">The first set of celestial objects or points and their positions.</param>
    /// <param name="objects2">The second set of celestial objects or points and their positions.</param>
    /// <returns>
    /// A nested dictionary. The first key relates to the first set of objects, the second key to the second set of objects.
    /// </returns>
    public Dictionary<Enum, Dictionary<Enum, Aspects>> RelationBetween(
        Dictionary<Enum, double> objects1, 
        Dictionary<Enum, double> objects2)
    {
        var result = new Dictionary<Enum, Dictionary<Enum, Aspects>>();
        foreach (var (obj1, position1) in objects1)
        {
            var aspects = new Dictionary<Enum, Aspects>();
            foreach (var (obj2, position2) in objects2)
            {
                aspects[obj2] = RelationBetween(obj1, position1, obj2, position2);
            }

            result[obj1] = aspects;
        }

        return result;
    }

    private bool _isInAspect(double angle, Aspects aspect, Enum obj1, Enum obj2)
    {
        var maxOrbit = Math.Max(_orbits[obj1][aspect], _orbits[obj2][aspect]);
        var deg = aspect.ToAngle();
        return deg - maxOrbit <= angle && angle <= deg + maxOrbit;
    }
}