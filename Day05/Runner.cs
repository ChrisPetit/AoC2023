namespace Day05;

public static class Runner
{
    public static void RunDay05()
    {
        var input = File.ReadAllText("input/Day05.txt");

        var sections = input.Split("\n\n");
        var seedToSoilMap = DataParser.ParseMap(sections[1]);
        var soilToFertilizerMap = DataParser.ParseMap(sections[2]);
        var fertilizerToWaterMap = DataParser.ParseMap(sections[3]);
        var waterToLightMap = DataParser.ParseMap(sections[4]);
        var lightToTemperatureMap = DataParser.ParseMap(sections[5]);
        var temperatureToHumidityMap = DataParser.ParseMap(sections[6]);
        var humidityToLocationMap = DataParser.ParseMap(sections[7]);

        // Part 1
        var helperPart1 = new GardenerHelper(
            DataParser.ParseSeeds(sections[0]), // Original seed parsing for Part 1
            seedToSoilMap,
            soilToFertilizerMap,
            fertilizerToWaterMap,
            waterToLightMap,
            lightToTemperatureMap,
            temperatureToHumidityMap,
            humidityToLocationMap);

        var resultPart1 = helperPart1.FindLowestLocation();
        Console.WriteLine($"Lowest location number (Part 1): {resultPart1}");

        // Part 2
        var helperPart2 = new GardenerHelper(
            DataParser.ParseSeedRanges(sections[0]), // New seed range parsing for Part 2
            seedToSoilMap,
            soilToFertilizerMap,
            fertilizerToWaterMap,
            waterToLightMap,
            lightToTemperatureMap,
            temperatureToHumidityMap,
            humidityToLocationMap);

        var resultPart2 = helperPart2.FindLowestLocationFromRanges();
        Console.WriteLine($"Lowest location number (Part 2): {resultPart2}");
    }
}