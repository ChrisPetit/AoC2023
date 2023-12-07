namespace Day07;

public class WildCardHand : Hand, IComparable<WildCardHand>
{
    private readonly Dictionary<char, int> _cardValues = new Dictionary<char, int>
    {
        {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7},
        {'8', 8}, {'9', 9}, {'T', 10}, {'Q', 12}, {'K', 13}, {'A', 14}, {'J', 1}
    };

    public WildCardHand(string input) : base(input)
    {
        var parts = input.Split(' ');
        Cards = parts[0];
        Bid = int.Parse(parts[1]);
        
        CardGroups = Cards.GroupBy(c => c)
            .Select(group => new KeyValuePair<char, int>(group.Key, group.Count()))
            .OrderByDescending(group => group.Value)
            .ThenByDescending(group => _cardValues[group.Key])
            .ToList();
        
        Type = DetermineBestHandWithJoker();
    }

    public int CompareTo(WildCardHand? other)
    {
        if (Type != other!.Type)
        {
            return other.Type.CompareTo(Type); // Higher hand type ranks higher
        }
        
        // Compare each card in sequence
        for (var i = 0; i < Cards.Length; i++)
        {
            var thisCardValue = _cardValues[Cards[i]];
            var otherCardValue = _cardValues[other.Cards[i]];

            if (thisCardValue != otherCardValue)
            {
                return otherCardValue.CompareTo(thisCardValue); // Higher card value ranks higher
            }
        }

        return 0; // If all criteria are equal, consider hands equal
    }

    private HandType DetermineBestHandWithJoker()
    {
        var jokerCount = Cards.Count(c => c == 'J');
        if (jokerCount == 0) return DetermineHandTypeWithoutJoker(); // If no Joker, evaluate normally
        if (jokerCount == 5) return HandType.FiveOfAKind; // All cards are Jokers
        
        var cardCounts = Cards.Where(c => c != 'J')
            .GroupBy(c => c)
            .Select(group => new { Card = group.Key, Count = group.Count() })
            .OrderByDescending(group => group.Count)
            .ToList();

        // Determine the best possible upgrade
        if (cardCounts.Count == 0) return HandType.OnePair; // All cards are Jokers
        if (cardCounts[0].Count + jokerCount == 5) return HandType.FiveOfAKind;
        if (cardCounts[0].Count + jokerCount == 4) return HandType.FourOfAKind;
        if (cardCounts[0].Count == 3 && jokerCount == 1) return HandType.FourOfAKind;
        if (cardCounts[0].Count == 2 && jokerCount == 2) return HandType.FourOfAKind;
        if (cardCounts[0].Count == 2 && jokerCount == 1 && cardCounts.Count == 2) return HandType.FullHouse;
        if (cardCounts[0].Count == 2 && jokerCount == 1 && cardCounts.Count == 3) return HandType.ThreeOfAKind;
        if (cardCounts[0].Count == 1 )
        {
            return jokerCount switch
            {
                >= 3 => HandType.FourOfAKind,
                2 => HandType.ThreeOfAKind,
                1 when cardCounts.Count > 1 => HandType.OnePair,
                _ => HandType.OnePair
            };
        }
        if (cardCounts.Count >= 2 && jokerCount + cardCounts.Count >= 4) return HandType.TwoPair;
        return HandType.OnePair;
    }
}