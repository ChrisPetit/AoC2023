namespace Day16;

public class Beam(int x, int y, Direction direction)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public Direction Direction { get; set; } = direction;
}