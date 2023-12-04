namespace Day01.Tests;


public class ProcessorTests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void CalculateCalibrationValue_ReturnsCorrectValue(string input, int expected)
    {
        // Act
        var actual = Processor.CalculateCalibrationValue(input);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    [InlineData("kvskplbpgfninesixvzkrv1fqnrjnrhvnpkpkhph27twonemvx", 91)]
    public void CalculateCalibrationValue_ShouldReturnCorrectValue(string input, int expectedValue)
    {
        // Act
        var actualValue = Processor.CalculateNewCalibrationValue(input);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }
}