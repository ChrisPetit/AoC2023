namespace Day04;
public class ScratchcardProcessor
{
    public static int ProcessCards(List<(List<int> winningNumbers, List<int> playerNumbers)> cards)
    {
        var totalCards = 0;
        var cardQueue = new List<int>(); // Queue to manage card processing

        for (var i = 0; i < cards.Count; i++)
        {
            var matches = cards[i].winningNumbers.Count(number => cards[i].playerNumbers.Contains(number));
            
            // Process the current card
            totalCards++;
            // For each match, add the next card to the queue
            for (var j = 1; j <= matches; j++)
            {
                if (i + j < cards.Count)
                {
                    cardQueue.Add(i + j);
                }
            }

            // Process copies in the queue
            while (cardQueue.Any())
            {
                var cardIndex = cardQueue[0];
                cardQueue.RemoveAt(0);

                matches = cards[cardIndex].winningNumbers.Count(number => cards[cardIndex].playerNumbers.Contains(number));
                totalCards++; // Count the copy

                for (var j = 1; j <= matches; j++)
                {
                    if (cardIndex + j < cards.Count)
                    {
                        cardQueue.Add(cardIndex + j);
                    }
                }
            }
        }

        return totalCards;
    }
}

