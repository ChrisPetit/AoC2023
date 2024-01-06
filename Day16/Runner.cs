namespace Day16;

public class Runner
{
    public static void RunDay16()
    {
        var input = File.ReadAllLines("input/Day16.txt");
        var contraption = new Contraption(input);
        var result = contraption.Energize();
        Console.WriteLine($"{result} tiles end up being energized");
        var controlPanel = new ControlPanel(input);
    
        var maxNumberOfEnergizedTiles = controlPanel.FindMaxEnergizedTiles();
        Console.WriteLine($"{maxNumberOfEnergizedTiles} is the largest number of energized tiles");
    }
}