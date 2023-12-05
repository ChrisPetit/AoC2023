namespace Day03;

public static class EngineSchematicGearRatioAnalyzer
{
    public static int SumOfGearRatios(string schematic)
    {
        var sum = 0;
        var lines = schematic.Split('\n');
        var visited = new bool[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] != '*') continue;
                var adjacentNumbers = new List<int>();
                GetAdjacentNumbers(lines, i, j, visited, adjacentNumbers);

                if (adjacentNumbers.Count == 2)
                {
                    sum += adjacentNumbers[0] * adjacentNumbers[1];
                }
            }
        }

        return sum;
    }

    private static void GetAdjacentNumbers(IReadOnlyList<string> lines, int row, int col, bool[,] visited, ICollection<int> adjacentNumbers)
    {
        // Directions
        var directions = new[] { 
            new[] { -1, -1 }, new[] { -1, 0 }, new[] { -1, 1 },
            new[] { 0, -1 }, new[] { 0, 1 },
            new[] { 1, -1 }, new[] { 1, 0 }, new[] { 1, 1 } };
            

        foreach (var dir in directions)
        {
            var newRow = row + dir[0];
            var newCol = col + dir[1];

            if (newRow < 0 || newRow >= lines.Count || newCol < 0 || newCol >= lines[newRow].Length) continue;
            if (!char.IsDigit(lines[newRow][newCol]) || visited[newRow, newCol]) continue;
            var (number, isAdjacent) = ExtractNumberAndCheckAdjacency(lines, newRow, newCol, visited);
            if (isAdjacent)
            {
                adjacentNumbers.Add(number);
            }
        }
    }

    private static (int number, bool isAdjacent) ExtractNumberAndCheckAdjacency(IReadOnlyList<string> lines, int row, int col, bool[,] visited)
    {
        var number = 0;
        var isAdjacent = false;
        var startCol = col;

        // Move left to find the start of the number
        while (startCol > 0 && char.IsDigit(lines[row][startCol - 1]))
        {
            startCol--;
        }

        // Now extract the number from left to right
        while (startCol < lines[row].Length && char.IsDigit(lines[row][startCol]))
        {
            number = number * 10 + (lines[row][startCol] - '0');
            visited[row, startCol] = true;
            isAdjacent = true; // If we're extracting a number, it's adjacent to the '*'
            startCol++;
        }

        return (number, isAdjacent);
    }
}
