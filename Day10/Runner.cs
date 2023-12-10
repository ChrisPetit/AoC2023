namespace Day10;

public class Runner
{
    public static void RunDay10()
    {
        var input = File.ReadAllLines("input/Day10.txt");
        var pipeMazeSolver = new PipeMazeSolver(input);
        var result = pipeMazeSolver.FindFarthestDistance();
        Console.WriteLine($"Farthest Distance: {result}");
        var result2 = pipeMazeSolver.CalculateEnclosedArea();
        Console.WriteLine($"Enclosed Area: {result2}");
    }
}