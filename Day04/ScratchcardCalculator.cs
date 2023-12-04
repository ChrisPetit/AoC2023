namespace Day04;

public class ScratchcardCalculator
{
    public static int CalculatePoints(List<int> winningNumbers, List<int> playerNumbers)
    {
        var points = 0;
        var matches = winningNumbers.Count(number => playerNumbers.Contains(number));

        if (matches > 0)
        {
            points = (int)Math.Pow(2, matches - 1);
        }

        return points;
    }
}
