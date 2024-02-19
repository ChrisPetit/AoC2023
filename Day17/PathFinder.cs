namespace Day17;

public class PathFinder(IReadOnlyList<string> input)
{
    private readonly int[,] _grid = InitializeGrid(input);

    private static int[,] InitializeGrid(IReadOnlyList<string> input)
    {
        var columns = input[0].Length;
        var rows = input.Count;
        var grid = new int[columns, rows];

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                if (int.TryParse(input[row][column].ToString(), out var number))
                {
                    grid[row, column] = number;
                }
                else
                {
                    throw new Exception($"Invalid number at position {row}, {column}");
                }
            }
        }

        return grid;
    }

    public List<Node> FindPath()
    {
        var openList = new List<Node>();
        var closedList = new List<Node>();
        var startNode = new Node(0, 0, _grid[0, 0]);
        startNode.G = CalculateG(startNode);
        startNode.H = CalculateHeuristic(startNode);
        
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            var currentNode = openList.OrderBy(n => n.F).First();
            openList.Remove(currentNode);

            var neighbours = GetNeighbours(currentNode);

            foreach (var neighbour in neighbours)
            {
                if (neighbour.Column == _grid.GetLength(0) - 1 && neighbour.Row == _grid.GetLength(1) - 1)
                {
                    return RetracePath(neighbour);
                }

                if (openList.Any(n=>n.Row == neighbour.Row && n.Column == neighbour.Column && n.F < neighbour.F))
                {
                    continue;
                }

                if (closedList.Any(n => n.Row == neighbour.Row && n.Column == neighbour.Column && n.F < neighbour.F))
                {
                    continue;
                }
                
                neighbour.H = CalculateHeuristic(neighbour);
                neighbour.G = CalculateG(neighbour);
                openList.Add(neighbour);
                
            }
            closedList.Add(currentNode);
        }

        throw new Exception("no path found");
    }

    private static List<Node> RetracePath(Node node)
    {
        var path = new List<Node>();
        var currentNode = node;

        // go backwards from end to start
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        // reverse so that path goes from start to end
        path.Reverse();

        return path;
    }

    private List<Node> GetNeighbours(Node node)
    {
        var neighbours = new List<Node>();
            
        //Get the size of the grid
        var maxColumn = _grid.GetLength(0);
        var maxRow = _grid.GetLength(1);

        //Movements to up, down, left and right (in that order)
        var directions = new[]
        {
            new { Row = -1, Col = 0, Id = 'U' },
            new { Row = 1, Col = 0, Id = 'D' },
            new { Row = 0, Col = -1, Id = 'L' },
            new { Row = 0, Col = 1, Id = 'R' }
        };
        for (var i = 0; i < 4; i++)
        {
            var newRow = node.Row + directions[i].Row;
            var newColumn = node.Column + directions[i].Col;
            //Ensure that the node is within the grid
            if (newRow < 0 || newRow >= maxRow || newColumn < 0 || newColumn >= maxColumn) continue;
            //Check if the new move is a step backwards or more than 3 steps in the same direction
            if (node.PreviousDirection == directions[i].Id || node.ConsecutiveSteps > 3) continue;
            var newNode = new Node(newColumn, newRow, _grid[newRow, newColumn])
            {
                Parent = node,
                PreviousDirection = directions[i].Id,
                ConsecutiveSteps = node.PreviousDirection == directions[i].Id ? node.ConsecutiveSteps + 1 : 1
            };
            neighbours.Add(newNode);
        }
        return neighbours;
    }

    private static int CalculateG(Node node)
    {
        if (node.Parent == null)
        {
            // if the node has no parent, it is the starting position, cost is 0
            return 0;
        }
        // add the cost of the parent node to the cost of this step (1) and the HeatLoss of this node
        return node.Parent.G + 1 + node.HeatLoss;
    }
    
    private int CalculateHeuristic(Node node)
    {
        // Assuming our end goal is at the bottom right of the grid
        var goalColumn = _grid.GetLength(0) - 1;
        var goalRow = _grid.GetLength(1) - 1;
    
        // Manhattan distance
        var heuristic = Math.Abs(node.Column - goalColumn) + Math.Abs(node.Row - goalRow) + node.HeatLoss; 

        return heuristic;
    }
}