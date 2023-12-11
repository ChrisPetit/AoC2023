namespace Day11;

public class GalaxyProcessor
{
    private readonly List<Vector> _galaxyCoordinates = new();
    private readonly Dictionary<int, long> _rowExpansionFactors = new();
    private readonly Dictionary<int, long> _columnExpansionFactors = new();

    public GalaxyProcessor(IReadOnlyList<string> input, int expansionFactor = 2)
    {
        CalculateExpansionFactors(input, expansionFactor); 
        IdentifyGalaxies(input);
    }
    
    private void CalculateExpansionFactors(IReadOnlyList<string> input, int expansionFactor)
    {
        // Calculate row expansion factors
        for (var i = 0; i < input.Count; i++)
        {
            _rowExpansionFactors[i] = input[i].All(c => c == '.') ? expansionFactor : 1;
        }

        // Calculate column expansion factors
        for (var i = 0; i < input[0].Length; i++)
        {
            _columnExpansionFactors[i] = input.All(line => line[i] == '.') ? expansionFactor : 1;
        }
    }
    
    private void IdentifyGalaxies(IReadOnlyList<string> input)
    {
        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] != '#') continue;
                var adjustedX = AdjustCoordinate(i, _rowExpansionFactors);
                var adjustedY = AdjustCoordinate(j, _columnExpansionFactors);
                _galaxyCoordinates.Add(new Vector(adjustedX, adjustedY));
            }
        }
    }
    
    private static long AdjustCoordinate(int originalCoordinate, Dictionary<int, long> expansionFactors)
    {
        long adjustedCoordinate = 0;
        for (var i = 0; i < originalCoordinate; i++)
        {
            adjustedCoordinate += expansionFactors[i];
        }
        return adjustedCoordinate;
    }
    
    public long CalculateTotalShortestPaths()
    {
        var totalDistance = 0L;
        for (var i = 0; i < _galaxyCoordinates.Count; i++)
        {
            for (var j = i + 1; j < _galaxyCoordinates.Count; j++)
            {
                var distance = CalculateManhattanDistance(_galaxyCoordinates[i], _galaxyCoordinates[j]);
                totalDistance += distance;
            }
        }
        return totalDistance;
    }

    private static long CalculateManhattanDistance(Vector a, Vector b)
    {
        return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
    }
}

public struct Vector
{
    public long X { get; }
    public long Y { get; }

    public Vector(long x, long y)
    {
        X = x;
        Y = y;
    }
}