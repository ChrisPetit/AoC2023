using Day05;

namespace Day05Tests;

public class GardenerHelperTests
{
    [Fact]
    public void FindLowestLocation_ReturnsCorrectLocation()
    {
        // Example seed data based on the assignment
        var seeds = new List<long> { 79, 14, 55, 13 };

        // Creating CategoryMaps for each stage
        var seedToSoilMap = new CategoryMap("50 98 2\n52 50 48");
        var soilToFertilizerMap = new CategoryMap("0 15 37\n37 52 2\n39 0 15");
        var fertilizerToWaterMap = new CategoryMap("49 53 8\n0 11 42\n42 0 7\n57 7 4");
        var waterToLightMap = new CategoryMap("88 18 7\n18 25 70");
        var lightToTemperatureMap = new CategoryMap("45 77 23\n81 45 19\n68 64 13");
        var temperatureToHumidityMap = new CategoryMap("0 69 1\n1 0 69");
        var humidityToLocationMap = new CategoryMap("60 56 37\n56 93 4");

        // Create a GardenerHelper instance with the maps
        var gardenerHelper = new GardenerHelper(
            seeds,
            seedToSoilMap,
            soilToFertilizerMap,
            fertilizerToWaterMap,
            waterToLightMap,
            lightToTemperatureMap,
            temperatureToHumidityMap,
            humidityToLocationMap);


        const long expectedLowestLocation = 35;

        // Act
        var result = gardenerHelper.FindLowestLocation();

        // Assert
        Assert.Equal(expectedLowestLocation, result);
    }
}