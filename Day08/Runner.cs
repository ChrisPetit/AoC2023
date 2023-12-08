namespace Day08;

public static class Runner
{
    public static void RunDay08()
    {
        var input = File.ReadLines("input/Day08.txt").ToList();
        
        var steps = HauntedWastelandNavigator.StepsToReachDestination(input.Skip(2), input[0]);
        Console.WriteLine($"Steps: {steps}");
        
        var simultaneousSteps = HauntedWastelandNavigator.StepsToReachAllDestinations(input.Skip(2), input[0]);
        Console.WriteLine($"Simultaneous Steps: {simultaneousSteps}");
    }
}