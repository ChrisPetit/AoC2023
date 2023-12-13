namespace Day13.Tests;

public class MirrorFinderTests
{
    [Fact]
    public void FindVerticalMirrorLineTest()
    {
        // Arrange
        var input = new[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#."
        };
        
        var parsed = PatternParser.ParsePatterns(input);
        // Act
        var result = MirrorFinder.FindVerticalMirrorLine(parsed[0]);
        // Assert
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void FindHorizontalMirrorLineTest()
    {
        // Arrange
        var input = new[]
        {
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };
        
        var parsed = PatternParser.ParsePatterns(input);
        // Act
        var result = MirrorFinder.FindHorizontalMirrorLine(parsed[0]);
        // Assert
        Assert.Equal(4, result);
    }
    
    [Fact]
    public void FindMirrorsTest()
    {
        // Arrange
        var input = new[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };
        
        var parsed = PatternParser.ParsePatterns(input);
        // Act
        var result = MirrorFinder.FindMirrors(parsed);
        // Assert
        Assert.Equal(405, result);
    }
    
    [Fact]
    public void SummarizeReflectionLinesWithSmudgesTest()
    {
        // Arrange
        var input = new[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };
        
        var parsed = PatternParser.ParsePatterns(input);
        // Act
        var result = MirrorFinder.SummarizeReflectionLines(parsed);
        // Assert
        Assert.Equal(400, result);
    }
}