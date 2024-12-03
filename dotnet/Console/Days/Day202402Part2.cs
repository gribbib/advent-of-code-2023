using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Day202402Part2 : AbstractDays
{
    public List<string> LineWithSkip { get; set; } = new List<string>();

    public override void DoFinalThings()
    {
    }

    public override void DoLoopThings(string line)
    {
        var lineSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(s => long.Parse(s)).ToArray();
        if (!IsSafe(lineSplit))
        {
            for (int i = 0; i < lineSplit.Length; i++)
            {
                var slice = lineSplit[0..i].Concat(lineSplit[(i+1)..lineSplit.Length]).ToArray();
                if (IsSafe(slice)){
                    Result++;
                    return;
                }
            }
            return;


            // bool compareValueSet = false;
            // int initialCompareValue = 0;
            // bool levelRemoved = false;
            // bool addLineToOutput = false;
            // long firstCompareValue = lineSplit[0];
            // for (int i = 0; i < lineSplit.Length - 1; i++)
            // {
            //     int compareIndex = i + 1;

            //     var diff = Math.Abs(firstCompareValue - lineSplit[compareIndex]);
            //     var compareValue = firstCompareValue.CompareTo(lineSplit[compareIndex]);

            //     if (!compareValueSet)
            //     {
            //         initialCompareValue = compareValue;
            //         compareValueSet = true;
            //     }

            //     if (diff == 0 || diff > 3 || initialCompareValue != compareValue)
            //     {
            //         if (levelRemoved)
            //         {
            //             return;
            //         }

            //         levelRemoved = true;
            //         // i++;
            //         addLineToOutput = true;

            //         if (i == 0 && (diff == 0 || diff > 3))
            //         {
            //             diff = Math.Abs(firstCompareValue - lineSplit[i + 2]);
            //             compareValue = firstCompareValue.CompareTo(lineSplit[i + 2]);
            //             if (diff == 0 || diff > 3)
            //             {
            //                 firstCompareValue = lineSplit[i + 1];
            //                 compareValueSet = false;
            //             }
            //             else
            //             {
            //                 initialCompareValue = compareValue;
            //                 firstCompareValue = lineSplit[i + 2];
            //                 i++;
            //             }
            //         }

            //         if (i + 2 < lineSplit.Length)
            //         {
            //             diff = Math.Abs(firstCompareValue - lineSplit[i + 2]);
            //             if (diff == 0)
            //             {
            //                 firstCompareValue = lineSplit[i + 1];
            //             }
            //         }

            //         if (i == 1 && lineSplit.Length >= 4 && initialCompareValue != compareValue && compareValue != 0)
            //         {
            //             compareValue = firstCompareValue.CompareTo(lineSplit[i + 2]);

            //             if (initialCompareValue != compareValue)
            //             {
            //                 initialCompareValue = compareValue;
            //                 firstCompareValue = lineSplit[i + 1];
            //             }
            //             else
            //             {

            //                 if (diff > 3)
            //                 {
            //                     return; //more than one would have to be removed
            //                 }
            //             }
            //         }

            //         // compareIndex++;
            //         // if (compareIndex < lineSplit.Length)
            //         // {
            //         //     diff = Math.Abs(firstCompareValue - lineSplit[compareIndex]);
            //         //     compareValue = firstCompareValue.CompareTo(lineSplit[compareIndex]);
            //         //     if (diff == 0 || diff > 3 || initialCompareValue != compareValue)
            //         //     {
            //         //         if (currentIndex == 0 && (diff == 0 || diff > 3))
            //         //         {
            //         //             diff = Math.Abs(lineSplit[currentIndex + 1] - lineSplit[compareIndex]);
            //         //             if (diff == 0 || diff > 3)
            //         //             {
            //         //                 return;
            //         //             }
            //         //             else
            //         //             {
            //         //                 levelRemoved = true;
            //         //                 i++;
            //         //                 addLineToOutput = true;
            //         //             }
            //         //         }
            //         //         // if (currentIndex == 0 && initialCompareValue != compareValue)
            //         //         // {
            //         //         //     diff = Math.Abs(lineSplit[currentIndex + 1] - lineSplit[compareIndex]);
            //         //         //     if (diff == 0 || diff > 3)
            //         //         //     {
            //         //         //         return;
            //         //         //     }
            //         //         //     else
            //         //         //     {
            //         //         //         levelRemoved = true;
            //         //         //         i++;
            //         //         //         addLineToOutput = true;
            //         //         //     }
            //         //         // }
            //         //         else if ((currentIndex == 1 || currentIndex == 0) && initialCompareValue != compareValue)
            //         //         {
            //         //             initialCompareValue = compareValue;
            //         //             compareValueSet = true;
            //         //             levelRemoved = true;
            //         //             addLineToOutput = true;
            //         //             if (currentIndex == 0)
            //         //             {
            //         //                 i++;
            //         //             }
            //         //         }
            //         //         else
            //         //         {
            //         //             return;
            //         //         }
            //         //     }
            //         //     else
            //         //     {
            //         //         levelRemoved = true;
            //         //         i++;
            //         //         addLineToOutput = true;
            //         //     }
            //         // }
            //         // else
            //         // {
            //         //     levelRemoved = true;
            //         //     i++;
            //         //         addLineToOutput = true;
            //         // }
            //     }
            //     else
            //     {
            //         firstCompareValue = lineSplit[i + 1];
            //     }
            // }
            // if (addLineToOutput)
            // {
            //     LineWithSkip.Add(line);
            // }
        }
        Result++;
    }

    public bool IsSafe(long[] lineSplit)
    {

        bool compareValueSet = false;
        int initialCompareValue = 0;
        for (int i = 0; i < lineSplit.Length - 1; i++)
        {
            var diff = Math.Abs(lineSplit[i] - lineSplit[i + 1]);
            if (diff == 0 || diff > 3)
            {
                return false;
            }

            var compareValue = lineSplit[i].CompareTo(lineSplit[i + 1]);
            if (compareValueSet)
            {
                if (initialCompareValue != compareValue)
                {
                    return false;
                }
            }
            else
            {
                initialCompareValue = compareValue;
                compareValueSet = true;
            }
        }
        return true;
    }
}