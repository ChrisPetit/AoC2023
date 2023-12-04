namespace Day04;

public class Runner
{
    public static void RunDay04()
    {
        var input = File.ReadAllLines("input/Day04.txt");
        var result = (from s in input select s.Split(":") into l select ScratchcardParser.ParseCard(l[1]) into parsed select ScratchcardCalculator.CalculatePoints(parsed.winningNumbers, parsed.playerNumbers)).Sum();

        Console.WriteLine(result);

        var cards = input.Select(s => ScratchcardParser.ParseCard(s.Split(":")[1])).ToList();
        var result2 = ScratchcardProcessor.ProcessCards(cards);
        Console.WriteLine(result2);
    }
}