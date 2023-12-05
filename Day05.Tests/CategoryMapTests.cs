using Day05;

namespace Day05Tests;

public class CategoryMapTests
{
    [Fact]
    public void GetMappedValue_ReturnsCorrectMapping()
    {
        var mapData = "50 98 2\n52 50 48"; // Example map data
        var map = new CategoryMap(mapData);

        Assert.Equal(50, map.GetMappedValue(98));
        Assert.Equal(51, map.GetMappedValue(99));
        Assert.Equal(52, map.GetMappedValue(50));
        // Add more assertions as needed
    }

    // Additional tests for edge cases, etc.
}