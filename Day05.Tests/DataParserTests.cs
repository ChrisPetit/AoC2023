using Day05;

namespace Day05Tests;

public class DataParserTests
{
    [Fact]
    public void DataParser_CorrectlyParsesSeeds()
    {
        const string seedData = "79 14 55 13";
        var seeds = DataParser.ParseSeeds(seedData);

        Assert.Contains(79, seeds);
        Assert.Contains(55, seeds);
    }
}