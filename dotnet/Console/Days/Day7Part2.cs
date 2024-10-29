
using System.Data;
using System.Globalization;

public class Day7Part2 : AbstractDays
{
    List<Hand> hands = new List<Hand>();
    public override void DoFinalThings()
    {
        hands.Sort();
        int i = 1;
        //for bug hunting:
        // hands.ForEach(h => Console.WriteLine($"{h} * {i} = {h.Points * i}; {Convert.ToInt16(h.HandType)} {String.Join(" ", h.GetComparableIntValuesFromHand())}; {h.HandValue[0]} {h.HandValue[1]} {h.HandValue[2]} {h.HandValue[3]} {h.HandValue[4]}"));
        // i = 1;
        Result = hands.Select(h => h.Points * i++).Sum();
    }

    public override void DoLoopThings(string line)
    {
        var split = line.Trim().Split(' ');
        hands.Add(new Hand(split[0], split[1], true));
    }
}