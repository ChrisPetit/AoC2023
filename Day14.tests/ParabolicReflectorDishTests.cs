namespace Day14.tests;

public class ParabolicReflectorDishTests
{
    [Fact]
    public void Test_CalculateLoadOnNorthBeams()
    {
        var input = new[]{
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };

        var dish = new ParabolicReflectorDish(input);
        var result = dish.CalculateLoadOnNorthBeams();
        Assert.Equal(136, result);
    }
    
    [Fact]
    public void Test_CalculateLoadOnNorthBeams_AfterManyCycles()
    {
        var input = new[] {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };
    
        var dish = new ParabolicReflectorDish(input);
        dish.PerformCycles(1000000000);  // Perform many cycles
        var result = dish.CalculateLoadOnNorthBeams();
        Assert.Equal(64, result);  // Expected output
    }
}
