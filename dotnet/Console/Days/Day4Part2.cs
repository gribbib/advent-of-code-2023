
class Day4Part2 : AbstractDays
{
    public List<List<ConsoleItem>> ConsoleCharacterLines { get; private set; } = new List<List<ConsoleItem>>();
    public Dictionary<int, int> copiedCardsDictionary = new Dictionary<int, int>();

    int LineCounter = 1;

    public override void DoLoopThings(string line)
    {
        int totalCopies = (copiedCardsDictionary.ContainsKey(LineCounter) ? copiedCardsDictionary[LineCounter] : 0) + 1;
        int startIndex = line.IndexOf(":") + 2;
        int seperatorStartIndex = line.IndexOf(" | ");
        string winningNumbersString = line.Substring(startIndex, seperatorStartIndex - startIndex).Replace("  ", " ").Trim();
        string givenNumbersString = line.Substring(seperatorStartIndex + 3).Replace("  ", " ").Trim();

        int[] winningNumbers = winningNumbersString.Split(' ').Select(int.Parse).ToArray();
        int[] givenNumbers = givenNumbersString.Split(' ').Select(int.Parse).ToArray();

        int joinedNumbers = (from w in winningNumbers
                               join g in givenNumbers on w equals g
                               select w).ToArray().Length;

        for (int i = LineCounter + 1; i <= joinedNumbers + LineCounter; i++)
        {
            if (copiedCardsDictionary.ContainsKey(i))
            {
                copiedCardsDictionary[i] += totalCopies;
            }
            else
            {
                copiedCardsDictionary.Add(i, totalCopies);
            }
        }

        Result += totalCopies;
        LineCounter++;
    }

    public override void DoFinalThings()
    {
    }
}
