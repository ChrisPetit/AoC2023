namespace Day14;
using System;
using System.Linq;

public class ParabolicReflectorDish
{
    private char[,]? _platform;
    private const char RoundedRock = 'O';
    private const char EmptySpace = '.';
    private readonly Dictionary<string, int> _memo;
    private readonly Dictionary<string, int> _stateToCycle = new();

    public ParabolicReflectorDish(string[] input)
    {
        _memo = new Dictionary<string, int>();
        if (input == null || input.Length == 0 || input.Any(string.IsNullOrEmpty) || input.Select(s => s.Length).Distinct().Count() > 1)
        {
            throw new ArgumentException("The input cannot be null and must be a rectangle shaped array.", nameof(input));
        }

        InitializePlatform(input);
        TiltPlatformNorth();
    }

    private void InitializePlatform(IReadOnlyList<string> input)
   {
       var rows = input.Count; // get the total number of rows (equivalent to the number of strings in the list)
       var cols = input[0].Length; // get the total number of columns (equivalent to the length of first string in the list)
       _platform = new char[rows, cols]; // initialize the platform as a 2D char array
   
       // Iterate over each row
       for (var i = 0; i < rows; i++)
       {
           // In each row, iterate over each column
           for (var j = 0; j < cols; j++)
           {
               // Assign each input[i][j] value to the corresponding [_platform[i, j]] location
               _platform[i, j] = input[i][j];
           }
       }
   }

    public int CalculateLoadOnNorthBeams()
    {
        // Before calculating the load, check if we already have the result in our cache
        var key = GetPlatformState();
        if (_memo.TryGetValue(key, out var beams))
        {
            return beams;
            
        }
        
        var load = 0;
        for (var i = 0; i < _platform!.GetLength(0); i++)
        {
            for (var j = 0; j < _platform.GetLength(1); j++)
            {
                if (_platform[i, j] == RoundedRock)
                    load += _platform.GetLength(0) - i;
            }
        }
        return load;
    }

    private void TiltPlatform()
    {
        TiltPlatformNorth();
        TiltPlatformWest();
        TiltPlatformSouth();
        TiltPlatformEast();
    }

    private void TiltPlatformNorth()
    {
        for (var column = 0; column < _platform!.GetLength(1); column++)
        {
            for (var row = 1; row < _platform.GetLength(0); row++)
            {
                if (_platform[row, column] != RoundedRock) continue;
                var targetRow = row;
                while (targetRow > 0 && _platform[targetRow - 1, column] == EmptySpace)
                {
                    targetRow--;
                }

                if (targetRow == row) continue;
                _platform[targetRow, column] = RoundedRock;
                _platform[row, column] = EmptySpace;
            }
        }
        
        // After performing the tilt, store the result in our cache
        var key = GetPlatformState();
        if (!_memo.ContainsKey(key)) 
        {
            _memo[key] = CalculateLoadOnNorthBeams();
        }
    }
    

   private void TiltPlatformWest()
    {
        for (var column = 0; column < _platform!.GetLength(0); column++)
        {
            for (var row = 1; row < _platform.GetLength(1); row++)
            {
                if (_platform[column, row] != RoundedRock) continue;
                var targetCol = row;
                while (targetCol > 0 && _platform[column, targetCol - 1] == EmptySpace)
                {
                    targetCol--;
                }

                if (targetCol == row) continue;
                _platform[column, targetCol] = RoundedRock;
                _platform[column, row] = EmptySpace;
            }
        }
    }
    
    private void TiltPlatformSouth()
    {
        for (var column = 0; column < _platform!.GetLength(1); column++)
        {
            for (var row = _platform.GetLength(0) - 2; row >= 0; row--)
            {
                if (_platform[row, column] != RoundedRock) continue;
                var targetRow = row;
                while (targetRow < _platform.GetLength(0) - 1 && _platform[targetRow + 1, column] == EmptySpace)
                {
                    targetRow++;
                }

                if (targetRow == row) continue;
                _platform[targetRow, column] = RoundedRock;
                _platform[row, column] = EmptySpace;
            }
        }
    }
    
    private void TiltPlatformEast()
    {
        for (var column = 0; column < _platform!.GetLength(0); column++)
        {
            for (var row = _platform.GetLength(1) - 2; row >= 0; row--)
            {
                if (_platform[column, row] != RoundedRock) continue;
                var targetCol = row;
                while (targetCol < _platform.GetLength(1) - 1 && _platform[column, targetCol + 1] == EmptySpace)
                {
                    targetCol++;
                }

                if (targetCol == row) continue;
                _platform[column, targetCol] = RoundedRock;
                _platform[column, row] = EmptySpace;
            }
        }
    }
    
    public void PerformCycles(int numberOfCycles)
    {
        var currentCycle = 0;
        while (currentCycle < numberOfCycles)
        {
            TiltPlatform();
            var currentState = GetPlatformState();

            if (_stateToCycle.TryGetValue(currentState, out var cycleWithSameState))
            {
                var cycleCollisionLength = currentCycle - cycleWithSameState;

                // This will skip the cycles to the one before the next cycle where
                // cycleCollisionLength would take place, essentially performing
                // cycleCollisionLength cycles at once
                var additionalCycles = (numberOfCycles - currentCycle) / cycleCollisionLength;
                currentCycle += additionalCycles * cycleCollisionLength;
            }

            _stateToCycle[currentState] = currentCycle;
            currentCycle++;
        }
    }
    
    private string GetPlatformState()
    {
        return string.Join("", _platform!.Cast<char>());
    }
}