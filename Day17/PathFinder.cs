using System.Net;

namespace Day17;

/// <summary>
/// Represents a class for finding the minimum heat loss path in a grid.
/// </summary>
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

    /// <summary>
    /// Finds the minimum heat loss path in a grid.
    /// </summary>
    /// <returns>The minimum heat loss.</returns>
    public int MinHeatLossPath()
    {
        var start = new Neighbour { Position = Node.Zero, Previous = Node.Zero, Count = 0, HeatLoss = _grid[0][0]};
        var target = new Node(_width - 1, _height - 1);

        var open = new PriorityQueue<Neighbour, int>();
        open.Enqueue(start, 0);
        var closed = new HashSet<Neighbour>();

        var cameFrom = new Dictionary<Neighbour, Neighbour>();

        var gScore = new Dictionary<Neighbour, int>();
        var fScore = new Dictionary<Neighbour, int>();

        gScore[start] = 0;
        fScore[start] = Heuristic(start, target);

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            closed.Add(current);
            if (current.Position == target)
            {
                var path = ReconstructPath(cameFrom, current);
                return path.Sum(s => s.HeatLoss) - _grid[0][0];
            }


            var neighbours = _part2 ? GetNeighboursPart2(current) : GetNeighboursPart1(current);
            
            foreach (var neighbour in neighbours)
            {
                var tentativeGScore = gScore[current] + neighbour.HeatLoss;
                var neighborGScore = gScore.GetValueOrDefault(neighbour, int.MaxValue);
                if (tentativeGScore >= neighborGScore) continue;
                cameFrom[neighbour] = current;
                gScore[neighbour] = tentativeGScore;
                fScore[neighbour] = tentativeGScore + Heuristic(neighbour, target);
                if (closed.Contains(neighbour)) continue;
                open.Enqueue(neighbour, fScore[neighbour]);
            }
        }
        throw new Exception("No path found");

        IEnumerable<Neighbour> ReconstructPath(IReadOnlyDictionary<Neighbour, Neighbour> cameFromNode, Neighbour current)
        {
            var totalPath = new LinkedList<Neighbour>();
            totalPath.AddFirst(current);
            while (cameFromNode.ContainsKey(current))
            {
                current = cameFromNode[current];
                totalPath.AddFirst(current);
            }

            return totalPath.Reverse();
        }

        int Heuristic(Neighbour step, Node targetNode)
        {
            // return 1;
            return step.Position.Manhattan(targetNode) + step.HeatLoss;
        }
    }

    /// <summary>
    /// Retrieves the neighbors of the given node for Part 1 of the path finding algorithm.
    /// </summary>
    /// <param name="node">The current node.</param>
    /// <returns>An IEnumerable collection of Neighbor objects representing the neighbors of the given node.</returns>
    private IEnumerable<Neighbour> GetNeighboursPart1(Neighbour node)
    {
        return new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
            .Where(IsNodeWithinGrid)
            .Where(p => p != node.Previous)
            .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = GetCount(p), HeatLoss = _grid[p.Y][p.X]})
            .Where(s => s.Count <= MaxCountPart1);

        int GetCount(Node pos)
        {
            return pos.X == node.Previous.X || pos.Y == node.Previous.Y ? node.Count + 1 : 1;
        }
    }
    
    /// <summary>
    /// Returns the neighbours of a given node for the second part of the grid.
    /// </summary>
    /// <param name="node">The node whose neighbours are to be determined.</param>
    /// <returns>An enumerable collection of neighbours for the given node.</returns>
    private IEnumerable<Neighbour> GetNeighboursPart2(Neighbour node)
    {
        return node.Count switch
        {
            0 => new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
                .Where(IsNodeWithinGrid)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = 1, HeatLoss = _grid[p.Y][p.X] }),
            < 4 => new[] { node.Position + (node.Position - node.Previous) }
                .Where(IsNodeWithinGrid)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = node.Count + 1, HeatLoss = _grid[p.Y][p.X] }),
            _ => new[] { node.Position.Up, node.Position.Right, node.Position.Down, node.Position.Left }
                .Where(IsNodeWithinGrid)
                .Where(p => p != node.Previous)
                .Select(p => new Neighbour { Position = p, Previous = node.Position, Count = GetCount(p), HeatLoss = _grid[p.Y][p.X] })
                .Where(s => s.Count <= MaxCountPart2)
        };

        int GetCount(Node pos)
        {
            return pos.X == node.Previous.X || pos.Y == node.Previous.Y ? node.Count + 1 : 1;
        }
    }
    

    /// <summary>
    /// Determines whether a given node is within the grid.
    /// </summary>
    /// <param name="p">The node to check.</param>
    /// <returns>
    /// <c>true</c> if the node is within the grid; otherwise, <c>false</c>.
    /// </returns>
    private bool IsNodeWithinGrid(Node p)
    {
        return p.X >= 0 && p.X < _width && p.Y >= 0 && p.Y < _height;
    }
    
    /// <summary>
    /// Represents a neighbour in the grid.
    /// </summary>
    private struct Neighbour
    {
        public Node Position;
        public Node Previous;
        public int Count;
        public int HeatLoss;
    }

    /// <summary>
    /// Represents a node in a grid.
    /// </summary>
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