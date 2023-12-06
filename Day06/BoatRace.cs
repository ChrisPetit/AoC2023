namespace Day06;

public static class BoatRace
{
    private static int CalculateWaysToWin(int raceTime, int recordDistance)
    {
        var waysToWin = 0;
        for (var holdTime = 1; holdTime < raceTime; holdTime++)
        {
            var travelTime = raceTime - holdTime;
            var distance = holdTime * travelTime;
            if (distance > recordDistance)
            {
                waysToWin++;
            }
        }
        return waysToWin;
    }

    public static long TotalWaysToWin(IEnumerable<(int raceTime, int recordDistance)> races)
    {
        return races.Aggregate<(int raceTime, int recordDistance), long>(1,
            (current, race) => current * CalculateWaysToWin(race.raceTime, race.recordDistance));
    }
    
    public static long CalculateWaysToWinSingleRace(long raceTime, long recordDistance)
    {
        long waysToWin = 0;
        for (long holdTime = 1; holdTime < raceTime; holdTime++)
        {
            var travelTime = raceTime - holdTime;
            var distance = holdTime * travelTime;
            if (distance > recordDistance)
            {
                waysToWin++;
            }
        }
        return waysToWin;
    }
}