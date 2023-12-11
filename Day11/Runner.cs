namespace Day11;

public class Runner
{
    public static void RunDay11()
    {
        var input = File.ReadAllLines("input/Day11.txt");
        var galaxyProcessor = new GalaxyProcessor(input);
        var totalShortestPaths = galaxyProcessor.CalculateTotalShortestPaths();
        Console.WriteLine($"Total shortest paths: {totalShortestPaths}");
        
        var galaxyProcessor2 = new GalaxyProcessor(input, 1000000);
        var totalShortestPaths2 = galaxyProcessor2.CalculateTotalShortestPaths();
        Console.WriteLine($"Total shortest paths: {totalShortestPaths2}");
    }
}