
class Day4Part1 : AbstractDays
{    public List<List<ConsoleItem>> ConsoleCharacterLines { get; private set; } = new List<List<ConsoleItem>>();

    public override void DoThings(string line)
    {
        int startIndex = line.IndexOf(":") + 2;
        int seperatorStartIndex = line.IndexOf(" | ");
        string winningNumbersString = line.Substring(startIndex, seperatorStartIndex - startIndex).Replace("  ", " ").Trim();
        string givenNumbersString = line.Substring(seperatorStartIndex + 3).Replace("  ", " ").Trim();

        int[] winningNumbers = winningNumbersString.Split(' ').Select(int.Parse).ToArray();
        int[] givenNumbers = givenNumbersString.Split(' ').Select(int.Parse).ToArray();

        int[] joinedNumbers = (from w in winningNumbers
            join g in givenNumbers on w equals g
            select w).ToArray();

        Result += Convert.ToInt32(Math.Pow(2,joinedNumbers.Length - 1));
    }
}
