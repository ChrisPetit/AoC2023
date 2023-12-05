namespace Day04;
public static class ScratchcardProcessor
{
    public static int ProcessCards(List<(List<int> winningNumbers, List<int> playerNumbers)> cards)
    {
        var totalCards = 0;
        var toProcess = new Queue<int>(); // Queue for cards to be processed

        // Initial queueing of all cards
        for (var i = 0; i < cards.Count; i++)
        {
            toProcess.Enqueue(i);
        }

        while (toProcess.Count > 0)
        {
            var currentIndex = toProcess.Dequeue();
            totalCards++;

            var currentCard = cards[currentIndex];
            var matches = currentCard.winningNumbers.Intersect(currentCard.playerNumbers).Count();

            // Queue additional cards based on matches
            for (var j = 1; j <= matches; j++)
            {
                var nextIndex = currentIndex + j;
                if (nextIndex < cards.Count)
                {
                    toProcess.Enqueue(nextIndex);
                }
            }
        }

        return totalCards;
    }
}


