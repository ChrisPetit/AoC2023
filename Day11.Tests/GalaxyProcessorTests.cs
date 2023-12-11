namespace Day11.Tests;

public class GalaxyProcessorTests
{
    private readonly string[] _exampleInput = new string[]
    {
        "...#......",
        ".......#..",
        "#.........",
        "..........",
        "......#...",
        ".#........",
        ".........#",
        "..........",
        ".......#..",
        "#...#....."
    };

    [Fact]
    public void TestPartOneTotalShortestPaths()
    {
        var processor = new GalaxyProcessor(_exampleInput);
        const int expectedTotalDistance = 374;
        Assert.Equal(expectedTotalDistance, processor.CalculateTotalShortestPaths());
    }

    [Fact]
    public void TestPartTwoTotalShortestPaths()
    {
        var processor = new GalaxyProcessor(_exampleInput, 10);
        long expectedTotalDistance = 1030;
        Assert.Equal(expectedTotalDistance, processor.CalculateTotalShortestPaths());
        
        processor = new GalaxyProcessor(_exampleInput, 100);
        expectedTotalDistance = 8410;
        Assert.Equal(expectedTotalDistance, processor.CalculateTotalShortestPaths());
    }
}