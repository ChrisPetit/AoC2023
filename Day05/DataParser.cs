namespace Day05;

public static class DataParser
{
    public static List<long> ParseSeeds(string seedData)
    {
        return seedData
            .Split(' ')
            .Where(s => long.TryParse(s, out _))
            .Select(long.Parse)
            .ToList();
    }
    
    public static CategoryMap ParseMap(string mapData)
    {
        return new CategoryMap(mapData);
    }
}

