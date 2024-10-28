
using System.Data;
using System.Globalization;
using Microsoft.VisualBasic;

class Day5Part2 : AbstractDays
{
    SeedRelations seedRelations = new SeedRelations();

    public override void DoFinalThings()
    {
        seedRelations.SetDestinationIds();
        Result = seedRelations.GetMinimumLocation();
    }

    public override void DoLoopThings(string line)
    {
        seedRelations.HandleLine(line, true);
    }
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

class Mapping
{
    public RangeInformation Destination { get; set; } = new RangeInformation();
    public RangeInformation Source { get; set; } = new RangeInformation();
}