namespace Day17.Tests;

public class PathFinderTests
{
    [Fact]
    public void TestFindShortestPath()
    {
        var input = new[]
        {
            "2413432311323",
            "3215453535623",
            "3255245654254",
            "3446585845452",
            "4546657867536",
            "1438598798454",
            "4457876987766",
            "3637877979653",
            "4654967986887",
            "4564679986453",
            "1224686865563",
            "2546548887735",
            "4322674655533"
        };
        
        var pathFinder = new PathFinder(input);
        var path = pathFinder.FindPath();
        var totalHeatLoss = path.Sum(n =>n!.HeatLoss);
        

        Assert.Equal(102, totalHeatLoss);
    }
}

