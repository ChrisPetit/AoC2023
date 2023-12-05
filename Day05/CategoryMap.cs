namespace Day05
{
    public class CategoryMap
    {
        private class RangeMapping
        {
            public long SourceStart { get; init; }
            public long DestinationStart { get; init; }
            public long RangeLength { get; init; }
        }

        private readonly List<RangeMapping> _mappings;

        public CategoryMap(string mapData)
        {
            _mappings = new List<RangeMapping>();

            var lines = mapData.Split('\n');
            foreach (var line in lines)
            {
                var parts = line.Split(' ').Where(s => long.TryParse(s, out _)).ToList();
                if (parts.Count == 3)
                {
                    _mappings.Add(new RangeMapping
                    {
                        DestinationStart = long.Parse(parts[0]),
                        SourceStart = long.Parse(parts[1]),
                        RangeLength = long.Parse(parts[2])
                    });
                }
            }
        }

        public long GetMappedValue(long source)
        {
            var lockObject = new object(); // Create a lock object for thread safety
            var mappedValue = source; // Initialize the mapped value

            Parallel.ForEach(_mappings, (mapping, state) =>
            {
                if (source < mapping.SourceStart || source >= mapping.SourceStart + mapping.RangeLength) return;
                lock (lockObject) // Lock the critical section to ensure thread safety
                {
                    mappedValue = mapping.DestinationStart + (source - mapping.SourceStart);
                    state.Break(); // Exit the loop once a mapping is found
                }
            });

            return mappedValue; // Return the mapped value
        }
    }
}