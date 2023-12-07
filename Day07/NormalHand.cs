namespace Day07;

public class NormalHand : Hand, IComparable<NormalHand>
{
    private readonly Dictionary<char, int> _cardValues = new Dictionary<char, int>
    {
        {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7},
        {'8', 8}, {'9', 9}, {'T', 10}, {'J', 11}, {'Q', 12}, {'K', 13}, {'A', 14}
    };

    public NormalHand(string input) : base(input)
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

    public int CompareTo(NormalHand? other)
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
}