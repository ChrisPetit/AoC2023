namespace Day09.Tests;

public class OasisAndSandInstabilitySensorTests
{
    [Fact]
    public void TestPredictionOfNextValues()
    {
        var input = new List<string>
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        };
        var result = OasisAndSandInstabilitySensor.PredictionOfNextValues(input);
        Assert.Equal(114, result.predicion);
        Assert.Equal(2, result.backwardPredicion);
    }
}