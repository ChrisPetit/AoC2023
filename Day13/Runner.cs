namespace Day13;

public static class Runner
{
    public static void RunDay13()
    {
        var input = File.ReadAllLines("input/Day13.txt");
        var patterns = PatternParser.ParsePatterns(input);
        var result = MirrorFinder.FindMirrors(patterns);
        Console.WriteLine($"Number of mirrors: {result}");
        
        result = MirrorFinder.SummarizeReflectionLines(patterns);
        Console.WriteLine($"Number of smudges: {result}");
    }
}