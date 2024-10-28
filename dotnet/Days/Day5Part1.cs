
using System.Data;
using System.Globalization;

class Day5Part1 : AbstractDays
{
    SeedRelations seedRelations = new SeedRelations();

    public override void DoFinalThings()
    {
        seedRelations.SetDestinationIds();
        Result = seedRelations.GetMinimumLocation();
    }

    public override void DoLoopThings(string line)
    {
        seedRelations.HandleLine(line, false);
    }
}

class SeedRelations
{
    List<List<RangeInformation>> FoundRangeInformationForMapTypes = new List<List<RangeInformation>>();
    private MapType lastMapType = MapType.Seed;
    MapType currentMapType = MapType.None;
    List<Mapping> mappings = new List<Mapping>();


    public void SetDestinationIds()
    {
        FoundRangeInformationForMapTypes.ForEach(seedId =>
        {
            // Parallel.ForEach(FoundRangeInformationForMapTypes, seedId => { //not faster in this scenario
            var newMappingIds = new List<RangeInformation>();
            var searchIds = seedId.Where(s => s.MapType == lastMapType);
            foreach (var searchId in searchIds)
            {
                newMappingIds.AddRange(GetIdMappingForCurrentMapType(seedId, searchId));
            }
            seedId.AddRange(newMappingIds);
        });
    }

    public void HandleLine(string line, bool seedsInRanges)
    {

        // Get initial seed information
        if (line.StartsWith("seeds: "))
        {
            line = line.Replace("seeds: ", "");
            string seedIdsString = line.Replace("  ", " ").Trim();
            long[] seedIdsAndRange = seedIdsString.Split(' ').Select(long.Parse).ToArray();
            if (seedsInRanges)
            {
                for (long i = 0; i < seedIdsAndRange.Length; i += 2)
                {
                    FoundRangeInformationForMapTypes.Add(new List<RangeInformation>()
                    {
                        new RangeInformation
                        {
                            MapType = MapType.Seed,
                            StartId = seedIdsAndRange[i],
                            Range = seedIdsAndRange[i + 1]
                        }
                });
                }
            }
            else
            {
                FoundRangeInformationForMapTypes = seedIdsAndRange.Select(s => new List<RangeInformation>()
                    {
                        new RangeInformation
                        {
                            MapType = MapType.Seed,
                            StartId = s,
                            Range = 1
                        }
                }).ToList();
            }
        }
        // Type is set and mapping is completed
        else if (String.IsNullOrWhiteSpace(line))
        {
            SetDestinationIds();
            lastMapType = currentMapType;
            currentMapType = MapType.None;
            mappings = new List<Mapping>();
        }
        // Type needs to be set for which the next mapping will be done
        //TODO: Error handling
        else if (line.Contains(" map:"))
        {
            currentMapType = Enum.Parse<MapType>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(line
                .Replace(" map:", "")
                .Substring(line.LastIndexOf("-") + 1)
                .ToLower()));
        }
        // mapping information for the set type are parsed and stored
        else
        {
            line = line.Replace("  ", " ").Trim();
            var mappingIds = line.Split(' ').Select(long.Parse).ToArray();
            mappings.Add(new Mapping
            {
                Destination = new RangeInformation { StartId = mappingIds[0], Range = mappingIds[2], MapType = currentMapType },
                Source = new RangeInformation { StartId = mappingIds[1], Range = mappingIds[2], MapType = lastMapType }
            });
        }
    }

    private List<RangeInformation> GetIdMappingForCurrentMapType(List<RangeInformation> seedId, RangeInformation searchRange)
    {
        var newMappingIds = new List<RangeInformation>();
        var mappingSearchedIdChecked = mappings.Where(m => searchRange.StartIdInRange(m.Source)).FirstOrDefault();
        if (mappingSearchedIdChecked == null)
        {
            // add check whether there is a destination id range that is part of the source destination range
            var mappingMappingIdChecked = mappings.Where(m => m.Source.StartIdInRange(searchRange)).FirstOrDefault();
            if (mappingMappingIdChecked == null)
            {
                newMappingIds.Add(new RangeInformation
                {
                    MapType = currentMapType,
                    StartId = searchRange.StartId,
                    Range = searchRange.Range
                });
            }
            else
            {
                // split of the part before and add source id as destination ids
                // check if range is sufficient and split if not
                var (leftRangeInformation, rightRangeInformation) = searchRange.Split(mappingMappingIdChecked.Source.StartId);
                leftRangeInformation.MapType = currentMapType;
                newMappingIds.Add(leftRangeInformation);

                if (mappingMappingIdChecked.Source.EndId < rightRangeInformation.EndId)
                {
                    // do same Id check for split range
                    newMappingIds.AddRange(GetIdMappingForCurrentMapType(seedId, rightRangeInformation));
                }
                else
                {
                    // Add new destination type because all source Ids are within the range so no extra check required
                    newMappingIds.Add(new RangeInformation
                    {
                        MapType = currentMapType,
                        StartId = mappingMappingIdChecked.Destination.StartId,
                        Range = rightRangeInformation.Range
                    });
                }
            }
        }
        else
        {
            // check if range is sufficient and split if not
            if (mappingSearchedIdChecked.Source.EndId < searchRange.EndId)
            {
                // split
                var (leftRangeInformation, rightRangeInformation) = searchRange.Split(mappingSearchedIdChecked.Source.EndId + 1);
                var splitRangeLeft = mappingSearchedIdChecked.Source.EndId - searchRange.EndId;
                newMappingIds.Add(new RangeInformation
                {
                    MapType = currentMapType,
                    StartId = mappingSearchedIdChecked.Destination.StartId + (searchRange.StartId - mappingSearchedIdChecked.Source.StartId),
                    Range = leftRangeInformation.Range
                });
                // do same Id check for split range
                newMappingIds.AddRange(GetIdMappingForCurrentMapType(seedId, rightRangeInformation));

            }
            else
            {
                newMappingIds.Add(new RangeInformation
                {
                    MapType = currentMapType,
                    StartId = mappingSearchedIdChecked.Destination.StartId + (searchRange.StartId - mappingSearchedIdChecked.Source.StartId),
                    Range = searchRange.Range
                });
            }
        }
        return newMappingIds;
    }

    public long GetMinimumLocation()
    {
        return FoundRangeInformationForMapTypes.SelectMany(s => s.Where(t => t.MapType == MapType.Location)).Min(s => s.StartId);
    }
}

enum MapType
{
    None, Seed, Soil, Fertilizer, Water, Light, Temperature, Humidity, Location
}