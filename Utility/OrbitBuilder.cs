using SharpAstrology.Definitions;
using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

public sealed class OrbitBuilder : IOrbitBuilderLoader, IOrbitBuilder
{
    private Dictionary<Enum, Dictionary<Aspects, int>> _orbits = new();

    public IOrbitBuilder UseDefaults()
    {
        _orbits = WesternAstrologyDefaults.PlanetsDefaultOrbits;
        return this;
    }
    
    public IOrbitBuilder FromJsonString(string text)
    {
        throw new NotImplementedException();
    }

    public IOrbitBuilder SetRule(Planets planet, Aspects aspect, ushort orbit)
    {
        if (_orbits.TryGetValue(planet, out var orbits))
        {
            orbits[aspect] = orbit;
        }
        else
        {
            _orbits[planet] = new Dictionary<Aspects, int>() { [aspect] = orbit };
        }
        return this;
    }
    
    public IOrbitBuilder SetRules(Planets planet, Dictionary<Aspects, ushort> orbits)
    {
        foreach (var (aspect, orbit) in orbits)
        {
            SetRule(planet, aspect, orbit);
        }

        return this;
    }

    public IOrbitBuilder SetRule(Planets planet, Aspects aspect, int orbit)
    {
        throw new NotImplementedException();
    }

    public IOrbitBuilder SetRules(Planets planet, Dictionary<Aspects, int> orbits)
    {
        throw new NotImplementedException();
    }

    public Dictionary<Enum, Dictionary<Aspects, int>> ToOrbits() => _orbits;

    private Planets PlanetFromString(string text)
    {
        return text.ToLower() switch
        {
            "sun" => Planets.Sun,
            "moon" => Planets.Moon,
            "mercury" => Planets.Mercury,
            "venus" => Planets.Venus,
            "mars" => Planets.Mars,
            "jupiter" => Planets.Jupiter,
            "saturn" => Planets.Saturn,
            "uranus" => Planets.Uranus,
            "neptune" => Planets.Neptune,
            "pluto" => Planets.Pluto,
            "north node" => Planets.NorthNode,
            "south node" => Planets.SouthNode,
            "chiron" => Planets.Chiron,
            "earth" => Planets.Earth,
            _ => throw new ArgumentOutOfRangeException(nameof(text), text, "Planet unknown.")
        };
    }
}

public interface IOrbitBuilder
{
    public IOrbitBuilder SetRule(Planets planet, Aspects aspect, int orbit);
    public IOrbitBuilder SetRules(Planets planet, Dictionary<Aspects, int> orbits);
    public Dictionary<Enum, Dictionary<Aspects, int>> ToOrbits();
}

public interface IOrbitBuilderLoader
{
    public IOrbitBuilder UseDefaults();
    public IOrbitBuilder FromJsonString(string text);
}
