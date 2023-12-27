namespace Day14;
using System;
using System.Linq;

public class ParabolicReflectorDish
{
    private char[,] _platform;
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
        var rows = input.Count;
        var cols = input[0].Length;
        _platform = new char[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
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
        for (var i = 0; i < _platform.GetLength(0); i++)
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
        for (var j = 0; j < _platform.GetLength(1); j++)
        {
            for (var i = 1; i < _platform.GetLength(0); i++)
            {
                if (_platform[i, j] != RoundedRock) continue;
                var targetRow = i;
                while (targetRow > 0 && _platform[targetRow - 1, j] == EmptySpace)
                {
                    targetRow--;
                }

                if (targetRow == i) continue;
                _platform[targetRow, j] = RoundedRock;
                _platform[i, j] = EmptySpace;
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
        for (var i = 0; i < _platform.GetLength(0); i++)
        {
            for (var j = 1; j < _platform.GetLength(1); j++)
            {
                if (_platform[i, j] != RoundedRock) continue;
                var targetCol = j;
                while (targetCol > 0 && _platform[i, targetCol - 1] == EmptySpace)
                {
                    targetCol--;
                }

                if (targetCol == j) continue;
                _platform[i, targetCol] = RoundedRock;
                _platform[i, j] = EmptySpace;
            }
        }
    }
    
    private void TiltPlatformSouth()
    {
        for (var j = 0; j < _platform.GetLength(1); j++)
        {
            for (var i = _platform.GetLength(0) - 2; i >= 0; i--)
            {
                if (_platform[i, j] != RoundedRock) continue;
                var targetRow = i;
                while (targetRow < _platform.GetLength(0) - 1 && _platform[targetRow + 1, j] == EmptySpace)
                {
                    targetRow++;
                }

                if (targetRow == i) continue;
                _platform[targetRow, j] = RoundedRock;
                _platform[i, j] = EmptySpace;
            }
        }
    }
    
    private void TiltPlatformEast()
    {
        for (var i = 0; i < _platform.GetLength(0); i++)
        {
            for (var j = _platform.GetLength(1) - 2; j >= 0; j--)
            {
                if (_platform[i, j] != RoundedRock) continue;
                var targetCol = j;
                while (targetCol < _platform.GetLength(1) - 1 && _platform[i, targetCol + 1] == EmptySpace)
                {
                    targetCol++;
                }

                if (targetCol == j) continue;
                _platform[i, targetCol] = RoundedRock;
                _platform[i, j] = EmptySpace;
            }
        }
    }
    
    // Add a new method for performing many cycles
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

                // This will skip the cycles to the one before the next cycle where cycleCollisionLength would take place, essentially performing cycleCollisionLength cycles at once
                var additionalCycles = (numberOfCycles - currentCycle) / cycleCollisionLength;
                currentCycle += additionalCycles * cycleCollisionLength;
            }

            _stateToCycle[currentState] = currentCycle;
            currentCycle++;
        }
    }
    
    private string GetPlatformState()
    {
        return string.Join("", _platform.Cast<char>());
    }
}