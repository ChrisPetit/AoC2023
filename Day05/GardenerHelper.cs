namespace Day05;
public class GardenerHelper
{
    private readonly List<long> _seeds;
    private readonly CategoryMap _seedToSoilMap;
    private readonly CategoryMap _soilToFertilizerMap;
    private readonly CategoryMap _fertilizerToWaterMap;
    private readonly CategoryMap _waterToLightMap;
    private readonly CategoryMap _lightToTemperatureMap;
    private readonly CategoryMap _temperatureToHumidityMap;
    private readonly CategoryMap _humidityToLocationMap;

    public GardenerHelper(
        List<long> seeds,
        CategoryMap seedToSoilMap,
        CategoryMap soilToFertilizerMap,
        CategoryMap fertilizerToWaterMap,
        CategoryMap waterToLightMap,
        CategoryMap lightToTemperatureMap,
        CategoryMap temperatureToHumidityMap,
        CategoryMap humidityToLocationMap)
    {
        _seeds = seeds;
        _seedToSoilMap = seedToSoilMap;
        _soilToFertilizerMap = soilToFertilizerMap;
        _fertilizerToWaterMap = fertilizerToWaterMap;
        _waterToLightMap = waterToLightMap;
        _lightToTemperatureMap = lightToTemperatureMap;
        _temperatureToHumidityMap = temperatureToHumidityMap;
        _humidityToLocationMap = humidityToLocationMap;
    }

    public long FindLowestLocation()
    {
        var locations = (from seed in _seeds
            select _seedToSoilMap.GetMappedValue(seed)
            into soil
            select _soilToFertilizerMap.GetMappedValue(soil)
            into fertilizer
            select _fertilizerToWaterMap.GetMappedValue(fertilizer)
            into water
            select _waterToLightMap.GetMappedValue(water)
            into light
            select _lightToTemperatureMap.GetMappedValue(light)
            into temperature
            select _temperatureToHumidityMap.GetMappedValue(temperature)
            into humidity
            select _humidityToLocationMap.GetMappedValue(humidity)).ToList();

        return locations.Min();
    }
    
    public long FindLowestLocationFromRanges()
    {
        _seeds.Order();
        var values = _seeds.AsParallel().Select(GetLocationForSeed).ToList();
        var minValue = values.Min();
        return minValue;
    }


    private long GetLocationForSeed(long seed)
    {
        var soil = _seedToSoilMap.GetMappedValue(seed);
        var fertilizer = _soilToFertilizerMap.GetMappedValue(soil);
        var water = _fertilizerToWaterMap.GetMappedValue(fertilizer);
        var light = _waterToLightMap.GetMappedValue(water);
        var temperature = _lightToTemperatureMap.GetMappedValue(light);
        var humidity = _temperatureToHumidityMap.GetMappedValue(temperature);
        return _humidityToLocationMap.GetMappedValue(humidity);
    }

}   
