public class Day9Part1 : AbstractDays
{
    public override void DoFinalThings()
    {
        // not needed
    }

    public override void DoLoopThings(string line)
    {
        var historySequence = line.Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(h => long.Parse(h)).ToArray();

        Result += GetEndingValues(historySequence);
    }

    public long GetEndingValues(long[] inputSequence)
    {
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
            return inputSequence[differenceLength];
        }
        else
        {
            return inputSequence[differenceLength] + GetEndingValues(differenceSequence);
        }
    }
}