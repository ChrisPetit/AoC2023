namespace Day03;

public class EngineSchematicAnalyzer
{
    public static int SumOfPartNumbers(string schematic)
    {
        var sum = 0;
        var lines = schematic.Split('\n');
        var visited = new bool[lines.Length, lines[0].Length];

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (!char.IsDigit(lines[i][j]) || visited[i, j]) continue;
                var (number, isAdjacent) = ExtractNumberAndCheckAdjacency(lines, i, j, visited);
                if (isAdjacent)
                {
                    sum += number;
                }
            }
        }

        return sum;
    }

    private static (int number, bool isAdjacent) ExtractNumberAndCheckAdjacency(string[] lines, int row, int col, bool[,] visited)
    {
        var number = 0;
        var isAdjacent = false;

        while (col < lines[row].Length && char.IsDigit(lines[row][col]))
        {
            number = number * 10 + (lines[row][col] - '0');
            visited[row, col] = true;
            isAdjacent |= IsSymbolAdjacent(lines, row, col);
            col++;
        }

        return (number, isAdjacent);
    }

    private static bool IsSymbolAdjacent(IReadOnlyList<string> lines, int row, int col)
    {
        int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

        for (var i = 0; i < 8; i++)
        {
            var newRow = row + dx[i];
            var newCol = col + dy[i];

            if (newRow < 0 || newRow >= lines.Count || newCol < 0 || newCol >= lines[newRow].Length) continue;
            var adjacentChar = lines[newRow][newCol];
            if (adjacentChar != '.' && !char.IsDigit(adjacentChar))
            {
                return true;
            }
        }

        return false;
    }
}
