using Day10;

namespace Day11.Tests;

public class PipeMazeSolverTests
{
    [Fact]
    public void TestSquareLoop()
    {
        string[] input = new string[]
        {
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            "....."
        };
        var solver = new PipeMazeSolver(input);
        Assert.Equal(4, solver.FindFarthestDistance());
    }

    [Fact]
    public void TestComplexLoop()
    {
        string[] input = new string[]
        {
            "..F7.",
            ".FJ|.",
            "SJ.L7",
            "|F--J",
            "LJ..."
        };
        var solver = new PipeMazeSolver(input);
        Assert.Equal(8, solver.FindFarthestDistance());
    }
    
    [Fact]
    public void TestEnclosedArea_SmallExample()
    {
        string[] input = {
            "...........",
            ".S-------7.",
            ".|F-----7|.",
            ".||.....||.",
            ".||.....||.",
            ".|L-7.F-J|.",
            ".|..|.|..|.",
            ".L--J.L--J.",
            "..........."
        };
        var solver = new PipeMazeSolver(input);
        solver.FindFarthestDistance(); // Run to populate the grid
        int enclosedArea = solver.CalculateEnclosedArea();
        Assert.Equal(4, enclosedArea);
    }
    
    [Fact]
    public void TestEnclosedArea_ExampleWithJunkPipes()
    {
        string[] input = {
            "FF7FSF7F7F7F7F7F---7",
            "L|LJ||||||||||||F--J",
            "FL-7LJLJ||||||LJL-77",
            "F--JF--7||LJLJ7F7FJ-",
            "L---JF-JLJ.||-FJLJJ7",
            "|F|F-JF---7F7-L7L|7|",
            "|FFJF7L7F-JF7|JL---7",
            "7-L-JL7||F7|L7F-7F7|",
            "L.L7LFJ|||||FJL7||LJ",
            "L7JLJL-JLJLJL--JLJ.L"
        };
        var solver = new PipeMazeSolver(input);
        solver.FindFarthestDistance(); // Run to populate the grid
        int enclosedArea = solver.CalculateEnclosedArea();
        Assert.Equal(10, enclosedArea);
    }

}
