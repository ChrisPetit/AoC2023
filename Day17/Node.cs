namespace Day17;

public class Node
{
    public int X { get; init; }
    public int Y { get; init; }
    public int HeatLoss { get; init; }
    public int G { get; set; } // Cost to reach this node
    public int H { get; set; } // Heuristic cost from this node to goal
    public int F => G + H; // Total cost
    public Node? Parent { get; set; }
    public Direction DirectionFromParent { get; set; } = Direction.None;
    public int StepsInCurrentDirection { get; set; }
}