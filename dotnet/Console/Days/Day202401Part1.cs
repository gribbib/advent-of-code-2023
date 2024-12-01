using System;
using System.Collections.Generic;

public class Day202401Part1 : AbstractDays
{
    List<long> leftIds = new List<long>();
    List<long> rightIds = new List<long>();
    public override void DoFinalThings()
    {
        leftIds.Sort();
        rightIds.Sort();

        var length = leftIds.Count();
        for (int i = 0; i < length; i++)
        {
            Result += Math.Abs(rightIds.ElementAt(i) - leftIds.ElementAt(i));
        }
    }

    public override void DoLoopThings(string line)
    {
        var lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        leftIds.Add(long.Parse(lineSplit[0]));
        rightIds.Add(long.Parse(lineSplit[1]));
    }
}