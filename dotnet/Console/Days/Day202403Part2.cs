using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class Day202403Part2 : AbstractDays
{
    bool multiply = true;
    public override void DoFinalThings()
    {
    }

    public override void DoLoopThings(string line)
    {
        // var lineSplitDo = line.Split("do()").SelectMany(s =>
        // {
        //     var endIndex = s.IndexOf("don't()");
        //     Console.ForegroundColor = ConsoleColor.Green;
        //     Console.Write("do()");
        //     Console.Write(s.Substring(0, endIndex == -1 ? s.Length : endIndex));
        //     Console.ForegroundColor = ConsoleColor.Red;
        //     Console.Write(s.Substring(endIndex == -1 ? s.Length : endIndex));
        //     return Regex.Matches(s.Substring(0, endIndex == -1 ? s.Length : endIndex), "mul\\([0-9]{1,3},[0-9]{1,3}\\)");
        // });
        // var lineSplitDo2 = line.Split("do()").Select(s =>
        // {
        //     var endIndex = s.IndexOf("don't()");
        //     return s.Substring(0, endIndex == -1 ? s.Length : endIndex);
        // });
        // Console.WriteLine("");
        // // var matches = Regex.Matches(line, "(do\\(\\)).*mul\\([0-9]{1,3},[0-9]{1,3}\\)|(don't\\(\\)).*mul\\([0-9]{1,3},[0-9]{1,3}\\)|mul\\([0-9]{1,3},[0-9]{1,3}\\)");
        // // foreach (Match match in lineSplitDo)
        // // {
        // //     var factorMatches = Regex.Matches(match.Value, "[0-9]{1,3}");
        // //     Result += int.Parse(factorMatches.ElementAt(0).Value) * int.Parse(factorMatches.ElementAt(1).Value);
        // // }
        // foreach (string lineDo in lineSplitDo2)
        // {
        //     Console.ForegroundColor = ConsoleColor.Green;
        //     Console.WriteLine(lineDo);
        //     Console.ForegroundColor = ConsoleColor.Blue;
        //     var matches = Regex.Matches(lineDo, "mul\\([0-9]{1,3},[0-9]{1,3}\\)");
        //     Console.WriteLine(String.Join(";", matches.Select(m => m.Value)));
        //     foreach (Match match in matches)
        //     {
        //         // var splitFactors = match.Value.Replace("mul(", "").Replace(")","").Split(",").Select(s => int.Parse(s)).ToArray();
        //         // Result += splitFactors[0] * splitFactors[1];
        //         var factorMatches = Regex.Matches(match.Value, "[0-9]{1,3}");
        //         Result += int.Parse(factorMatches.ElementAt(0).Value) * int.Parse(factorMatches.ElementAt(1).Value);
        //     }
        // }


        //TODO: Rewrite own solution above to only have implicit "do()" at the begin of the program not the line
        var matches = Regex.Matches(line, "mul\\([0-9]{1,3},[0-9]{1,3}\\)|do\\(\\)|don't\\(\\)");

        foreach (Match match in matches)
        {
            // var splitFactors = match.Value.Replace("mul(", "").Replace(")","").Split(",").Select(s => int.Parse(s)).ToArray();
            // Result += splitFactors[0] * splitFactors[1];
            if (match.Value == "do()")
            {
                multiply = true;
            }
            else if (match.Value == "don't()")
            {
                multiply = false;
            }
            else
            {
                if (multiply)
                {
                    var factorMatches = Regex.Matches(match.Value, "[0-9]{1,3}");
                    Result += int.Parse(factorMatches.ElementAt(0).Value) * int.Parse(factorMatches.ElementAt(1).Value);
                }
            }
        }
    }
}