namespace Day16;

internal class Beam
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }

    public Beam(int x, int y, Direction direction)
    {
        X = x;
        Y = y;
        Direction = direction;

    }
}