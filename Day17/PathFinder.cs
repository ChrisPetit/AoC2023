namespace Day17;

public class PathFinder
{
    private readonly Node?[,] _grid;
    private readonly Node? _startNode;
    private readonly Node? _goalNode;
    private readonly int _gridWidth;
    private readonly int _gridHeight;
    private int _stepsInCurrentDirection;
    private Direction _currentDirection;

    public PathFinder(IReadOnlyList<string> input)
    {
        _grid = InitializeGrid(input, out _startNode, out _goalNode, out _gridWidth, out _gridHeight);
        _stepsInCurrentDirection = 0;
    }

    private static Node?[,] InitializeGrid(IReadOnlyList<string> input, out Node? startNode, out Node? goalNode,
        out int gridWidth, out int gridHeight)
    {
        if (input == null || input.Count == 0)
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        gridHeight = input.Count;
        gridWidth = input[0].Length; // assuming all strings have the same length

        Node?[,] grid = new Node[gridWidth, gridHeight];

        for (var y = 0; y < gridHeight; y++)
        {
            for (var x = 0; x < gridWidth; x++)
            {
                if (!char.IsDigit(input[y][x]))
                    throw new FormatException("Input contains non-numeric character.");
                grid[x, y] = new Node { X = x, Y = y, HeatLoss = input[y][x] - '0' };
            }
        }

        startNode = grid[0, 0];
        goalNode = grid[gridWidth - 1, gridHeight - 1];
        return grid;
    }

    private List<Node?> GetNeighbours(Node? node)
    {
        var neighbours = new List<Node?>();
        var directions = new List<(int x, int y, Direction dir)>
        {
            (0, -1, Direction.Up), // Up
            (0, 1, Direction.Down), // Down
            (-1, 0, Direction.Left), // Left
            (1, 0, Direction.Right) // Right
        };

        foreach (var (dx, dy, newDir) in directions)
        {
            var newX = node!.X + dx;
            var newY = node.Y + dy;
            var canMoveInDirection = CanMoveInDirection(node, newDir);

            if (newX < 0 || newX >= _gridWidth || newY < 0 || newY >= _gridHeight || !canMoveInDirection) continue;
            var neighbour = _grid[newX, newY];
            _currentDirection = newDir;
            _stepsInCurrentDirection = (_currentDirection == newDir) ? _stepsInCurrentDirection + 1 : 1;
            neighbours.Add(neighbour);
        }

        return neighbours;
    }

    
    private bool CanMoveInDirection(Node? node, Direction newDirection)
    {
        if (_currentDirection == Direction.None)
            return true;

        if (_currentDirection == newDirection)
            return _stepsInCurrentDirection < 3;

        return _currentDirection != OppositeDirection(newDirection);
    }

    private static Direction OppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => Direction.None
        };
    }

    private int CalculateHeuristic(Node? node)
    {
        // Chebyshev distance as heuristic minus HeatLoss
        return Math.Max(Math.Abs(node!.X - _goalNode!.X),
                   Math.Abs(node.Y - _goalNode.Y)) -
               node.HeatLoss;
    }

    public List<Node?> FindPath()
    {
        var queue = new List<Node?>();
        var visited = new HashSet<Node?>();
        _startNode!.G = 0;
        _startNode.H = CalculateHeuristic(_startNode);
        _startNode.DirectionFromParent = Direction.None;
        _startNode.StepsInCurrentDirection = 0;
        queue.Add(_startNode);

        while (queue.Count > 0)
        {
            var currentNode = queue.OrderBy(node => node!.F).First();
            if (currentNode!.X == _goalNode!.X && currentNode.Y == _goalNode.Y)
            {
                return ReconstructPath(currentNode);
            }

            queue.Remove(currentNode);
            visited.Add(currentNode);
            
            foreach (var neighbour in GetNeighbours(currentNode))
            {
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                var tentativeGScore = currentNode.G + neighbour!.HeatLoss;
                if (!queue.Contains(neighbour)) queue.Add(neighbour);
                else if (tentativeGScore >= neighbour.G) continue;

                neighbour.Parent = currentNode;
                neighbour.G = tentativeGScore;
                neighbour.H = CalculateHeuristic(neighbour);

            }
        }

        throw new Exception("Path not found");
    }

    private static List<Node?> ReconstructPath(Node? currentNode)
    {
        var path = new List<Node?>();
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }
}