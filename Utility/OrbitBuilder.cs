using System.Text.Json;

using SharpAstrology.Definitions;
using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

public sealed class OrbitBuilder : IOrbitBuilder
{
    private Dictionary<Aspects, Dictionary<Enum, int>> _orbits;

    private OrbitBuilder(Dictionary<Aspects, Dictionary<Enum, int>> orbits)
    {
        _orbits = orbits;
    }

    public static IOrbitBuilder Empty() => new OrbitBuilder(new());
    
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
        var result = new Dictionary<Aspects, Dictionary<Enum, int>>();
        using var doc = JsonDocument.Parse(text);
        var root = doc.RootElement;
        foreach (var property in root.EnumerateObject())
        {
            var aspect = property.Name switch
            {
                "conjunction" => Aspects.Conjunction,
                "opposition" => Aspects.Opposition,
                "square" => Aspects.Square,
                "sextile" => Aspects.Sextile,
                "trine" => Aspects.Trine,
                "semisextile" => Aspects.SemiSextile,
                "quincunx" => Aspects.Quincunx,
                "quintile" => Aspects.Quintile,
                _ => throw new NotSupportedException($"Aspect {property.Name} is not supported in json parser.")
            };
            var orbitDictionary = new Dictionary<Enum, int>();
            foreach (var objectProperty in property.Value.EnumerateObject())
            {
                Enum obj = objectProperty.Name switch
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
                    "asc" => Cross.Asc,
                    "ic" => Cross.Ic,
                    "dc" => Cross.Dc,
                    "mc" => Cross.Mc,
                    "vertex" => Cross.Vertex,
                    _ => throw new NotSupportedException($"Object {objectProperty.Name} is not supported in json parser.")
                };
                orbitDictionary[obj] = objectProperty.Value.GetInt32();
            }
            result[aspect] = orbitDictionary;
        }
    
        return new OrbitBuilder(result);
    }

    public IOrbitBuilder SetRule(Aspects aspect, Enum planet, int orbit)
    {
        if (_orbits.TryGetValue(aspect, out var orbits))
        {
            orbits[planet] = orbit;
        }
        else
        {
            _orbits[aspect] = new Dictionary<Enum, int>() { [planet] = orbit };
        }
        return this;
    }
    
    public IOrbitBuilder SetRules(Aspects aspect, Dictionary<Enum, int> orbits)
    {
        foreach (var (p, orbit) in orbits)
        {
            SetRule(aspect, p, orbit);
        }
        
        return this;
    }

    public Dictionary<Aspects, Dictionary<Enum, int>> Build() => _orbits.ToDictionary();
}

public interface IOrbitBuilder
{
    public IOrbitBuilder SetRule(Aspects aspect, Enum planet, int orbit);
    public IOrbitBuilder SetRules(Aspects aspect, Dictionary<Enum, int> orbits);
    public Dictionary<Aspects, Dictionary<Enum, int>> Build();
}
