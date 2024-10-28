
class Day2Part2 : AbstractDays
{
    public int GameCounter { get; private set; }

    public override void DoLoopThings(string line)
    {
        int power = GetMinimumColourThreshold(line, "red") *
            GetMinimumColourThreshold(line, "blue") *
            GetMinimumColourThreshold(line, "green");

        //if the code runs till here the game is valid and should added to the result
        Result += power;
    }

    private int GetMinimumColourThreshold(string line, string colour)
    {
        int lastNumber = 1;
        var list = line.AllIndexesOf(colour);
        foreach (var index in list)
        {
            // get index of space and then get number between the two space
            int spaceIndex = line.LastIndexOf(" ", index - 2);
            var numberString = line.Substring(spaceIndex, index - 1 - spaceIndex);
            var numberColour = int.Parse(numberString);

            if (lastNumber < numberColour)
            {
                lastNumber = numberColour;
            }
        }

        return lastNumber;
    }
    
    public override void DoFinalThings()
    {
    }
}
