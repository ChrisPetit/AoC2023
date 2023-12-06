namespace Day06;

public class Runner
{
    public static void RunDay06()
    {
        var input = File.ReadAllText("input/Day06.txt");
        var races = BoatRaceParser.ParseInputFromFile(input);
        var totalWaysToWin = BoatRace.TotalWaysToWin(races);
        Console.WriteLine($"Total Ways to Win: {totalWaysToWin}");
        
        var (raceTime, recordDistance) = BoatRaceParser.ParseSingleRaceInput(input);
        var waysToWin = BoatRace.CalculateWaysToWinSingleRace(raceTime, recordDistance);
        Console.WriteLine($"Total Ways to Win: {waysToWin}");
    }
}