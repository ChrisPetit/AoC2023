namespace Day03.Tests;

public class EngineSchematicGearRatioAnalyzerTests
{
    [Fact]
    public void TestSumOfGearRatios()
    {
        const string schematic = "467..114..\n" +
                                 "...*......\n" +
                                 "..35..633.\n" +
                                 "......#...\n" +
                                 "617*......\n" +
                                 ".....+.58.\n" +
                                 "..592.....\n" +
                                 "......755.\n" +
                                 "...$.*....\n" +
                                 ".664.598..";
        
        var result = EngineSchematicGearRatioAnalyzer.SumOfGearRatios(schematic);
        const int expected = 467835;

        Assert.Equal(expected, result);
    }
}