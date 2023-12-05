using Day05;

namespace Day05Tests;

public class DataParserTests
{
    [Fact]
    public void ParseSeeds_ReturnsCorrectSeedList()
    {
        var seedData = "79 14 55 13"; // Example seed data
        var expectedSeeds = new List<long> { 79, 14, 55, 13 }; // Expected result

        var seeds = DataParser.ParseSeeds(seedData);

        Assert.Equal(expectedSeeds, seeds);
    }

    // Additional tests for ParseMap, ParseSeedRanges, etc.
}