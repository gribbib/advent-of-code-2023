using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class Day202403Part1 : AbstractDays
{
    public override void DoFinalThings()
    {
    }

    public override void DoLoopThings(string line)
    {
        var matches = Regex.Matches(line, "mul\\([0-9]{1,3},[0-9]{1,3}\\)");
        foreach (Match match in matches)
        {
            // var splitFactors = match.Value.Replace("mul(", "").Replace(")","").Split(",").Select(s => int.Parse(s)).ToArray();
            // Result += splitFactors[0] * splitFactors[1];
            var factorMatches = Regex.Matches(match.Value, "[0-9]{1,3}");
            Result += int.Parse(factorMatches.ElementAt(0).Value) * int.Parse(factorMatches.ElementAt(1).Value);
        }
    }
}