namespace Day15.Tests;

public class HASHalgorithmTest
{
    [Fact]
    public void HASHalgorithmRunTest()
    {
        // Arrange
        var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        // Act
        var expectedOutput = 1320;
        var actualOutput = HASHalgorithm.Run(input);
        
        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }


    [Fact]
    public void HASHalgorithmHASHMAPTest()
    {
        // Arrange
        var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
        // Act
        var expectedOutput = 145;
        var actualOutput = HASHalgorithm.HASHMAP(input);
        // Assert
        Assert.Equal(expectedOutput, actualOutput);
    }
    
}