namespace Day08.Tests;

public class HauntedWastelandNavigatorTests
{
    [Fact]
    public void TestStepsToReachDestination()
    {
        var map = new List<string>
        {
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        };

        var steps = HauntedWastelandNavigator.StepsToReachDestination(map, "LLR");
        Assert.Equal(6, steps);
    }

    [Fact]
    public void TestStepsToReachAllDestinations()
    {
        var map = new List<string>
        {
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)"
        };
        var steps = HauntedWastelandNavigator.StepsToReachAllDestinations(map, "LR");
        Assert.Equal(6, steps);
    }
}
