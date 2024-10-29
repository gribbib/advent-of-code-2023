
using System.Data;
using System.Globalization;

public class Day7Part2 : AbstractDays
{
    List<Hand> hands = new List<Hand>();
    public override void DoFinalThings()
    {
        hands.Sort();
        int i = 1;
        Result = hands.Select(h => h.Points * i++).Sum();
    }

    public override void DoLoopThings(string line)
    {
        var split = line.Trim().Split(' ');
        hands.Add(new Hand(split[0], split[1], true));
    }
}