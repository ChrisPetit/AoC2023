namespace Day03;

public class Runner
{
    public static void RunDay03()
    {
        var input = File.ReadAllText("input/Day03.txt");
        var result = EngineSchematicAnalyzer.SumOfPartNumbers(input);
        Console.WriteLine(result);
        var result2 = EngineSchematicGearRatioAnalyzer.SumOfGearRatios(input);
        Console.WriteLine(result2);
    }
}