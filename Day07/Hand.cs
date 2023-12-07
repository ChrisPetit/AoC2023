namespace Day07;

public class Hand// : IComparable<Hand>
{
    public string Cards { get; set; }
    public int Bid { get; set; }
    public HandType Type { get; set; }
    protected List<KeyValuePair<char, int>> CardGroups;

    private readonly Dictionary<char, int> _cardValues = new Dictionary<char, int>
    {
        {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7},
        {'8', 8}, {'9', 9}, {'T', 10}, {'J', 11}, {'Q', 12}, {'K', 13}, {'A', 14}
    };

    protected Hand(string input)
    {
        var parts = input.Split(' ');
        Cards = parts[0];
        Bid = int.Parse(parts[1]);
        
        CardGroups = Cards.GroupBy(c => c)
                          .Select(group => new KeyValuePair<char, int>(group.Key, group.Count()))
                          .OrderByDescending(group => group.Value)
                          .ThenByDescending(group => _cardValues[group.Key])
                          .ToList();
        
        Type = DetermineHandTypeWithoutJoker();
    }

    protected HandType DetermineHandTypeWithoutJoker()
    {
        {
            if (CardGroups.Any(g => g.Value == 5)) return HandType.FiveOfAKind;
            if (CardGroups.Any(g => g.Value == 4)) return HandType.FourOfAKind;
            if (CardGroups.Count == 2 && CardGroups.Any(g => g.Value == 3)) return HandType.FullHouse;
            if (CardGroups.Any(g => g.Value == 3) && CardGroups.Count >= 3) return HandType.ThreeOfAKind;
            if (CardGroups.Count(g => g.Value == 2) == 2) return HandType.TwoPair;
            if (CardGroups.Count == 4 && CardGroups.Any(g => g.Value == 2)) return HandType.OnePair;
            return HandType.HighCard;
        }
    }
}