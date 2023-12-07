namespace Day07;

public static  class Runner
{
    public static void RunDay07()
    {
        var input = File.ReadAllLines("input/Day07.txt").ToList();
        var totalWinnings = CamelCardsGame.CalculateTotalWinnings(input);
        Console.WriteLine($"Total Winnings: {totalWinnings}");
        
        var totalWinningsWithWildCards = CamelCardsGame.CalculateTotalWinningsWithWildCards(input);
        Console.WriteLine($"Total Winnings With Wild Cards: {totalWinningsWithWildCards}");
    }
}