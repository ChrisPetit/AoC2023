namespace Day13;

public static class MirrorFinder
{
    public static long FindMirrors(List<char[,]> arrayList)
    {
        var result = 0L;
        foreach (var array in arrayList)
        {
            var verticalResult = FindVerticalMirrorLine(array);
            if (verticalResult != -1) result += verticalResult;
            
            var horizontalResult = FindHorizontalMirrorLine(array);
            if (horizontalResult != -1) result += horizontalResult * 100;
        }
        return result;
    }
    
    public static long SummarizeReflectionLines(IEnumerable<char[,]> arrayList)
    {
        return arrayList.Sum(FindAndFixSmudge);
    }

    
    public static long FindVerticalMirrorLine(char[,] array)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        for (var col = 0; col < cols - 1; col++)
        {
            var isMirror = true;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j <= col; j++)
                {
                    if (col + 1 + j >= cols || array[i, col - j] == array[i, col + 1 + j]) continue;
                    isMirror = false;
                    break;
                }
                if (!isMirror) break;
            }
            if (isMirror) return col + 1;
        }
        return -1; // No vertical mirror line found
    }
    
    public static long FindHorizontalMirrorLine(char[,] array)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        for (var row = 0; row < rows - 1; row++)
        {
            var isMirror = true;
            for (var j = 0; j < cols; j++)
            {
                for (var i = 0; i <= row; i++)
                {
                    if (row + 1 + i >= rows || array[row - i, j] == array[row + 1 + i, j]) continue;
                    isMirror = false;
                    break;
                }
                if (!isMirror) break;
            }
            if (isMirror) return row + 1;
        }
        return -1; // No horizontal mirror line found
    }

    public static void FlipCellState(char[,] array, int row, int col)
    {
        array[row, col] = array[row, col] == '#' ? '.' : '#';
    }
    
    public static long FindAndFixSmudge(char[,] array)
    {
        var originalVerticalLine = FindVerticalMirrorLine(array);
        var originalHorizontalLine = FindHorizontalMirrorLine(array);

        for (var row = 0; row < array.GetLength(0); row++)
        {
            for (var col = 0; col < array.GetLength(1); col++)
            {
                FlipCellState(array, row, col);

                var newVerticalLine = FindVerticalMirrorLine(array);
                var newHorizontalLine = FindHorizontalMirrorLine(array);

                // Check if a valid new line different from the original line is found
                if ((newVerticalLine != -1 && newVerticalLine != originalVerticalLine) ||
                    (newHorizontalLine != -1 && newHorizontalLine != originalHorizontalLine))
                {
                    long result = (newVerticalLine != -1) ? newVerticalLine : newHorizontalLine * 100;
                    FlipCellState(array, row, col); // Restore original state
                    return result; // Return the new reflection line
                }

                FlipCellState(array, row, col); // Restore original state
            }
        }

        return -1; // No new valid reflection line found
    }


    public static int FindHorizontalMirrorLineWithSmudge(char[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int mid = 1; mid <= rows / 2; mid++)
        {
            int discrepancyCount = 0;

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < mid; row++)
                {
                    if (array[mid - row - 1, col] != array[mid + row, col])
                    {
                        discrepancyCount++;
                        if (discrepancyCount > 1) break;
                    }
                }
                if (discrepancyCount > 1) break;
            }

            if (discrepancyCount == 1) return mid;
        }
        return -1; // No horizontal mirror line with smudge found
    }
    
    public static int FindVerticalMirrorLineWithSmudge(char[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int mid = 1; mid <= cols / 2; mid++)
        {
            int discrepancyCount = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < mid; col++)
                {
                    if (array[row, mid - col - 1] != array[row, mid + col])
                    {
                        discrepancyCount++;
                        if (discrepancyCount > 1) break;
                    }
                }
                if (discrepancyCount > 1) break;
            }

            if (discrepancyCount == 1) return mid;
        }
        return -1; // No vertical mirror line with smudge found
    }


    public static long SummarizeReflectionLinesWithSmudges(List<char[,]> arrays)
    {
        long sum = 0;

        foreach (var array in arrays)
        {
            int horizontalResult = FindHorizontalMirrorLineWithSmudge(array);
            if (horizontalResult != -1)
                sum += horizontalResult * 100;

            int verticalResult = FindVerticalMirrorLineWithSmudge(array);
            if (verticalResult != -1)
                sum += verticalResult;
        }

        return sum;
    }



}