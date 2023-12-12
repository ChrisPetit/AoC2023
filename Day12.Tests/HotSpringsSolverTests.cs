namespace Day12.Tests;

public class HotSpringsSolverTests
{
    [Theory]
    [InlineData("???.###", new[] {1, 1, 3}, 1)]
    [InlineData(".??..??...?##.", new[] {1, 1, 3}, 4)]
    [InlineData("?#?#?#?#?#?#?#?", new[] {1, 3, 1, 6}, 1)]
    [InlineData("????.#...#...", new[] {4, 1, 1}, 1)]
    [InlineData("????.######..#####.", new[] {1, 6, 5}, 4)]
    [InlineData("?###????????", new[] {3, 2, 1}, 10)]
    public void TestCountArrangements(string input, int[] groupSizes, int expected)
    {
        // Act
        var result = HotSpringsSolver.CountArrangements(input, groupSizes);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void TestCountTotalArrangements()
    {
        // Arrange
        var input = new string[]
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
        };
        
        // Act
        var result = HotSpringsSolver.CountTotalArrangements(input);
        
        // Assert
        Assert.Equal(21, result);
    }

    [Fact]
    public void TestUnfoldedRow()
    {
        // Arrange
        const string input = "???.### 1,1,3";

        // Act
        var result = HotSpringsSolver.UnfoldedRow(input);

        // Assert
        Assert.Equal("???.###????.###????.###????.###????.### 1,1,3,1,1,3,1,1,3,1,1,3,1,1,3", result);
    }
    
    [Fact]
    public void TestUnFoldAndCountTotalArrangements()
    {
        // Arrange
        var input = new []
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
        };
        
        // Act
        var result = HotSpringsSolver.UnFoldAndCountTotalArrangements(input);
        
        // Assert
        Assert.Equal(525152, result);
    }
}