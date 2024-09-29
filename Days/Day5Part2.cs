
using System.Data;

class Day5Part2 : AbstractDays
{
    List<SeedRelations> seedIds = new List<SeedRelations>();
    private MapType lastMapType = MapType.Seed;
    MapType currentMapType = MapType.None;
    List<Mapping> mappings = new List<Mapping>();

    public override void DoFinalThings()
    {
        SetDestinationIds(lastMapType, currentMapType);
        Result = seedIds.Min(s => s.Ids[MapType.Location]);
    }

    public override void DoLoopThings(string line)
    {
        if (line.StartsWith("seeds: "))
        {
            line = line.Replace("seeds: ", "");
            string seedIdsString = line.Replace("  ", " ").Trim();
            long[] seedIdsAndRange = seedIdsString.Split(' ').Select(long.Parse).ToArray();

            for (long l = seedIdsAndRange[0]; l < seedIdsAndRange[0] + seedIdsAndRange[1]; l++)
            {
                seedIds.Add(new SeedRelations
                {
                    Ids = new Dictionary<MapType, long> { { MapType.Seed, l } }
                });
            }

            for (long l = seedIdsAndRange[2]; l < seedIdsAndRange[2] + seedIdsAndRange[3]; l++)
            {
                seedIds.Add(new SeedRelations
                {
                    Ids = new Dictionary<MapType, long> { { MapType.Seed, l } }
                });
            }
        }
        else if (String.IsNullOrWhiteSpace(line))
        {
            SetDestinationIds(lastMapType, currentMapType);
            lastMapType = currentMapType;
            currentMapType = MapType.None;
            mappings = new List<Mapping>();
        }
        else if (line.StartsWith("seed-to-soil map:"))
        {
            currentMapType = MapType.Soil;
        }
        else if (line.StartsWith("soil-to-fertilizer map:"))
        {
            currentMapType = MapType.Fertilizer;
        }
        else if (line.StartsWith("fertilizer-to-water map:"))
        {
            currentMapType = MapType.Water;
        }
        else if (line.StartsWith("water-to-light map:"))
        {
            currentMapType = MapType.Light;
        }
        else if (line.StartsWith("light-to-temperature map:"))
        {
            currentMapType = MapType.Temperature;
        }
        else if (line.StartsWith("temperature-to-humidity map:"))
        {
            currentMapType = MapType.Humidity;
        }
        else if (line.StartsWith("humidity-to-location map:"))
        {
            currentMapType = MapType.Location;
        }
        else
        {
            line = line.Replace("  ", " ").Trim();
            var mappingIds = line.Split(' ').Select(long.Parse).ToArray();
            mappings.Add(new Mapping { DestinationId = mappingIds[0], SourceId = mappingIds[1], Range = mappingIds[2] });
        }
    }

    private void SetDestinationIds(MapType sourceType, MapType destinationType)
    {
        foreach (var seedId in seedIds)
        {
            long searchId = seedId.Ids[sourceType];
            var mapping = mappings.Where(m => searchId >= m.SourceId && searchId < (m.SourceId + m.Range)).FirstOrDefault();
            if (mapping == null)
            {
                seedId.Ids.Add(destinationType, searchId);
            }
            else
            {
                seedId.Ids.Add(destinationType, mapping.DestinationId + (searchId - mapping.SourceId));
            }
        }
    }
}