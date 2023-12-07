namespace Day07.Tests;

public class CamelCardsTests
{
    [Fact]
    public void TestCalculateTotalWinnings()
    {
        var hands = new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };

        var totalWinnings = CamelCardsGame.CalculateTotalWinnings(hands);
        Assert.Equal(6440, totalWinnings);
    }
    
        [Fact]
        public void TestCalculateTotalWinningsWithWildCards()
        {
            var hands = new List<string>
            {
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483"
            };
    
            var totalWinnings = CamelCardsGame.CalculateTotalWinningsWithWildCards(hands);
            Assert.Equal(5905, totalWinnings);
        }
}
