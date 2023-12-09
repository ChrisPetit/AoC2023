namespace Day09;

public class Runner
{
    public static void RunDay09()
    {
        var input = File.ReadLines("input/Day09.txt").ToList();
        var result = OasisAndSandInstabilitySensor.PredictionOfNextValues(input);
        Console.WriteLine($"Result: {result.predicion}");
        Console.WriteLine($"Result: {result.backwardPredicion}");
    }
}