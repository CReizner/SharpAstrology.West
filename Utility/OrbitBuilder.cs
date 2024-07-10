using System.Collections.Frozen;
using System.Text.Json;

using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

public sealed class OrbitBuilder : IOrbitBuilder
{
    private Dictionary<Aspects, Dictionary<Planets, int>> _orbits;

    private OrbitBuilder(Dictionary<Aspects, Dictionary<Planets, int>> orbits)
    {
        _orbits = orbits;
    }

    public static IOrbitBuilder Empty() => new OrbitBuilder(new Dictionary<Aspects, Dictionary<Planets, int>>());
    
    public static IOrbitBuilder WithWesternDefaultOrbits()
    {
        var json = File.ReadAllText(Path.Join(AppContext.BaseDirectory, "Assets", "defaultOrbits.json"));
        return FromJsonString(json);
    }

    public static IOrbitBuilder FromJsonFile(string path)
    {
        var json = File.ReadAllText(path);
        return FromJsonString(json);
    }
    
    public static IOrbitBuilder FromJsonString(string text)
    {
        var result = new Dictionary<Aspects, Dictionary<Planets, int>>();
        using var doc = JsonDocument.Parse(text);
        var root = doc.RootElement;
        foreach (var property in root.EnumerateObject())
        {
            var aspect = ConvertToAspect(property.Name);
            var orbitDictionary = new Dictionary<Planets, int>();
            foreach (var objectProperty in property.Value.EnumerateObject())
            {
                var obj = ConvertToCelestialObject(objectProperty.Name);
                orbitDictionary[obj] = objectProperty.Value.GetInt32();
            }
            result[aspect] = orbitDictionary;
        }
    
        return new OrbitBuilder(result);
    }

    public IOrbitBuilder SetRule(Aspects aspect, Planets planet, int orbit)
    {
        if (_orbits.TryGetValue(aspect, out var orbits))
        {
            orbits[planet] = orbit;
        }
        else
        {
            _orbits[aspect] = new Dictionary<Planets, int>() { [planet] = orbit };
        }
        return this;
    }
    
    /// <summary>
    /// Sets orbit rules for multiple planets within a specific aspect.
    /// </summary>
    /// <param name="aspect">The aspect to set the rules for.</param>
    /// <param name="orbits">A dictionary of planets and their orbits.</param>
    public IOrbitBuilder SetRules(Aspects aspect, Dictionary<Planets, int> orbits)
    {
        foreach (var (p, orbit) in orbits)
        {
            SetRule(aspect, p, orbit);
        }
        
        return this;
    }

    /// <summary>
    /// Sets orbit rules for multiple aspects and planets from a JSON string.
    /// </summary>
    /// <param name="json">The JSON string containing aspect and planet orbits.</param>
    public IOrbitBuilder SetRulesFromJson(string json)
    {
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;
        foreach (var property in root.EnumerateObject())
        {
            var aspect = ConvertToAspect(property.Name);
            var orbitDictionary = new Dictionary<Planets, int>();
            foreach (var objectProperty in property.Value.EnumerateObject())
            {
                var obj = ConvertToCelestialObject(objectProperty.Name);
                orbitDictionary[obj] = objectProperty.Value.GetInt32();
            }
            _orbits[aspect] = orbitDictionary;
        }

        return this;
    }

    /// <summary>
    /// Builds and returns the orbits dictionary.
    /// </summary>
    public Dictionary<Aspects, Dictionary<Planets, int>> Build() => _orbits.ToDictionary();
    
    
    private static Aspects ConvertToAspect(string text) => text switch
    {
        "conjunction" => Aspects.Conjunction,
        "opposition" => Aspects.Opposition,
        "square" => Aspects.Square,
        "sextile" => Aspects.Sextile,
        "trine" => Aspects.Trine,
        "semisextile" => Aspects.SemiSextile,
        "quincunx" => Aspects.Quincunx,
        "quintile" => Aspects.Quintile,
        _ => throw new NotSupportedException($"Aspect {text} is not supported in json parser.")
    };
    
    private static Planets ConvertToCelestialObject(string text) => text switch
    {
        "sun" => Planets.Sun,
        "moon" => Planets.Moon,
        "mercury" => Planets.Mercury,
        "venus" =>  Planets.Venus,
        "mars" => Planets.Mars,
        "jupiter" => Planets.Jupiter,
        "saturn" => Planets.Saturn,
        "uranus" => Planets.Uranus,
        "neptune" => Planets.Neptune,
        "pluto" => Planets.Pluto,
        "northnode" => Planets.NorthNode,
        "southnode" => Planets.SouthNode,
        "chiron" => Planets.Chiron,
        "earth" => Planets.Earth,
        // "asc" => Cross.Asc,
        // "ic" => Cross.Ic,
        // "dc" => Cross.Dc,
        // "mc" => Cross.Mc,
        // "vertex" => Cross.Vertex,
        _ => throw new NotSupportedException($"Object {text} is not supported in json parser.")
    };
}

public interface IOrbitBuilder
{
    public IOrbitBuilder SetRule(Aspects aspect, Planets planet, int orbit);
    public IOrbitBuilder SetRules(Aspects aspect, Dictionary<Planets, int> orbits);
    public IOrbitBuilder SetRulesFromJson(string json);
    public Dictionary<Aspects, Dictionary<Planets, int>> Build();
}
