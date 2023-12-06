namespace Day06.Tests;

public class BoatRaceTests
{
    [Fact]
    public void TestTotalWaysToWin()
    {
        // Arrange
        var races = new List<(int raceTime, int recordDistance)>
        {
            (7, 9),
            (15, 40),
            (30, 200)
        };

        // Act
        var result = BoatRace.TotalWaysToWin(races);

        // Assert
        Assert.Equal(288, result); // 4 * 8 * 9
    }
    
    [Fact]
    public void TestCalculateWaysToWinSingleRace()
    {
        // Arrange
        const long raceTime = 71530;
        const long recordDistance = 940200;

        // Act
        var waysToWin = BoatRace.CalculateWaysToWinSingleRace(raceTime, recordDistance);

        // Assert
        Assert.Equal(71503, waysToWin);
    }
}