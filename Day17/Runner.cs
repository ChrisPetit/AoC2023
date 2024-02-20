namespace Day17;

public static class Runner
{
    public static void RunDay17()
    {
        var input = File.ReadAllLines("input/Day17.txt");
        var pathFinder = new PathFinder(input);
        var heatLoss = pathFinder.MinHeatLossPath();
        Console.WriteLine($"The heat loss is {heatLoss}");

        pathFinder = new PathFinder(input, true);
        heatLoss = pathFinder.MinHeatLossPath();
        Console.WriteLine($"The heat loss is {heatLoss}");
    }
}