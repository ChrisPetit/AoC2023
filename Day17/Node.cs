namespace Day17;

public class Node(int column, int row, int heatLoss)
{
    public int Column { get; init; } = column;
    public int Row { get; init; } = row;
    public int HeatLoss { get; init; } = heatLoss; //Cost
    public int G { get; set; } // Cost to reach this node
    public int H { get; set; } // Heuristic cost from this node to goal
    public int F => G + H; // Total cost
    public Node? Parent { get; set; }
    public Direction DirectionFromParent { get; set; } = Direction.None;
    public char PreviousDirection { get; set; }
    public int ConsecutiveSteps { get; set; } 
    public int StepsInCurrentDirection { get; set; }
}