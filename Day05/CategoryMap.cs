namespace Day05;

public class CategoryMap
{
    private readonly List<(long from, long to, long adjustment)> _mappings;

    public CategoryMap(string mapData)
    {
        _mappings = new List<(long from, long to, long adjustment)>();
        var lines = mapData.Split('\n');
        foreach (var line in lines)
        {
            var parts = line.Split(' ').Where(s => long.TryParse(s, out _)).ToArray();
            if (parts.Length == 3)
            {
                _mappings.Add((long.Parse(parts[1]), long.Parse(parts[1]) + long.Parse(parts[2]) - 1,
                    long.Parse(parts[0]) - long.Parse(parts[1])));
            }
        }

        // Sort the mappings based on the 'from' value
        _mappings = _mappings.OrderBy(x => x.from).ToList();
    }

    public long GetAdjustedValue(long value)
    {
        foreach (var (from, to, adjustment) in _mappings)
        {
            if (value >= from && value <= to)
            {
                return value + adjustment;
            }
        }
        return value;
    }
    
    public List<(long from, long to, long adjustment)> GetMappings()
    {
        return _mappings;
    }
}
