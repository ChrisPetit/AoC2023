using System.Diagnostics;

namespace Day05;

public static class Runner
{
    public static void RunDay05()
    {
        var input = File.ReadAllText("input/Day05.txt");

        var sections = input.Split("\n\n");
        var seeds = DataParser.ParseSeeds(sections[0]);
        var allMaps = new List<CategoryMap>(){
            DataParser.ParseMap(sections[1]),
            DataParser.ParseMap(sections[2]),
            DataParser.ParseMap(sections[3]),
            DataParser.ParseMap(sections[4]),
            DataParser.ParseMap(sections[5]),
            DataParser.ParseMap(sections[6]),
            DataParser.ParseMap(sections[7])
         };

        var helper = new GardenerHelper(
            seeds,
            allMaps);

        var result1 = helper.FindLowestLocation();
        var result2 = helper.FindLowestLocationFromRanges();
        Console.WriteLine($"Lowest location number (Part 1): {result1}");
        Console.WriteLine($"Lowest location number (Part 2): {result2}");
    }
}