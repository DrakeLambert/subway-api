namespace SubwayApi;

public record Station(
    string Notes,
    string Name,
    Guid Id,
    string Line,
    double Latitude,
    double Longitude
)
{
    public double DistanceTo(Station otherStation)
    {
        const double earthRadiusMiles = 3959;
        const double oneDegree = Math.PI / 180.0;

        var d1 = Latitude * oneDegree;
        var num1 = Longitude * oneDegree;
        var d2 = otherStation.Latitude * oneDegree;
        var num2 = (otherStation.Longitude * oneDegree) - num1;

        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + (Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0));

        return earthRadiusMiles * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
    }
}
