// using SharpAstrology.DataModels;
// using SharpAstrology.Enums;
// using SharpAstrology.Interfaces;
//
// namespace SharpAstrology.Utility;
//
// public static class AstrologyChartComposer
// {
//     public static AstrologyChart PairCompose(AstrologyChart chart1, AstrologyChart chart2, IEphemerides eph,
//         PairCompositeMethod method = PairCompositeMethod.Midpoint, bool corrected = false)
//     {
//         var planetPositions =
//             _calculateCombinePlanetPositions(chart1.PlanetsPositions, chart2.PlanetsPositions, corrected);
//         var compositeChart = new AstrologyCompositeChart
//         {
//             PlanetsPositions = planetPositions,
//         };
//
//         if (!chart1.HousesAvailable || !chart2.HousesAvailable) return compositeChart;
//         
//         switch (method)
//         {
//             case PairCompositeMethod.Midpoint:
//                 compositeChart.HousePositions = new HousePosition
//                 {
//                     HouseCusps = _calculateCuspsPositionsMidpoint(chart1.HousePositions.HouseCusps, chart2.HousePositions.HouseCusps),
//                     Cross = _calculateCrossPositionsMidpoint(chart1.HousePositions.Cross, chart2.HousePositions.Cross)
//                 };
//                 break;
//             case PairCompositeMethod.ReferencePlace:
//                 break;
//             default:
//                 throw new ArgumentException($"PairComposeMethod unknown: {method}");
//         }
//
//         return compositeChart;
//     }
//     
//     private static Dictionary<Planets, PlanetPosition> _calculateCombinePlanetPositions(
//         IDictionary<Planets, PlanetPosition> positions1, IDictionary<Planets, PlanetPosition> positions2, bool corrected)
//     {
//         var result = new Dictionary<Planets, PlanetPosition>();
//         foreach (var planet in positions1.Keys)
//         {
//             var diff = AstrologyUtility.AngleDifference(
//                 positions1[planet].Longitude, positions2[planet].Longitude);
//             result[planet] = new PlanetPosition { Longitude = AstrologyUtility.SubtractDegree(positions1[planet].Longitude, diff / 2) };
//         }
//
//         if (!corrected) return result;
//         if (Math.Abs(AstrologyUtility.AngleDifference(
//                 result[Planets.Mercury].Longitude,
//                 result[Planets.Sun].Longitude)) >= 50)
//         {
//             result[Planets.Mercury].Longitude = AstrologyUtility.AddDegree(result[Planets.Mercury].Longitude, 180);
//         }
//         if (Math.Abs(AstrologyUtility.AngleDifference(
//                 result[Planets.Venus].Longitude,
//                 result[Planets.Sun].Longitude)) >= 50)
//         {
//             result[Planets.Mercury].Longitude = AstrologyUtility.AddDegree(result[Planets.Mercury].Longitude, 180);
//         }
//
//         return result;
//     }
//
//     private static Dictionary<Houses, double> _calculateCuspsPositionsMidpoint(Dictionary<Houses, double> cusps1, Dictionary<Houses, double> cusps2)
//     {
//         var housePositions = new Dictionary<Houses, double>();
//         foreach (var house in Enum.GetValues<Houses>())
//         {
//             var diff = AstrologyUtility.AngleDifference(cusps1[house], cusps2[house]);
//             housePositions[house] = AstrologyUtility.SubtractDegree(cusps1[house], diff / 2);
//         }
//
//         return housePositions;
//     }
//     
//     private static Dictionary<Cross, double> _calculateCrossPositionsMidpoint(Dictionary<Cross, double> cross1, Dictionary<Cross, double> cross2)
//     {
//         var crossPositions = new Dictionary<Cross, double>();
//         foreach (var direction in Enum.GetValues<Cross>())
//         {
//             var diff = AstrologyUtility.AngleDifference(cross1[direction], cross2[direction]);
//             crossPositions[direction] = AstrologyUtility.SubtractDegree(cross1[direction], diff / 2);
//         }
//
//         return crossPositions;
//     }
//     
// }