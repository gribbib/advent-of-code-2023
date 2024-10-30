public class Day9Part2 : AbstractDays
{
    // following code left in for commenting my original solution
    long localResult;
    public override void DoFinalThings()
    {
        // not needed
    }

    public override void DoLoopThings(string line)
    {
        var historySequence = line.Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(h => long.Parse(h)).ToArray();
        // following code left in for commenting my original solution
        // localResult += GetBeginningValues(historySequence);
        Array.Reverse(historySequence);
        Result += Day9Part1.GetEndingValues(historySequence);
    }

    // my original solution before finding the hint with "Reverse()"
    public long GetBeginningValues(long[] inputSequence)
    {
#if DEBUG
        Console.WriteLine("[{0}]", string.Join(" ", inputSequence));
#endif
        var differenceLength = inputSequence.Length - 1;
        var differenceSequence = new long[differenceLength];
        bool allZeros = true;
        for (int i = 0; i < differenceLength; i++)
        {
            differenceSequence[i] = inputSequence[i + 1] - inputSequence[i];

            if (differenceSequence[i] != 0)
            {
                allZeros = false;
            }
        }

        if (allZeros)
        {
#if DEBUG
            Console.WriteLine("[{0}]", string.Join(" ", differenceSequence));
#endif
            return inputSequence[0];
        }
        else
        {
            return inputSequence[0] - GetBeginningValues(differenceSequence);
        }
    }
}