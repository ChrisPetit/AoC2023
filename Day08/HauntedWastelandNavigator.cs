using System.Numerics;

namespace Day08;

public static class HauntedWastelandNavigator
{
    public static long StepsToReachDestination(IEnumerable<string> nodes, string instructions)
    {
        return Walk(ParseMap(nodes, instructions), "AAA");
    }
    
    public static long StepsToReachAllDestinations(IEnumerable<string> nodes, string instructions)
    {
        var map = ParseMap(nodes, instructions);
        var startingNodes = map.network.Keys.Where(k => k[^1] == 'A');
        var steps = startingNodes.Select(n => Walk(map, n));

        return Lcm(steps);
    }
    
    private static long Walk((int[] instructions, Dictionary<string, string[]> network) map, string node)
    {
        var (instructions, network) = map;
        var steps = 0L;

        while (node[^1] != 'Z')
        {
            node = network[node][instructions[steps % instructions.Length]];
            steps++;
        }

        return steps;
    }
    
    private static (int[] instructions, Dictionary<string, string[]> network) ParseMap(IEnumerable<string> nodes, string instructionLine)
    {
        var instructions = instructionLine.Select(c => c == 'L' ? 0 : 1).ToArray();
        var network = new Dictionary<string, string[]>();

        foreach (var line in nodes)
        {
            var parts = line.Split(" = ");
            var label = parts[0];
            var connections = parts[1].Trim('(', ')').Split(", ");
            network.Add(label, connections);
        }

        return (instructions, network);
    }
    
    private static long Lcm(IEnumerable<long> numbers)
    {
        return (long) numbers.Aggregate(BigInteger.One,
            (accumulated, next) => accumulated * next / BigInteger.GreatestCommonDivisor(accumulated, next));
    }
}