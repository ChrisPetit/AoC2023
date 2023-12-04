namespace Day02;

public static class Runner
{
    public static void RunDay02()
    {
        var input = File.ReadAllLines("input/day02.txt");
        var result = GameAnalyzer.CalculatePossibleGames(input.ToList(), 12, 13, 14);
        Console.WriteLine(result);

        var result2 = GameCalculator.CalculateTotalPower(input.ToList());
        Console.WriteLine(result2);
    }
}