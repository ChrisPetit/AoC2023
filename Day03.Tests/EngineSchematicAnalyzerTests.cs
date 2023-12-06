namespace Day03.Tests;

public class EngineSchematicAnalyzerTests
{
    [Fact]
    public void TestSumOfPartNumbers()
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
        
        var result = EngineSchematicAnalyzer.SumOfPartNumbers(schematic);
        const int expected = 4361;

        Assert.Equal(expected, result);
    }
}
