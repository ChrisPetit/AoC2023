namespace Day12;

public static class HotSpringsSolver
{
    private static readonly Dictionary<(string, string, int), long> MemoizationCache = new();
    
    public static long CountTotalArrangements(IEnumerable<string> rows)
    {
        return (from row in rows
            select row.Split(' ')
            into parts
            let conditions = parts[0]
            let groups = parts[1].Split(',').Select(int.Parse).ToArray()
            select CountArrangements(conditions, groups)).Sum();
    }

    public static long UnFoldAndCountTotalArrangements(string[] rows)
    {
        var unfoldedRows = new string[rows.Length];
        for (var i = 0; i < rows.Length; i++)
        {
            unfoldedRows[i] = UnfoldedRow(rows[i]);
        }
        return CountTotalArrangements(unfoldedRows);
    }

    
    public static string UnfoldedRow(string row)
    {
        var parts = row.Split(' ');
        var conditions = parts[0];
        var groups = parts[1];

        var unfoldedConditions = string.Join("?", Enumerable.Repeat(conditions, 5));
        var unfoldedGroups = string.Join(",", Enumerable.Repeat(groups, 5));

        return unfoldedConditions + " " + unfoldedGroups;
    }

    public static long CountArrangements(string condition, int[] groups, int currentGroup = 0)
    {
        var groupsKey = string.Join(",", groups);
        var cacheKey = (condition, groupsKey, currentGroup);

        if (MemoizationCache.TryGetValue(cacheKey, out var cachedResult))
        {
            return cachedResult;
        }
        
        while (true)
        {
            // Base cases
            if (string.IsNullOrEmpty(condition))
            {
                if (currentGroup > 0)
                {
                    return (groups.Length == 1 && currentGroup == groups[0]) ? 1 : 0;
                }

                return groups.Length == 0 ? 1 : 0;
            }

            if (currentGroup > 0 && (groups.Length == 0 || currentGroup > groups[0]))
            {
                // Current group is too long
                return 0;
            }

            switch (condition[0])
            {
                // Recursive cases
                // If a group has started
                case '.' when currentGroup > 0:
                {
                    if (currentGroup != groups[0]) return 0;
                    condition = condition[1..];
                    groups = groups.Skip(1).ToArray();
                    currentGroup = 0;
                    continue;
                }
                case '.':
                    condition = condition[1..];
                    currentGroup = 0;
                    continue;
                case '#':
                    // Increment current group length
                    condition = condition[1..];
                    currentGroup += 1;
                    continue;
                default:
                {
                    // Current location can be operational or broken
                    var count = 0L;
                    if (groups.Length == 0 || currentGroup == groups[0])
                    {
                        // Current location must be operational
                        count += CountArrangements(condition[1..], groups.Skip(1).ToArray());
                    }
                    else
                    {
                        // Current location must be broken
                        if (currentGroup > 0)
                        {
                            count += CountArrangements(condition[1..], groups, currentGroup + 1);
                        }
                        else
                        {
                            // Current group hasn't started, it doesn't have to start now
                            count += CountArrangements(condition[1..], groups, currentGroup + 1);
                            count += CountArrangements(condition[1..], groups, currentGroup);
                        }
                    }
                    
                    MemoizationCache[cacheKey] = count;
                    return count;
                }
            }
        }
    }
}

