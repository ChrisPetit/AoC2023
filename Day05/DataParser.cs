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
    
    public static List<long> ParseSeedRanges(string seedData)
    {
        var parts = seedData.Split(' ').Where(s => long.TryParse(s, out _)).Select(long.Parse).ToArray();
        var seeds = new List<long>();

        for (var i = 0; i < parts.Length; i += 2)
        {
            var start = parts[i];
            var length = parts[i + 1];
            for (long j = 0; j < length; j++)
            {
                seeds.Add(start + j);
            }
        }

        return seeds;
    }


    public static CategoryMap ParseMap(string mapData)
    {
        return new CategoryMap(mapData);
    }
}

