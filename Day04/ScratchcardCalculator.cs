namespace Day04;

public static class ScratchcardCalculator
{
    public static int CalculatePoints(IEnumerable<int> winningNumbers, List<int> playerNumbers)
    {
        var points = 0;
        var matches = winningNumbers.Count(playerNumbers.Contains);

        if (matches > 0)
        {
            points = (int)Math.Pow(2, matches - 1);
        }

        return points;
    }
}
