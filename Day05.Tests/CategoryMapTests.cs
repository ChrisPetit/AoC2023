using Day05;

namespace Day05Tests;

public class CategoryMapTests
{
    [Fact]
    public void CategoryMap_CorrectlyAppliesMappings()
    {
        // Example mapping data for a category map
        const string mapData = "50 98 2\n52 50 48";
        var categoryMap = new CategoryMap(mapData);

        // Test some mappings
        Assert.Equal(50, categoryMap.GetAdjustedValue(98));
        Assert.Equal(52, categoryMap.GetAdjustedValue(50));
    }
}