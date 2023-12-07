namespace Day07;

public class CamelCardsGame
{
    public static int CalculateTotalWinnings(IEnumerable<string> hands)
    {
        var handObjects = hands.Select(hand => new NormalHand(hand)).ToList();
        handObjects.Sort();

        return handObjects.Select((t, i) => t.Bid * (handObjects.Count - i)).Sum();
    }
    
    public static int CalculateTotalWinningsWithWildCards(IEnumerable<string> hands)
    {
        var handObjects = hands.Select(hand => new WildCardHand(hand)).ToList();
        handObjects.Sort();

        return handObjects.Select((t, i) => t.Bid * (handObjects.Count - i)).Sum();
    }
}