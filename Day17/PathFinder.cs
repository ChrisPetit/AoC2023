namespace Day17;

public class PathFinder()
{
    private Node[,] _nodes = null!;
    private Node _startNode;
    private Node _goalNode;


    public PathFinder(IReadOnlyList<string> input) : this()
    {
        InitializeNodes(input);
        _startNode = _nodes[0, 0];
        _goalNode = _nodes[_nodes.GetLength(0) - 1, _nodes.GetLength(1) - 1];
    }

    private List<Node> GetNeighbours(Node node, Node previousNode)
    {
        // Implement logic to get valid neighbors considering turning restrictions
        // and no reversing rule.
        var directions = new List<(int dx, int dy)>
        {
            (0, 1), // Down
            (1, 0), // Right
            (0, -1), // Up
            (-1, 0) // Left
        };

        return (from direction in directions
            let dx = direction.dx
            let dy = direction.dy
            let newX = node.X + dx
            let newY = node.Y + dy
            where newX >= 0 && newX < _nodes.GetLength(0) && newY >= 0 && newY < _nodes.GetLength(1)
            select _nodes[newX, newY]
            into neighbour
            where neighbour != previousNode && neighbour.Value != 0
            select neighbour).ToList();
    }

    private int CalculateHeuristic(Node node)
    {
        // Use Manhattan distance with adjustments for turning restrictions
        var dx = Math.Abs(node.X - _goalNode.X);
        var dy = Math.Abs(node.Y - _goalNode.Y);
        var turningCost = 0;
        var parentNode = node.Parent;
        if(parentNode != null)
        {
            var grandParentNode = parentNode.Parent;
            if(grandParentNode != null)
            {
                turningCost = ((node.X - parentNode.X) * (parentNode.X - grandParentNode.X) +(node.Y - parentNode.Y) * (parentNode.Y - grandParentNode.Y) != 0) ?
                    2 : 0;
            }
        }
        var heuristic = dx + dy + turningCost;
        return heuristic;
    }

    private List<Node>? FindPath()
    {
        // OpenSet is a SortedSet of nodes to be evaluated, start by adding the start node
        var openSet =
            new SortedSet<Node>(Comparer<Node>
                    .Create((x, y) => x.F != y.F ? x.F
                        .CompareTo(y.F) : x.G
                        .CompareTo(y.G)))
                {_startNode};

        // Create a hashset for quick lookups
        var openSetLookup = new HashSet<Node> { _startNode };

        // While there are still nodes to be evaluated
        while (openSet.Count > 0)
        {
            // Get the node in openSet having the lowest F score
            var currentNode = openSet.Min;

            // If currentNode is the goal node, then we reached the end
            if (currentNode == _goalNode)
            {
                // Construct the path from goal to start
                var path = new List<Node>();
                while (currentNode != null)
                {
                    path.Insert(0, currentNode);
                    currentNode = currentNode.Parent;
                }

                return path;
            }

            // Else, continue the search
            openSet.Remove(currentNode);

            // Generate all the successors
            foreach (var neighbour in GetNeighbours(currentNode, currentNode.Parent))
            {
                // Calculate new G cost for the neighbour
                var tentativeGCost = currentNode.G + neighbour.Value;

                // If this path to neighbour is better than what was previously calculated
                if (openSetLookup.Contains(neighbour) && tentativeGCost >= neighbour.G) continue;
                neighbour.Parent = currentNode;
                neighbour.G = tentativeGCost;
                neighbour.H = CalculateHeuristic(neighbour);

                // If this neighbour is not in openSet, add it
                if (openSetLookup.Contains(neighbour)) continue;
                openSet.Add(neighbour);
                openSetLookup.Add(neighbour);
            }
        }

        return null; // If there's no possible path
    }

    public int CalculateHeat()
    {
        // Calculate the heat by summing up the values of all nodes in the path
        int heat = 0;
        foreach (var node in FindPath())
        {
            heat += node.Value;
        }
        return heat;
    }

    private void InitializeNodes(IReadOnlyList<string> input)
    {
        // Initialize the nodes array width and height based on the input
        var width = input[0].Length;
        var height = input.Count;
        _nodes = new Node[width, height];

        // Iterate and fill up the values for each node
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var value = input[i][j] - '0'; // assuming values are digits in the input strings
                _nodes[j, i] = new Node(j, i, value);
            }
        }
    }
}

public class Node(int x, int y, int value)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Value { get; set; } = value;// Node value
    public int G { get; set; } // Cost to reach this node
    public int H { get; set; } // Heuristic cost from this node to goal
    public int F => G + H; // Total cost
    public Node Parent { get; set; }
}