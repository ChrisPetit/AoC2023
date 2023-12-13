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
        var result = 0L;
        foreach (var array in arrayList)
        {
            var verticalResult = FindVerticalMirrorSmudged(array);
            if (verticalResult != -1) result += verticalResult;

            var horizontalResult = FindHorizontalMirrorSmudged(array);
            if (horizontalResult != -1) result += horizontalResult * 100;
        }
        return result;
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


    private static int FindVerticalMirrorSmudged(char[,] array)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        for (var mirror = 1; mirror < cols; mirror++)
        {
            var smudges = 0;
            var rightSide = cols - mirror;
            var reflectionWidth = Math.Min(mirror, rightSide);

            for (var d = 0; d < reflectionWidth && smudges <= 1; d++)
            {
                for (var row = 0; row < rows && smudges <= 1; row++)
                {
                    if (array[row, mirror - 1 - d] != array[row, mirror + d])
                    {
                        smudges++;
                    }
                }
            }

            if (smudges == 1)
            {
                return mirror;
            }
        }

        return 0;
    }


    private static int FindHorizontalMirrorSmudged(char[,] array)
    {
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);

        for (var mirror = 1; mirror < rows; mirror++)
        {
            var smudges = 0;
            var bottomSide = rows - mirror;
            var reflectionHeight = Math.Min(mirror, bottomSide);

            for (var d = 0; d < reflectionHeight && smudges <= 1; d++)
            {
                for (var col = 0; col < cols && smudges <= 1; col++)
                {
                    if (array[mirror - 1 - d, col] != array[mirror + d, col])
                    {
                        smudges++;
                    }
                }
            }

            if (smudges == 1)
            {
                return mirror;
            }
        }

        return 0;
    }

}