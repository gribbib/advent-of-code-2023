using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Day202402Part1 : AbstractDays
{
    public override void DoFinalThings()
    {
    }

    public override void DoLoopThings(string line)
    {
        var lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => long.Parse(s)).ToArray();
        bool compareValueSet = false;
        int initialCompareValue = 0;
        for (int i = 0; i < lineSplit.Length - 1; i++)
        {
            var diff = Math.Abs(lineSplit[i] - lineSplit[i + 1]);
            if (diff == 0 || diff > 3)
            {
                return;
            }

            var compareValue = lineSplit[i].CompareTo(lineSplit[i + 1]);
            if (compareValueSet)
            {
                if (initialCompareValue != compareValue)
                {
                    return;
                }
            }
            else
            {
                initialCompareValue = compareValue;
                compareValueSet = true;
            }
        }
        Result ++;
    }
}