namespace Day10;
public class PipeMazeSolver
{
    private readonly List<string> _maze;
    private readonly char[,] _x;
    private readonly int _rows;
    private readonly int _cols;

    public PipeMazeSolver(IEnumerable<string> input)
    {
        _maze = input.ToList();
        _rows = _maze.Count;
        _cols = _maze[0].Length;
        _x = new char[_rows, _cols];
        for (var i = 0; i < _rows; i++)
        {
            for (var j = 0; j < _cols; j++)
            {
                _x[i, j] = '.';
            }
        }
    }

    public int FindFarthestDistance()
    {
        var start = FindStart();
        var direction = 0;
        var steps = 0;

        // Initial direction based on the pipe next to S
        if (start.x < _cols - 1 && "-J7".Contains(_maze[start.y][start.x + 1])) direction = 1;
        else if (start.y > 0 && "|7F".Contains(_maze[start.y - 1][start.x])) direction = 2;
        else if (start.x > 0 && "-FL".Contains(_maze[start.y][start.x - 1])) direction = 3;

        var completedLoop = false;
        int x = start.x, y = start.y;
        _x[y, x] = 'S';

        while (!completedLoop)
        {
            var c = '.';
            switch (direction)
            {
                case 0:
                    c = _maze[y + 1][x];
                    direction = c switch
                    {
                        'J' => 3,
                        'L' => 1,
                        _ => direction
                    };
                    y++;
                    break;
                case 1:
                    c = _maze[y][x + 1];
                    direction = c switch
                    {
                        'J' => 2,
                        '7' => 0,
                        _ => direction
                    };
                    x++;
                    break;
                case 2:
                    c = _maze[y - 1][x];
                    direction = c switch
                    {
                        'F' => 1,
                        '7' => 3,
                        _ => direction
                    };
                    y--;
                    break;
                case 3:
                    c = _maze[y][x - 1];
                    direction = c switch
                    {
                        'F' => 0,
                        'L' => 2,
                        _ => direction
                    };
                    x--;
                    break;
            }
            _x[y, x] = c;
            completedLoop = c == 'S';
            steps++;
        }

        return steps / 2; // Farthest point is half the loop's length
    }

    private (int x, int y) FindStart()
    {
        for (var y = 0; y < _rows; y++)
        {
            for (var x = 0; x < _cols; x++)
            {
                if (_maze[y][x] == 'S')
                {
                    return (x, y);
                }
            }
        }
        throw new InvalidOperationException("Start position not found");
    }

    public int CalculateEnclosedArea()
    {
        var enclosedTiles = 0;
        for (var i = 0; i < _rows; i++)
        {
            var inside = false;
            var online = '.';
            for (var j = 0; j < _cols; j++)
            {
                var current = _x[i, j];
                if ("|JLF7".Contains(current))
                {
                    switch (current)
                    {
                        case '|': inside = !inside; break;
                        case 'F': online = 'F'; break;
                        case 'L': online = 'L'; break;
                        case '7': if (online == 'L') inside = !inside; break;
                        case 'J': if (online == 'F') inside = !inside; break;
                    }
                }
                else if (current == '.')
                {
                    if (inside) enclosedTiles++;
                }
            }
        }
        return enclosedTiles;
    }
}