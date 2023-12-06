using System.Text;

namespace Day06;

public static class BoatRaceParser
{
    public static List<(int raceTime, int recordDistance)> ParseInputFromFile(string input)
    {
        var lines = input.Split('\n');

        var raceTimes = ExtractNumbersFromLine(lines[0]);
        var recordDistances = ExtractNumbersFromLine(lines[1]);

        if (raceTimes.Count != recordDistances.Count)
            throw new FormatException("Number of race times and record distances must be equal");

        return raceTimes.Zip(recordDistances, (t, d) => (t, d)).ToList();
    }

    public static (long raceTime, long recordDistance) ParseSingleRaceInput(string input)
    {
        var lines = input.Split('\n');

        var raceTime = RemoveSpaces(lines[0]);
        var recordDistance = RemoveSpaces(lines[1]);

        return (raceTime, recordDistance);
    }

    private static long RemoveSpaces(string line)
    {
        var sb = new StringBuilder();
        var parts = line.Split(":");
        if (parts.Length != 2) throw new FormatException("Line format is incorrect");
        var i = parts[1].Split(" ");
        foreach (var s in i)
        {
            sb.Append(s).Replace(" ", "");
        }

        return long.Parse(sb.ToString());
    }

    private static List<int> ExtractNumbersFromLine(string line)
    {
        return line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
            .Where(part => int.TryParse(part, out _))
            .Select(int.Parse)
            .ToList();
    }
}