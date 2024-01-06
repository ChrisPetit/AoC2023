namespace Day16.Tests;

public class ControlPanelTests
{
    [Fact]
    public void TestFindMaxNumberOfEnergizedTiles()
    {
        string[] input = 
        [
            ".|...\\....",
            "|.-.\\.....",
            ".....|-...",
            "........|.",
            "..........",
            ".........\\",
            @"..../.\\..",
            ".-.-/..|..",
            ".|....-|.\\",
            "..//.|...."
        ];
        const int expectedResult = 51; // add your expected number of energized cells here
        var controlPanel = new ControlPanel(input);
    
        var maxNumberOfEnergizedTiles = controlPanel.FindMaxEnergizedTiles();
        Assert.Equal(expectedResult, maxNumberOfEnergizedTiles);
    }
}