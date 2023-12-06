namespace Day04;

public static class ScratchcardParser
{
    public static (List<int> winningNumbers, List<int> playerNumbers) ParseCard(string cardData)
    {
        var parts = cardData
            .Split('|')
            .Select(part => part.Trim()).ToArray();
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid card data format.");
        }

        var winningNumbers = parts[0]
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToList();
        var playerNumbers = parts[1]
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToList();

        return (winningNumbers, playerNumbers);
    }
}
