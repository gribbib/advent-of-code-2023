
using System.Data;
using System.Globalization;

public class Day8Part1 : AbstractDays
{
    string leftRightInstructions = string.Empty;
    Dictionary<string, (string left, string right)> Nodes = new Dictionary<string, (string left, string right)>();
    int lineCounter = 0;
    public override void DoFinalThings()
    {
        string lastFoundNode = "AAA";
        while (lastFoundNode != "ZZZ")
        {
            foreach (var leftRightInstruction in leftRightInstructions)
            {
                if (leftRightInstruction == 'L')
                {
                    lastFoundNode = Nodes[lastFoundNode].left;
                }
                else if (leftRightInstruction == 'R')
                {
                    lastFoundNode = Nodes[lastFoundNode].right;
                }
                Result++;
            }
        }
    }

    public override void DoLoopThings(string line)
    {
        if (lineCounter > 1)
        {
            var split = line.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Nodes.Add(split[0]
                , (split[2].Replace("(", "").Replace(",", "")
                , split[3].Replace(")", "")));
            return;
        }

        if (lineCounter == 0)
        {
            leftRightInstructions = line.Trim();
        }

        lineCounter++;
    }
}