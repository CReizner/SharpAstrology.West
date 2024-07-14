namespace SharpAstrology.Enums;

public enum Aspects
{
    Conjunction,
    Opposition,
    Square,
    Sextile,
    Trine,
    SemiSextile,
    Quincunx,
    Quintile,
    // Biquintile,
    // SemiSquare,
    // SesquiQuadrate,
    None
}

public static class AspectsExtensionMethods
{
    public static double ToAngle(this Aspects aspect)
    {
        return aspect switch
        {
            Aspects.Conjunction => 0,
            Aspects.Opposition => 180,
            Aspects.Square => 90,
            Aspects.Sextile => 60,
            Aspects.Trine => 120,
            Aspects.SemiSextile => 30,
            Aspects.Quincunx => 150,
            Aspects.Quintile => 72,
            _ => throw new NotSupportedException($"Aspect {aspect} not supported in this function.")
        };
    }
}