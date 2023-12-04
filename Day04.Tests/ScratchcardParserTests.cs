namespace Day04.Tests;

public class ScratchcardParserTests
{
    [Theory]
    [InlineData("41 48 83 86 17 | 83 86 6 31 17 9 48 53", new[] { 41, 48, 83, 86, 17 }, new[] { 83, 86, 6, 31, 17, 9, 48, 53 })]
    [InlineData("13 32 20 16 61 | 61 30 68 82 17 32 24 19", new[] { 13, 32, 20, 16, 61 }, new[] { 61, 30, 68, 82, 17, 32, 24, 19 })]
    // Add more valid test cases here
    public void ParseCard_ValidInput_ReturnsCorrectData(string cardData, int[] expectedWinningNumbers, int[] expectedPlayerNumbers)
    {
        // Arrange

        // Act
        var (winningNumbers, playerNumbers) = ScratchcardParser.ParseCard(cardData);

        // Assert
        Assert.Equal(expectedWinningNumbers, winningNumbers);
        Assert.Equal(expectedPlayerNumbers, playerNumbers);
    }

    [Theory]
    [InlineData("")]
    [InlineData("41 48 83 86 17")]
    [InlineData("41 48 83 86 17 || 83 86 6 31 17 9 48 53")]
    // Add more invalid test cases here
    public void ParseCard_InvalidInput_ThrowsArgumentException(string invalidCardData)
    {
        // Arrange

        // Act & Assert
        Assert.Throws<ArgumentException>(() => ScratchcardParser.ParseCard(invalidCardData));
    }
}
