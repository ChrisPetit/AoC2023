namespace Day16.Tests;

public class ContraptionTests 
{
    [Fact]
    public void Test_Contraption_Energizes_Correct_Number_Of_Tiles() 
    {
        // Arrange
        string[] input = new string[]
        {
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
        };
        var contraption = new Contraption(input);

        // Act
        var result = contraption.Energize();

        // Assert
        Assert.Equal(46, result);
    }
}