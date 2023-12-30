using System.Text;

namespace Day15;

public static class HASHalgorithm
{
    public static int Run(string initializationSequence)
    {
        var elements = initializationSequence.Split(",");

        var results = elements
            .Select(element => Encoding.ASCII
                .GetBytes(element.Trim()))
            .Select(bytes => bytes
                .Aggregate(0, (current, i) => ((i + current) * 17) % 256)).ToList();

        return results.Sum();
    }

    public static int HASHMAP(string initializationSequence)
    {
        var elements = initializationSequence.Split(",");
        var results = new List<int>();

        var boxes = new List<Tuple<string, int>>[256];

        InitializeBoxes(boxes);

        foreach (var element in elements)
        {
            var splitElement = element.Split('=', '-');
            var label = splitElement[0];
            
            var box = Run(label);
            if (element.Contains('='))
            {
                var index = boxes[box].FindIndex(x => x.Item1 == label);
                var focalLength = int.Parse(splitElement[1]);
                if (index != -1)
                {
                    boxes[box][index] = new Tuple<string, int>(label, focalLength);
                }
                else
                {
                    boxes[box].Add(new Tuple<string, int>(label, focalLength));
                }
            }
            else if (element.Contains('-'))
            {
                var lensToRemove = boxes[box].FirstOrDefault(x => x.Item1 == label);
                if (lensToRemove != null)
                {
                    boxes[box].Remove(lensToRemove);
                }
            }
        }

        for (var box = 0; box < boxes.Length; box++)
        {
            for (var slot = 0; slot < boxes[box].Count; slot++)
            {
                var lens = boxes[box][slot];
                var focalLength = lens.Item2;
                results.Add((box + 1) * (slot + 1) * focalLength);
            }
        }

        return results.Sum();
    }

    private static void InitializeBoxes(IList<List<Tuple<string, int>>> boxes)
    {
        for (var i = 0; i < boxes.Count; i++)
        {
            boxes[i] = new List<Tuple<string, int>>();
        }
    }
}