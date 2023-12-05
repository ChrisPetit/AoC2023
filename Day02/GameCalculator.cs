using System.Text.RegularExpressions;

namespace Day02;

public static class GameCalculator
{
    public static int CalculateTotalPower(List<string> gameRecords)
    {
        var totalPower = 0;

        foreach (var record in gameRecords)
        {
            var (red, green, blue) = GetMinimumCubes(record);
            totalPower += red * green * blue;
        }

        return totalPower;
    }

    private static (int red, int green, int blue) GetMinimumCubes(string record)
    {
        var cubeRegex = new Regex(@"(\d+) (red|green|blue)");
        int maxRed = 0, maxGreen = 0, maxBlue = 0;

        var sets = record.Split(':')[1].Split(';');
        foreach (var set in sets)
        {
            int red = 0, green = 0, blue = 0;
            var matches = cubeRegex.Matches(set);

            foreach (Match match in matches)
            {
                var count = int.Parse(match.Groups[1].Value);
                var color = match.Groups[2].Value;

                switch (color)
                {
                    case "red":
                        red += count;
                        break;
                    case "green":
                        green += count;
                        break;
                    case "blue":
                        blue += count;
                        break;
                }
            }

            maxRed = Math.Max(maxRed, red);
            maxGreen = Math.Max(maxGreen, green);
            maxBlue = Math.Max(maxBlue, blue);
        }

        return (maxRed, maxGreen, maxBlue);
    }
}