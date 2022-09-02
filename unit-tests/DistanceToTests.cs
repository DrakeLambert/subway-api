using System;
using Xunit;

namespace SubwayApi.Tests;

public class DistanceToTests
{
    const double MAXIMUM_ERROR_MILES = 0.01;

    [Theory]
    [InlineData(0, 0, 0, 0, 0)]
    [InlineData(40.73005400028978, -73.99106999861966, 40.71880300107709, -74.00019299927328, 0.91)]
    [InlineData(40.76172799961419, -73.98384899986625, 40.68086213682956, -73.97499915116808, 5.61)]
    [InlineData(40.846809998885504, -73.83256900003744, 40.745630001138395, -73.90298400173006, 7.9)]
    public void CalculatesDistance(double lat1, double long1, double lat2, double long2, double expectedDistance)
    {
        var station1 = StationWithPosition(lat1, long1);
        var station2 = StationWithPosition(lat2, long2);

        var actualDistance = station1.DistanceTo(station2);

        var error = Math.Abs(actualDistance - expectedDistance);
        Assert.True(error < MAXIMUM_ERROR_MILES, $"Error maximum is {MAXIMUM_ERROR_MILES}, but got {error}.");
    }

    private static Station StationWithPosition(double latitude, double longitude) => new(string.Empty, string.Empty, new(), string.Empty, latitude, longitude);
}