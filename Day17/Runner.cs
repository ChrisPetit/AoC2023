namespace Day17;

public static class Runner
{
    public static void RunDay17()
    {
        var input = File.ReadAllLines("input/Day17.txt");
        var pathFinder = new PathFinder(input);
        var path = pathFinder.FindPath();
        var totalHeatLoss = path.Sum(n =>n!.HeatLoss);
        Console.WriteLine($"The heat loss is {totalHeatLoss}");
    }
}