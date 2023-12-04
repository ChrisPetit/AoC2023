namespace Day04.Tests;

public class ScratchcardCalculatorTests
{
    [Fact]
    public void CalculatePoints_ValidInput_CalculatesCorrectPoints()
    {
        // Arrange
        var testCases = new List<(List<int> winningNumbers, List<int> playerNumbers, int expectedPoints)>
        {
            (new List<int> { 41, 48, 83, 86, 17 }, new List<int> { 83, 86, 6, 31, 17, 9, 48, 53 }, 8),
            (new List<int> { 13, 32, 20, 16, 61 }, new List<int> { 61, 30, 68, 82, 17, 32, 24, 19 }, 2),
            (new List<int> { 1, 21, 53, 59, 44 }, new List<int> { 69, 82, 63, 72, 16, 21, 14, 1 }, 2),
            (new List<int> { 41, 92, 73, 84, 69 }, new List<int> { 59, 84, 76, 51, 58, 5, 54, 83 }, 1),
            (new List<int> { 87, 83, 26, 28, 32 }, new List<int> { 88, 30, 70, 12, 93, 22, 82, 36 }, 0),
            (new List<int> { 31, 18, 13, 56, 72 }, new List<int> { 74, 77, 10, 23, 35, 67, 36, 11 }, 0)
        };

        foreach (var testCase in testCases)
        {
            // Act
            var result = ScratchcardCalculator.CalculatePoints(testCase.winningNumbers, testCase.playerNumbers);

            // Assert
            Assert.Equal(testCase.expectedPoints, result);
        }
    }
}