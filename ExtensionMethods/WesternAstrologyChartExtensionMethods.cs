using SharpAstrology.Enums;
using SharpAstrology.DataModels;
using SharpAstrology.Utility;

namespace SharpAstrology.ExtensionMethods;

public static class WesternAstrologyChartExtensionMethods
{
    public static bool PlanetIsInSign(this WesternAstrologyChart chart,
        Planets planet, ZodiacSigns sign, Decanates? decanate = null)
    {
        var longitude = chart.PlanetsPositions[planet].Longitude;
        if (sign != AstrologyUtility.ZodiacSignOf(longitude)) return false;
        if (decanate is null) return true;
        var deg = longitude % 30;
        return decanate switch
        {
            Decanates.First when deg < 10 => true,
            Decanates.Second when deg >= 10 & deg < 20 => true,
            Decanates.Third when deg >= 20 & deg < 30 => true,
            _ => false
        };
    }

    public static bool RulerIsInSign(this WesternAstrologyChart chart,
        RulerOf ruler, ZodiacSigns sign, Dictionary<ZodiacSigns, Planets> rulership, Decanates? decanate = null)
    {
        if (!chart.HousesAvailable) return false;
        var inSign = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler.ToHouse()]);
        var planet = rulership[inSign];
        return chart.PlanetIsInSign(planet, sign, decanate);
    }

    public static bool PlanetIsInHouse(this WesternAstrologyChart chart, 
        Planets planet, Houses house, bool relativeSign = false)
    {
        if (!chart.HousesAvailable) return false;
        if (!relativeSign) return chart.PlanetsHousePosition[planet] == house;
        var cuspLongitude = chart.HousePositions.HouseCusps[house];
        var planetsLongitude = chart.PlanetsPositions[planet].Longitude;
        return AstrologyUtility.ZodiacSignOf(cuspLongitude) == AstrologyUtility.ZodiacSignOf(planetsLongitude);
    }
    
    public static bool RulerIsInHouse(this WesternAstrologyChart chart, 
        RulerOf ruler, Houses house, Dictionary<ZodiacSigns, Planets> rulership, bool relativeSign = false)
    {
        if (!chart.HousesAvailable) return false;
        var inSign = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler.ToHouse()]);
        var planet = rulership[inSign];
        return chart.PlanetIsInHouse(planet, house, relativeSign);
    }

    public static bool HouseIsInSign(this WesternAstrologyChart chart, 
        Houses house, ZodiacSigns sign, Decanates? decanate = null)
    {
        if (!chart.HousesAvailable) return false;
        var longitude = chart.HousePositions.HouseCusps[house];
        if (sign != AstrologyUtility.ZodiacSignOf(longitude)) return false;
        if (decanate is null) return true;
        var deg = longitude % 30;
        return decanate switch
        {
            Decanates.First when deg < 10 => true,
            Decanates.Second when deg >= 10 & deg < 20 => true,
            Decanates.Third when deg >= 20 & deg < 30 => true,
            _ => false
        };
    }

    public static bool CrossIsInSign(this WesternAstrologyChart chart, 
        Cross direction, ZodiacSigns sign, Decanates? decanate = null)
    {
        if (!chart.HousesAvailable) return false;
        var longitude = chart.HousePositions.Cross[direction];
        if (sign != AstrologyUtility.ZodiacSignOf(longitude)) return false;
        if (decanate is null) return true;
        var deg = longitude % 30;
        return decanate switch
        {
            Decanates.First when deg < 10 => true,
            Decanates.Second when deg >= 10 & deg < 20 => true,
            Decanates.Third when deg >= 20 & deg < 30 => true,
            _ => false
        };
    }
    
    public static Aspects AspectBetween(this WesternAstrologyChart chart, 
        Planets planet, RulerOf ruler, Dictionary<ZodiacSigns, Planets> rulership)
    {
        if (!chart.HousesAvailable) return Aspects.None;
        var inSign = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler.ToHouse()]);
        var planet2 = rulership[inSign];
        return chart.Aspects[planet][planet2];
    }
    
    public static Aspects AspectBetween(this WesternAstrologyChart chart, 
        RulerOf ruler1, RulerOf ruler2, Dictionary<ZodiacSigns, Planets> rulership)
    {
        if (!chart.HousesAvailable) return Aspects.None;
        var inSign1 = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler1.ToHouse()]);
        var inSign2 = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler2.ToHouse()]);
        var planet1 = rulership[inSign1];
        var planet2 = rulership[inSign2];
        return chart.Aspects[planet1][planet2];
    }
    
    public static Aspects AspectBetween(this WesternAstrologyChart chart, 
        RulerOf ruler, Cross direction, Dictionary<ZodiacSigns, Planets> rulership)
    {
        if (!chart.HousesAvailable) return Aspects.None;
        var inSign = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler.ToHouse()]);
        var planet = rulership[inSign];
        return chart.Aspects[planet][direction];
    }
    
    public static Motion PlanetsMotion(this WesternAstrologyChart chart, Planets planet)
    {
        return chart.PlanetsPositions[planet].SpeedLongitude < 0 ? Motion.Retrograde : Motion.Forward;
    }
    
    public static Motion PlanetsMotion(this WesternAstrologyChart chart, RulerOf ruler, Dictionary<ZodiacSigns, Planets> rulership)
    {
        if (!chart.HousesAvailable) throw new ArgumentException("Cannot calculate motion for a ruler, when houses are not given.");
        var inSign = AstrologyUtility.ZodiacSignOf(chart.HousePositions.HouseCusps[ruler.ToHouse()]);
        var planet = rulership[inSign];
        return chart.PlanetsPositions[planet].SpeedLongitude < 0 ? Motion.Retrograde : Motion.Forward;
    }

    public static Houses GetHouseOf(this WesternAstrologyChart chart, Planets planet)
    {
        return AstrologyUtility.HouseOf(chart.PlanetsPositions[planet].Longitude, chart.HousePositions.HouseCusps);
    }
    
    public static Houses GetHouseOf(this WesternAstrologyChart chart, Cross direction)
    {
        if (!chart.HousesAvailable) throw new ArgumentException("Cannot calculate house position, when houses are not available.");
        var longitude = chart.HousePositions.Cross[direction];
        return AstrologyUtility.HouseOf(longitude, chart.HousePositions.HouseCusps);
    }
}