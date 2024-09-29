
class Day2Part1 : AbstractDays
{
    private readonly int RedThreshold = 12;
    private readonly int GreenThreshold = 13;
    private readonly int BlueThreshold = 14;
    public int GameCounter { get; private set; }

    public override void DoLoopThings(string line)
    {
        GameCounter++;
        CheckColourThreshold(line, "red", RedThreshold);
        CheckColourThreshold(line, "blue", BlueThreshold);
        CheckColourThreshold(line, "green", GreenThreshold);

        //if the code runs till here the game is valid and should added to the result
        Result += GameCounter;
    }


    private void CheckColourThreshold(string line, string colour, int colourThreshold)
    {
        var list = line.AllIndexesOf(colour);
        foreach (var index in list)
        {
            // get index of space and then get number between the two space
            int spaceIndex = line.LastIndexOf(" ", index - 2);
            var numberString = line.Substring(spaceIndex, index - 1 - spaceIndex);
            var numberColour = int.Parse(numberString);
            if (numberColour > colourThreshold)
            {
                throw new ContinueException();
            }
        }
    }
    
    public override void DoFinalThings()
    {
    }
}
