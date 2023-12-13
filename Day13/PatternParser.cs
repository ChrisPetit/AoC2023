namespace Day13;


public static class PatternParser
{
    public static List<char[,]> ParsePatterns(IEnumerable<string> input)
    {
        var patterns = new List<char[,]>();
        var currentPattern = new List<string>();

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentPattern.Count <= 0) continue;
                patterns.Add(ConvertToCharArray(currentPattern));
                currentPattern.Clear();
            }
            else
            {
                currentPattern.Add(line);
            }
        }

        // Add the last pattern if the input doesn't end with a blank line
        if (currentPattern.Count > 0)
        {
            patterns.Add(ConvertToCharArray(currentPattern));
        }

        return patterns;
    }

    private static char[,] ConvertToCharArray(IReadOnlyList<string> patternLines)
    {
        var rows = patternLines.Count;
        var cols = patternLines[0].Length; // Assuming all lines have equal length
        var charArray = new char[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                charArray[i, j] = patternLines[i][j];
            }
        }

        return charArray;
    }
}
