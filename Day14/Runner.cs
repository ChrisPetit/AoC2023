namespace Day14;

public static class Runner
{
    public static void RunDay14()
    {
        var input = File.ReadAllLines("input/Day14.txt");
        var dish = new ParabolicReflectorDish(input);
        var result = dish.CalculateLoadOnNorthBeams();
        Console.WriteLine($"The total load on the north support beams is: {result}", result);
        dish = new ParabolicReflectorDish(input);
        dish.PerformCycles(1000000000);  // Perform many cycles
        result = dish.CalculateLoadOnNorthBeams();
        Console.WriteLine($"The total load on the north support beams is after 1000000000 cycles is: {result}", result);

    }
}