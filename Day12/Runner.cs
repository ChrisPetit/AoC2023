namespace Day12;

public class Runner
{
    public static void RunDay12()
    {
        var input = File.ReadLines("input/Day12.txt").ToArray();
        var result = HotSpringsSolver.CountTotalArrangements(input);

        Console.WriteLine($"sum of those counts: {result}");
        result = HotSpringsSolver.UnFoldAndCountTotalArrangements(input);
        Console.WriteLine($"sum of possible arrangement counts: {result}");
    }
}