namespace Day17;

public class PathFinder
{
    private readonly int[][] _grid;
    private readonly int _width;
    private readonly int _height;
    private readonly bool _part2;
    
    private const int MaxCountPart1 = 3;
    private const int MaxCountPart2 = 10;

    public PathFinder(IEnumerable<string> input, bool part2 = false)
    {
        _grid = input.Select(line => line.Trim().Select(c => int.Parse("" + c)).ToArray()).ToArray();
        _width = _grid[0].Length;
        _height = _grid.Length;
        _part2 = part2;
    }
    
    public int MinHeatLossPath()
    {
        var start = new Neighbour { Position = Node.Zero, Previous = Node.Zero, Count = 0 };
        var target = new Node(_width - 1, _height - 1);

        var open = new PriorityQueue<Neighbour, int>();
        open.Enqueue(start, 0);

        var cameFrom = new Dictionary<Neighbour, Neighbour>();

        var gScore = new Dictionary<Neighbour, int>();
        var fScore = new Dictionary<Neighbour, int>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, target);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            if (current.Position == target)
            {
                var path = ReconstructPath(cameFrom, current);
                return path.Sum(s => _grid[s.Position.Y][s.Position.X]) - _grid[0][0];
            }


            var neighbours = _part2 ? GetNeighboursPart2(current) : GetNeighboursPart1(current);
            
            foreach (var neighbor in neighbours)
            {
                var tentativeGScore = gScore[current] + _grid[neighbor.Position.Y][neighbor.Position.X];
                var neighborGScore = gScore.GetValueOrDefault(neighbor, int.MaxValue);
                if (tentativeGScore >= neighborGScore) continue;
                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = tentativeGScore + Heuristic(neighbor, target);
                if (!open.UnorderedItems.Any(s => s.Element.Equals(neighbor)))
                {
                    open.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }
        throw new Exception("No path found");

        IEnumerable<Neighbour> ReconstructPath(IReadOnlyDictionary<Neighbour, Neighbour> cameFromNode, Neighbour current)
        {
            var totalPath = new List<Neighbour> { current };
            while (cameFromNode.ContainsKey(current))
            {
                current = cameFromNode[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            return totalPath;
        }

        int Heuristic(Neighbour step, Node targetNode)
        {
            // return 1;
            return step.Position.Manhattan(targetNode);
        }
    }
    
    private bool IsNodeWithinGrid(Node p)
    {
        return p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height;
    }

    private IEnumerable<Neighbour> GetNeighboursPart1(Neighbour node)
    {
        return new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
            .Where(IsNodeWithinGrid)
            .Where(p => p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height).Where(p => p != node.Previous)
            .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = GetCount(p) })
            .Where(s => s.Count <= MaxCountPart1);

        int GetCount(Node pos)
        {
            return pos.X == node.Previous.X || pos.Y == node.Previous.Y ? node.Count + 1 : 1;
        }
    }

    private IEnumerable<Neighbour> GetNeighboursPart2(Neighbour node)
    {


        return node.Count switch
        {
            0 => new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
                .Where(p => p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = 1 }),
            < 4 => new[] { node.Position + (node.Position - node.Previous) }
                .Where(p => p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = node.Count + 1 }),
            _ => new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
                .Where(IsNodeWithinGrid)
                .Where(p => p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height)
                .Where(p => p != node.Previous)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = GetCount(p) })
                .Where(s => s.Count <= MaxCountPart2)
        };

        int GetCount(Node pos)
        {
            return pos.X == node.Previous.X || pos.Y == node.Previous.Y ? node.Count + 1 : 1;
        }
    }

    private struct Neighbour
    {
        public Node Position;
        public Node Previous;
        public int Count;
    }
    
    public record Node(int X, int Y)
    {
        public static Node Zero => new(0, 0);

        public Node Left => new(X - 1, Y);
        public Node Right => new(X + 1, Y);
        public Node Up => new(X, Y - 1);
        public Node Down => new(X, Y + 1);

        public IEnumerable<Node> Adjacent => new[] { Up, Right, Down, Left };

        public static Node operator +(Node a, Node b) => new(a.X + b.X, a.Y + b.Y);
        public static Node operator -(Node a, Node b) => new(a.X - b.X, a.Y - b.Y);

        public int Manhattan(Node other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public override string ToString() => $"({X}, {Y})";

    }
}