
using System.Data;
using System.Globalization;

class Day5Part2 : AbstractDays
{
    List<SeedRelations2> FoundIdsForInitialSeeds = new List<SeedRelations2>();
    private MapType lastMapType = MapType.Seed;
    MapType currentMapType = MapType.None;
    List<Mapping2> mappings = new List<Mapping2>();

    public override void DoFinalThings()
    {
        SetDestinationIds(lastMapType, currentMapType);
        // Result = seedIds.Min(s => s.Ids.Where(t => t.MapType == MapType.Location).SelectMany(t => t.StartId));
        Result = FoundIdsForInitialSeeds.SelectMany(s => s.FoundMapTypeDestinationIdRanges.Where(t => t.MapType == MapType.Location)).Min(s => s.StartId);
    }

    public override void DoLoopThings(string line)
    {
        // Get initial seed information
        if (line.StartsWith("seeds: "))
        {
            line = line.Replace("seeds: ", "");
            string seedIdsString = line.Replace("  ", " ").Trim();
            long[] seedIdsAndRange = seedIdsString.Split(' ').Select(long.Parse).ToArray();

            for (long i = 0; i < seedIdsAndRange.Length; i += 2)
            {
                FoundIdsForInitialSeeds.Add(new SeedRelations2
                {
                    FoundMapTypeDestinationIdRanges = new List<RangeInformation>()
                    {
                        new RangeInformation
                        {
                            MapType = MapType.Seed,
                            StartId = seedIdsAndRange[i],
                            Range = seedIdsAndRange[i + 1]
                        }
                    }
                });
            }
        }
        // Type is set and mapping is completed
        else if (String.IsNullOrWhiteSpace(line))
        {
            SetDestinationIds(lastMapType, currentMapType);
            lastMapType = currentMapType;
            currentMapType = MapType.None;
            mappings = new List<Mapping2>();
        }
        // Type needs to be set for which the next mapping will be done
        //TODO: Error handling
        else if (line.Contains(" map:")){
            currentMapType = Enum.Parse<MapType>(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(line
                .Replace(" map:", "")
                .Substring(line.LastIndexOf("-")+1)
                .ToLower()));
        }
        // mapping information for the set type are parsed and stored
        else
        {
            line = line.Replace("  ", " ").Trim();
            var mappingIds = line.Split(' ').Select(long.Parse).ToArray();
            mappings.Add(new Mapping2
            {
                Destination = new RangeInformation { StartId = mappingIds[0], Range = mappingIds[2], MapType = currentMapType },
                Source = new RangeInformation { StartId = mappingIds[1], Range = mappingIds[2], MapType = lastMapType }
            });
        }
    }

    private void SetDestinationIds(MapType sourceType, MapType destinationType)
    {
        foreach (var seedId in FoundIdsForInitialSeeds)
        {
            var newMappingIds = new List<RangeInformation>();
            var searchIds = seedId.FoundMapTypeDestinationIdRanges.Where(s => s.MapType == sourceType);
            foreach (var searchId in searchIds)
            {
                newMappingIds.AddRange(NewMethod(destinationType, seedId, searchId));
            }
            seedId.FoundMapTypeDestinationIdRanges.AddRange(newMappingIds);
        }
    }

    private List<RangeInformation> NewMethod(MapType destinationType, SeedRelations2 seedId, RangeInformation searchRange)
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
                    MapType = destinationType,
                    StartId = searchRange.StartId,
                    Range = searchRange.Range
                });
            }
            else
            {
                // split of the part before and add source id as destination ids
                // check if range is sufficient and split if not
                var (leftRangeInformation, rightRangeInformation) = searchRange.Split(mappingMappingIdChecked.Source.StartId);
                leftRangeInformation.MapType = destinationType;
                newMappingIds.Add(leftRangeInformation);

                if (mappingMappingIdChecked.Source.EndId < rightRangeInformation.EndId)
                {
                    // do same Id check for split range
                    newMappingIds.AddRange(NewMethod(destinationType, seedId, rightRangeInformation));
                }
                else
                {
                    // Add new destination type because all source Ids are within the range so no extra check required
                    newMappingIds.Add(new RangeInformation
                    {
                        MapType = destinationType,
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
                    MapType = destinationType,
                    StartId = mappingSearchedIdChecked.Destination.StartId + (searchRange.StartId - mappingSearchedIdChecked.Source.StartId),
                    Range = leftRangeInformation.Range
                });
                // do same Id check for split range
                newMappingIds.AddRange(NewMethod(destinationType, seedId, rightRangeInformation));

            }
            else
            {
                newMappingIds.Add(new RangeInformation
                {
                    MapType = destinationType,
                    StartId = mappingSearchedIdChecked.Destination.StartId + (searchRange.StartId - mappingSearchedIdChecked.Source.StartId),
                    Range = searchRange.Range
                });
            }
        }
        return newMappingIds;
    }
}

class SeedRelations2
{
    public List<RangeInformation> FoundMapTypeDestinationIdRanges { get; set; }
}

class RangeInformation
{
    public MapType MapType { get; set; }
    public long StartId { get; set; }
    public long Range { get; set; }
    public long EndId { get { return StartId + Range - 1; } }

    public bool StartIdInRange(RangeInformation otherRangeInformation)
    {
        return otherRangeInformation.StartId <= StartId && StartId <= otherRangeInformation.EndId;
    }

    public bool EndIdInRange(RangeInformation otherRangeInformation)
    {
        return otherRangeInformation.StartId <= EndId && EndId <= otherRangeInformation.EndId;
    }

    public (RangeInformation leftRangeInformation, RangeInformation rightRangeInformation) Split(long splitId)
    {
        var splitRangeLeft = splitId - StartId;
        RangeInformation leftRangeInformation = new RangeInformation
        {
            MapType = MapType,
            StartId = StartId,
            Range = splitRangeLeft
        };

        var rightRangeInformation = new RangeInformation
        {
            MapType = MapType,
            StartId = splitId,
            Range = Range - splitRangeLeft
        };
        return (leftRangeInformation, rightRangeInformation);
    }
}

class Mapping2
{
    public RangeInformation Destination { get; set; }
    public RangeInformation Source { get; set; }
}