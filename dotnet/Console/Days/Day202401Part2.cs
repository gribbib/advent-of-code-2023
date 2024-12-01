using System;
using System.Collections.Generic;

public class Day202401Part2 : AbstractDays
{
    Dictionary<long, int> leftIds = new Dictionary<long, int>();
    Dictionary<long, int> rightIds = new Dictionary<long, int>();
    public override void DoFinalThings()
    {
        foreach (var item in leftIds)
        {
            Result += item.Key * item.Value * (rightIds.ContainsKey(item.Key) ? rightIds[item.Key] : 0);
        }
    }

    public override void DoLoopThings(string line)
    {
        var lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        long leftId = long.Parse(lineSplit[0]);
        long rightId = long.Parse(lineSplit[1]);
        if (leftIds.ContainsKey(leftId))
        {
            leftIds[leftId]++;
        }
        else
        {
            leftIds.Add(leftId, 1);
        }

        if (rightIds.ContainsKey(rightId))
        {
            rightIds[rightId]++;
        }
        else
        {
            rightIds.Add(rightId, 1);
        }
    }
}