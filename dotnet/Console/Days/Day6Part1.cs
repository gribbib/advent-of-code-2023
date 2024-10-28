
using System.Data;
using System.Globalization;

public class Day6Part1 : AbstractDays
{
    int[] times, distances;
    public override void DoFinalThings()
    {
        if (times.Length != distances.Length)
        {
            Result = -1;
            return;
        }

        Result = 1;

        for (int i = 0; i < times.Length; i++)
        {
            int resultCounter = 0;
            for (int t = 1; t <= (times[i]-1)/2; t++)
            {
                int restTime = times[i] - t;
                int speed = t;
                int distance = restTime * speed;

                if (distance > distances[i])
                {
                    resultCounter++;
                }
            }

            resultCounter *= 2;

            if (times[i]%2 == 0){
                int restTime = times[i]/2;
                int speed = times[i]/2;
                int distance = restTime * speed;

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
            line = line.Replace("Time:", "").Trim();
            times = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
        else
        {
            line = line.Replace("Distance:", "").Trim();
            distances = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
    }
}