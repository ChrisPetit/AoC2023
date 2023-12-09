namespace Day09;

public static class OasisAndSandInstabilitySensor
{
    public static (long predicion, long backwardPredicion) PredictionOfNextValues(IEnumerable<string> input)
    {   var dataSets = new List<List<List<long>>>();
        foreach (var line in input)
        {
            var dataSet = new List<List<long>>();
            var initialNumbers = line.Split(" ").Select(long.Parse).ToList();
            dataSet.Add(initialNumbers);
            var numbers = initialNumbers;
            do
            {
                numbers = DifferenceAtEachStep(numbers);
                dataSet.Add(numbers);
            } while (numbers.Any(n => n != 0));
            dataSets.Add(dataSet);
        }
        
        var predictions = new List<long>();
        foreach (var dataSet in dataSets)
        {
            var prediction = PredictionOfNextValue(dataSet);
            predictions.Add(prediction);
        }

        var backwardsPredictions = new List<long>();
        foreach (var dataSet in dataSets)
        {
            var backwardsPrediction = PredictionOfPreviousValue(dataSet);
            backwardsPredictions.Add(backwardsPrediction);
        }

        return (predictions.Sum(), backwardsPredictions.Sum());
    }

    private static List<long> DifferenceAtEachStep(List<long> numbers)
    {
        var differences = new List<long>();
        for (var i = 0; i < numbers.Count - 1; i++)
        {
            differences.Add(numbers[i + 1] - numbers[i]);
        }

        return differences;
    }

    private static long PredictionOfNextValue(List<List<long>> values)
    {
        for (var i = values.Count - 1; i >= 0; i--)
        {
            var currentValues = values[i];
            if (currentValues.All(v => v == 0))
            {
                currentValues.Add(0);
                continue;
            }
            var previousValues = values[i + 1];
            var predictedValue = currentValues.Last() + previousValues.Last();
            currentValues.Add(predictedValue);
        }
        
        return values[0].Last();
    }

    private static long PredictionOfPreviousValue(List<List<long>> values)
    {
        for (var i = values.Count - 1; i >= 0; i--)
        {
            var currentValues = values[i];
            if (currentValues.All(v => v == 0))
            {
                currentValues.Insert(0, 0);
                continue;
            }
            var previousValues = values[i + 1];
            var predictedValue = currentValues.First() - previousValues.First();
            currentValues.Insert(0, predictedValue);
        }
        
        return values[0].First();
    }
}