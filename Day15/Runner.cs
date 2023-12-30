namespace Day15;

public class Runner
{
    public static void RunDay15()
    {
        var input = File.ReadAllText("input/Day15.txt");
        var result = HASHalgorithm.Run(input);
        Console.WriteLine($"The sum of the results is: {result}");
        var hashMapResult = HASHalgorithm.HASHMAP(input);
        Console.WriteLine($"The sum of the hash map results is: {hashMapResult}");
    }
}