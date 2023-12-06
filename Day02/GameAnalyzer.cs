using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day02;

public static class GameAnalyzer
{
    public static int CalculatePossibleGames(List<string> gameRecords, int redCubes, int greenCubes, int blueCubes)
    {
        var sumOfGameIds = 0;
        var cubeRegex = new Regex(@"(\d+) (red|green|blue)");

        foreach (var record in gameRecords)
        {
            var gameId = int.Parse(record.Split(':')[0].Replace("Game", "").Trim());
            var sets = record.Split(':')[1].Split(';');
            var isGamePossible = true;

            foreach (var set in sets)
            {
                var matches = cubeRegex.Matches(set);
                int red = 0, green = 0, blue = 0;

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

                if (red <= redCubes && green <= greenCubes && blue <= blueCubes) continue;
                isGamePossible = false;
                break;
            }

            if (isGamePossible)
            {
                sumOfGameIds += gameId;
            }
        }

        return sumOfGameIds;
    }
}
