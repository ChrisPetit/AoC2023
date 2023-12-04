using System.Text.RegularExpressions;

namespace Day01;

public class Processor
{
    public static void RunDay01()
    {
        var input = File.ReadAllLines("input/day01.txt");
        var result = input.Select(CalculateCalibrationValue).ToList();
        Console.WriteLine(result.Sum());
        var result2 = input.Select(CalculateNewCalibrationValue).ToList();
        Console.WriteLine(result2.Sum());
    }
    
    public static int CalculateCalibrationValue(string input)
    {
        var list = input.Where(char.IsDigit).ToList();

        var a = list.First().ToString() + list.Last();

        return int.Parse(a);
    }
    
    // Mapping of spelled-out numbers to their digit equivalents
    

    public static int CalculateNewCalibrationValue(string input)
    {
        // select either the first digit or the first spelled out number
        var number1 = Regex.Match(input, @"\d|one|two|three|four|five|six|seven|eight|nine", RegexOptions.IgnoreCase).Value;
        var reversedInput = new string(input.Reverse().ToArray());
        var number2 = Regex.Match(reversedInput, @"\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin", RegexOptions.IgnoreCase).Value;
        
        var a = GetNumber(number1) + GetNumber(number2).ToString();

        return int.Parse(a);
    }
    
    private static int GetNumber(string input)
    {
        var wordsToNumbers = new Dictionary<string, int>
        {
            {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4},
            {"five", 5}, {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9},
            {"eno", 1}, {"owt", 2}, {"eerht", 3}, {"ruof", 4},
            {"evif", 5}, {"xis", 6}, {"neves", 7}, {"thgie", 8}, {"enin", 9}
        };
        
        if (int.TryParse(input, out var number))
        {
            return number;
        }
        return wordsToNumbers.TryGetValue(input, out number) ? number : 0;
    }
}