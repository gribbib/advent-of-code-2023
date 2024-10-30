
using System.Data;
using System.Globalization;

public class Day8Part2 : AbstractDays
{
    string leftRightInstructions = string.Empty;
    Dictionary<string, (string left, string right)> Nodes = new Dictionary<string, (string left, string right)>();
    int lineCounter = 0;
    public override void DoFinalThings()
    {
        string[] lastFoundNodes = Nodes.Keys.Where(k => k.EndsWith("A")).ToArray();
        int parallelNodeNumber = lastFoundNodes.Length;
        long[] firstZSteps = new long[parallelNodeNumber];
        Array.Fill(firstZSteps, -1);
        bool allEndInZ = false;
        Console.WriteLine($"Parallel nodes: {parallelNodeNumber}");
        Console.WriteLine("");

        // the loop breaks when either all Zs have been found in the same step 
        // or for each node the first occurence of the Zs have been found
        while (!allEndInZ && firstZSteps.Any(s => s == -1))
        {
            allEndInZ = true;
            foreach (var instruction in leftRightInstructions)
            {
                Result++;
#if !DEBUG
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine(Result);
#endif

                allEndInZ = true;
                Parallel.For(0, parallelNodeNumber,
                i =>
                {
                    lastFoundNodes[i] = GetFullInstructionsFinalNode(lastFoundNodes[i], instruction.ToString());

                    if (!lastFoundNodes[i].EndsWith("Z"))
                    {
                        allEndInZ = false;
                    }
                    else
                    {
#if !DEBUG
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
#endif
                        // every start node has exactly one node ending in Z 
                        // that continues to repeat in the same number of steps
                        // so we store the number of steps needed to find the first Z for that node
                        if (firstZSteps[i] == -1)
                        {
                            Console.WriteLine($"Found a Z at Result {Result} for i {i} for node {lastFoundNodes[i]}");
                            Console.WriteLine();
                            firstZSteps[i] = Result;
                        }
                    }
                });
                // aus diesen Steps könnte man das kleinste gemeinsame vielfache ermitteln.
                // bspw. in dem solange die werte multipliziert werden bis sie das gleiche ergebnis erhalten.
                // Dabei aufpassen, dass sie nicht mit dem gleichen Faktor das kgV erhalten.

                // in case all Zs have been found in the same step then the instructions will not be continued
                if (allEndInZ)
                {
                    break;
                }
            }
        }

        // in case the Zs were not found in the same step
        // we calculate the total number of steps by finding the least common multiplier 
        // for all found step numbers of the first occurences of Z
        if (!allEndInZ)
        {
            long lcm = firstZSteps[0];
            for (int i = 1; i < parallelNodeNumber; i++)
            {
                lcm = LeastCommonMultiple(lcm, firstZSteps[i]);
            }
            Result = lcm;
        }

    }

    static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public long LeastCommonMultiple(long a, long b)
    {
        return a / GreatestCommonDivisor(a, b) * b;
    }

    /// <summary>
    /// Method to get the final node for the given instructions with recursion. This can be a single "R" or "L" 
    /// but also a combination of instructions like "LR".
    /// </summary>
    /// <param name="entryNode">Node to start with which will be checked in the global class dictionary <see cref="Nodes"/></param>
    /// <param name="instructions">Submit either full instructions like "LR" or a single instruction like "R" or "L"</param>
    /// <returns></returns>
    public string GetFullInstructionsFinalNode(string entryNode, string instructions)
    {
        if (instructions.Length == 0)
        {
            return entryNode;
        }
        string instruction = instructions.Substring(0, 1);
        string leftInstructions = instructions.Remove(0, 1);
        if (instruction == "L")
        {
            return GetFullInstructionsFinalNode(Nodes[entryNode].left, leftInstructions);
        }
        else
        {
            return GetFullInstructionsFinalNode(Nodes[entryNode].right, leftInstructions);
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