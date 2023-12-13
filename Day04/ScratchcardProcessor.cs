using System.Collections;

namespace Day04;
public static class ScratchcardProcessor
{
    public static int ProcessCards(List<(List<int> winningNumbers, List<int> playerNumbers)> cards)
    {
        var processedCardsCount = 0;
        var cardsToProcess = new Queue<int>(); // Queue for cards to be processed

        // Initial queueing of all cards
        for (var i = 0; i < cards.Count; i++)
        {
            cardsToProcess.Enqueue(i);
        }

        while (cardsToProcess.Count > 0)
        {
            var currentIndex = cardsToProcess.Dequeue();
            processedCardsCount++;

            var currentCard = cards[currentIndex];
            var matches = currentCard.winningNumbers.Count(x => currentCard.playerNumbers.Contains(x));
            
            // Queue additional cards based on matches
            EnqueueBasedOnMatches(cards, currentIndex, matches, cardsToProcess);
        }

        return processedCardsCount;
    }
    
    private static void EnqueueBasedOnMatches(ICollection cards, int currentIndex, 
        int matches, Queue<int> cardsToProcess)
    {
        // this new function handles enqueueing of cards based on the number of matches
        for (var j = 1; j <= matches; j++)
        {
            var nextIndex = currentIndex + j;
            if (nextIndex < cards.Count)
            {
                cardsToProcess.Enqueue(nextIndex);
            }
        }
    }
}


