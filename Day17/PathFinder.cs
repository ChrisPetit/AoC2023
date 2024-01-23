namespace Day17;

public class PathFinder
{
    private readonly Node?[,] _grid;
    private readonly Node? _startNode;
    private readonly Node? _goalNode;
    private readonly int _gridSize;

    public PathFinder(IReadOnlyList<string> input)
    {
        _grid = InitializeGrid(input, out _startNode, out _goalNode, out _gridSize);
    }

    private static Node?[,] InitializeGrid(IReadOnlyList<string> input, out Node? startNode, out Node? goalNode, out int gridSize)
    {
        if (input == null || input.Count == 0)
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        gridSize = input.Count;
        Node?[,] grid = new Node[gridSize, gridSize];

        for (var y = 0; y < gridSize; y++)
        {
            for (var x = 0; x < gridSize; x++)
            {
                if (!char.IsDigit(input[y][x]))
                    throw new FormatException("Input contains non-numeric character.");

                grid[x, y] = new Node { X = x, Y = y, HeatLoss = input[y][x] - '0' };
            }
        }

        startNode = grid[0, 0];
        goalNode = grid[gridSize - 1, gridSize - 1];
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

        foreach (var (dx, dy, dir) in directions)
        {
            var newX = node!.X + dx;
            var newY = node.Y + dy;
            var canMoveInDirection = CanMoveInDirection(node, dir);

            if (newX < 0 || newX >= _gridSize || newY < 0 || newY >= _gridSize || !canMoveInDirection) continue;
            var neighbour = _grid[newX, newY];
            neighbour!.DirectionFromParent = dir;
            neighbour.StepsInCurrentDirection = (node.DirectionFromParent == dir) ? node.StepsInCurrentDirection + 1 : 1;
            neighbours.Add(neighbour);
        }

        return neighbours;
    }

    
    private static bool CanMoveInDirection(Node? node, Direction newDirection)
    {
        if (node!.DirectionFromParent == Direction.None)
            return true;

        if (node.DirectionFromParent == newDirection)
            return node.StepsInCurrentDirection <= 2;

        return node.DirectionFromParent != OppositeDirection(newDirection);
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
        // Manhattan distance as heuristic
        return Math.Abs(node!.X - _goalNode!.X) + Math.Abs(node.Y - _goalNode.Y) - node.HeatLoss;
    }

    public List<Node?> FindPath()
    {
        var openSet = new List<Node?>();
        var closedSet = new HashSet<Node?>();
        _startNode!.G = 0;
        _startNode.H = CalculateHeuristic(_startNode);
        _startNode.DirectionFromParent = Direction.None;
        _startNode.StepsInCurrentDirection = 0;
        openSet.Add(_startNode);

        while (openSet.Count > 0)
        {
            var currentNode = openSet.OrderBy(node => node!.F).First();
            if (currentNode == _goalNode)
            {
                return ReconstructPath(currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (var neighbour in GetNeighbours(currentNode))
            {
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }

                var tentativeGScore = currentNode!.G + neighbour!.HeatLoss;
                if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
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