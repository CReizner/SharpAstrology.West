using SharpAstrology.Enums;
using SharpAstrology.ExtensionMethods;
using SharpAstrology.Interfaces;
using SharpAstrology.Utility;

namespace SharpAstrology.DataModels;

public sealed class WesternAstrologyChart
{
    public Dictionary<Planets, PlanetPosition> PlanetsPositions { get; set; }
    public Dictionary<Enum, Dictionary<Enum, double>> Angles { get; set; }
    public Dictionary<Enum, Dictionary<Enum, Aspects>> Aspects { get; set; }
    public Dictionary<Planets, Houses> PlanetsHousePosition { get; set; }
    public HousePosition HousePositions { get; set; }
    public bool HousesAvailable { get; set; }
    
    
    public static WesternAstrologyChart FromPointInTime(DateTime pointInTime, IEphemerides eph, AstrologyChartOptions? options = null)
    {
        options ??= new AstrologyChartOptions();
        var aspectCalculator = new AspectCalculator(options.Orbits);
        var planetPositions = eph.PlanetsPosition(options.IncludeObjects, pointInTime);
        var planetsLongitudes = planetPositions.ToDictionary(
            x => x.Key as Enum,
            x => x.Value.Longitude);
        
        return new WesternAstrologyChart()
        {
            PlanetsPositions = planetPositions,
            Angles = AstrologyUtility.AnglesBetween(planetsLongitudes, planetsLongitudes),
            Aspects = aspectCalculator.RelationBetween(planetsLongitudes, planetsLongitudes),
            HousesAvailable = false
        };
    }

    public static WesternAstrologyChart FromPointInTimeAndSpace(DateTime pointInTime, double latitude, double longitude,
        IEphemerides eph, AstrologyChartOptions? options = null)
    {
        var chartModel = FromPointInTime(pointInTime, eph, options);
        chartModel.HousePositions = eph
            .HouseCuspPositions(pointInTime, latitude, longitude);
        chartModel.PlanetsHousePosition = AstrologyUtility.PlanetsHousePosition(chartModel.PlanetsPositions,
            chartModel.HousePositions.HouseCusps);
        chartModel.HousesAvailable = true;

        return chartModel;
    }
    
}