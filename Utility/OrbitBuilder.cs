using System.Text.Json;

using SharpAstrology.Definitions;
using SharpAstrology.Enums;

namespace SharpAstrology.Utility;

public sealed class OrbitBuilder : IOrbitBuilder
{
    private Dictionary<Enum, Dictionary<Aspects, int>> _orbits;

    private OrbitBuilder(Dictionary<Enum, Dictionary<Aspects, int>> orbits)
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
        var result = new Dictionary<Enum, Dictionary<Aspects, int>>();
        using var doc = JsonDocument.Parse(text);
        var root = doc.RootElement;
        foreach (var property in root.EnumerateObject())
        {
            Enum obj = property.Name switch
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
                _ => throw new NotSupportedException($"Object {property.Name} is not supported in json parser.")
            };
            var aspectsDictionary = new Dictionary<Aspects, int>();
            foreach (var aspectProperty in property.Value.EnumerateObject())
            {
                var aspect = aspectProperty.Name switch
                {
                    "conjunction" => Aspects.Conjunction,
                    "opposition" => Aspects.Opposition,
                    "square" => Aspects.Square,
                    "sextile" => Aspects.Sextile,
                    "trine" => Aspects.Trine,
                    "semisextile" => Aspects.SemiSextile,
                    "quincunx" => Aspects.Quincunx,
                    "quintile" => Aspects.Quintile,
                    _ => throw new NotSupportedException($"Aspect {aspectProperty.Name} is not supported in json parser.")
                };
                aspectsDictionary[aspect] = aspectProperty.Value.GetInt32();
            }
            result[obj] = aspectsDictionary;
        }

        return new OrbitBuilder(result);
    }

    public IOrbitBuilder SetRule(Planets planet, Aspects aspect, int orbit)
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
    
    public IOrbitBuilder SetRules(Planets planet, Dictionary<Aspects, int> orbits)
    {
        foreach (var (aspect, orbit) in orbits)
        {
            SetRule(planet, aspect, orbit);
        }

        return this;
    }

    public Dictionary<Enum, Dictionary<Aspects, int>> ToOrbits() => _orbits;
}

public interface IOrbitBuilder
{
    public IOrbitBuilder SetRule(Planets planet, Aspects aspect, int orbit);
    public IOrbitBuilder SetRules(Planets planet, Dictionary<Aspects, int> orbits);
    public Dictionary<Enum, Dictionary<Aspects, int>> ToOrbits();
}
