namespace Day05;

public class GardenerHelper
{
    private readonly List<long> _seeds;
    private readonly List<CategoryMap> _allMaps;

    public GardenerHelper(
        List<long> seeds,
        List<CategoryMap> allMaps)
    {
        _seeds = seeds;
        _allMaps = allMaps;
    }

    public long FindLowestLocation()
    {
        return _seeds
            .Select(seed => _allMaps
                .Aggregate(seed, (current, map) => map
                    .GetAdjustedValue(current)))
            .Prepend(long.MaxValue).Min();
    }

    public long FindLowestLocationFromRanges()
    {
        var ranges = new List<(long from, long to)>();
        for (var i = 0; i < _seeds.Count; i += 2)
        {
            var start = _seeds[i];
            var end = start + _seeds[i + 1] - 1;
            ranges.Add((start, end));
        }

        foreach (var map in _allMaps)
        {
            var newRanges = new List<(long from, long to)>();
            foreach (var range in ranges)
            {
                var currentStart = range.from;
                var currentEnd = range.to;

                foreach (var (from, to, adjustment) in map.GetMappings())
                {
                    if (currentEnd < from)
                    {
                        // The current range is entirely before the mapping range
                        newRanges.Add((currentStart, currentEnd));
                        break;
                    }

                    if (currentStart > to)
                    {
                        // The current range is entirely after the mapping range, continue to next mapping
                        continue;
                    }

                    // The current range intersects with the mapping range
                    if (currentStart < from)
                    {
                        newRanges.Add((currentStart, from - 1));
                    }

                    var newStart = Math.Max(currentStart, from) + adjustment;
                    var newEnd = Math.Min(currentEnd, to) + adjustment;
                    newRanges.Add((newStart, newEnd));
                    currentStart = to + 1;

                    if (currentStart > currentEnd)
                    {
                        break;
                    }
                }

                if (currentStart <= currentEnd)
                {
                    newRanges.Add((currentStart, currentEnd));
                }
            }

            ranges = newRanges;
        }

        return ranges.Any() ? ranges.Min(r => r.from) : long.MaxValue;
    }
}