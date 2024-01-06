namespace Day16;

public class ControlPanel(IReadOnlyList<string> input)
{
    public int FindMaxEnergizedTiles()
    {
        var maxEnergizedCells = 0;
            
        var rows = input.Count;
        var cols = input[0].Length;
            
        // Beam entering from top row
        for(var i = 0; i < cols; i++)
        {
            var contraption = new Contraption(input, new Beam(i, -1, Direction.Down));
            maxEnergizedCells = Math.Max(maxEnergizedCells, contraption.Energize());
        }
            
        // Beam entering from bottom row
        for(var i = 0; i < cols; i++)
        {
            var contraption = new Contraption(input, new Beam(i, rows, Direction.Up));
            maxEnergizedCells = Math.Max(maxEnergizedCells, contraption.Energize());
        }
            
        // Beam entering from left column
        for(var i = 0; i < rows; i++)
        {
            var contraption = new Contraption(input, new Beam(-1, i, Direction.Right));
            maxEnergizedCells = Math.Max(maxEnergizedCells, contraption.Energize());
        }
            
        // Beam entering from right column
        for(var i = 0; i < rows; i++)
        {
            var contraption = new Contraption(input, new Beam(cols, i, Direction.Left));
            maxEnergizedCells = Math.Max(maxEnergizedCells, contraption.Energize());
        }
            
        return maxEnergizedCells;
    }
}