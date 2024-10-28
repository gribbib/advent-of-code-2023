
using System.Data;
using System.Globalization;

public class Day6Part2 : AbstractDays
{
    long[] times, distances;
    public override void DoFinalThings()
    {
        if (times.Length != distances.Length)
        {
            Result = -1;
            return;
        }

        Result = 1;

        for (long i = 0; i < times.Length; i++)
        {
            long resultCounter = 0;
            for (long t = 1; t <= (times[i]-1)/2; t++)
            {
                long restTime = times[i] - t;
                long speed = t;
                long distance = restTime * speed;

                if (distance > distances[i])
                {
                    resultCounter++;
                }
            }

            resultCounter *= 2;

            if (times[i]%2 == 0){
                long restTime = times[i]/2;
                long speed = times[i]/2;
                long distance = restTime * speed;

                if (distance > distances[i])
                {
                    resultCounter++;
                }
            }

            if (resultCounter > 0)
            {
                Result *= resultCounter;
            }
        }
        // Result = -1;
    }

    public override void DoLoopThings(string line)
    {
        if (line.StartsWith("Time:"))
        {
            line = line.Replace("Time:", "").Trim().Replace(" ", "");
            times = [long.Parse(line)];
        }
        else
        {
            line = line.Replace("Distance:", "").Trim().Replace(" ", "");
            distances = [long.Parse(line)];
        }
    }
}