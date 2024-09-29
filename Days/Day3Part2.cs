
class Day3Part2 : AbstractDays
{
    private string? lastLine;
    private List<PartNumber> lastLineNumbers = new List<PartNumber>();
    private List<PartNumber> lastLineNotUsedNumbers = new List<PartNumber>();
    private int lineCounter;

    public List<List<ConsoleItem>> ConsoleCharacterLines { get; private set; } = new List<List<ConsoleItem>>();
    List<Tuple<int, int, List<PartNumber>>> GearList = new List<Tuple<int, int, List<PartNumber>>>();

    public override void DoLoopThings(string line)
    {
        List<ConsoleItem> consoleCharacters = new List<ConsoleItem>();
        PartNumber? number = null;
        bool symbolSet = false;
        int indexCounter = 0;
        List<PartNumber> currentLineNumbers = new List<PartNumber>();
        // gear list mit index als id für gear und liste für nummer. am ende dann die gears mit genau 2 nummer suchen und entsprechend berechnen.
        foreach (var character in line)
        {
            if (char.IsNumber(character))
            {
                if (number == null)
                {
                    number = new PartNumber();
                    number.Index = indexCounter;
                }
                number.ValueString += character;
            }
            else if (character != '*')
            {
                if (number != null)
                {
                    currentLineNumbers.Add(number);
                    if (symbolSet)
                    {
                        GearList.First(g => g.Item1 == number.Index - 1 && g.Item2 == lineCounter).Item3.Add(number);
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
                    }
                    else
                    {
                        //check former line if symbol is available
                        if (!String.IsNullOrEmpty(lastLine) && GearList.Any())
                        {
                            if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => c == '*'))
                            {
                                var indexes = lastLine.Substring(number.SearchStartIndex, number.SearchLength).AllIndexesOf("*");
                                foreach (var i in indexes)
                                {
                                    GearList.First(g => g.Item1 == i + number.SearchStartIndex && g.Item2 == lineCounter - 1).Item3.Add(number);
                                }
                                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Blue, Index = number.Index });
                            }
                            else
                            {
                                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                            }
                        }
                        else
                        {
                            consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                        }
                    }
                }
                number = null;
                symbolSet = false;
                consoleCharacters.Add(new ConsoleItem { Text = character.ToString(), Colour = ConsoleColor.Black, Index = indexCounter });
            }
            else
            {
                var symbolConsoleColor = ConsoleColor.Yellow;
                symbolSet = true;
                var numberList = new List<PartNumber>();
                GearList.Add(new Tuple<int, int, List<PartNumber>>(indexCounter, lineCounter, numberList));
                if (number != null)
                {
                    numberList.Add(number);
                    consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Cyan, Index = number.Index });
                }
                if (lastLineNumbers.Any())
                {
                    foreach (var n in lastLineNumbers)
                    {
                        if (n.IndexInRange(indexCounter))
                        {
                            numberList.Add(n);
                            symbolConsoleColor = ConsoleColor.Green;
                            ConsoleCharacterLines.Last().Where(i => i.Index == n.Index).FirstOrDefault().Colour = ConsoleColor.Magenta;
                            n.Added = true;
                        }
                    }
                }
                consoleCharacters.Add(new ConsoleItem { Text = character.ToString(), Colour = symbolConsoleColor, Index = indexCounter, Line = lineCounter });
                number = null;
            }
            indexCounter++;
        }

        if (number != null)
        {
            number.LastOnLine = true;
            currentLineNumbers.Add(number);
            if (symbolSet)
            {
                GearList.First(g => g.Item1 == number.Index - 1 && g.Item2 == lineCounter).Item3.Add(number);
                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
            }
            else
            {
                //check former line if symbol is available
                if (!String.IsNullOrEmpty(lastLine))
                {
                    if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => c == '*'))
                    {
                        var indexes = lastLine.Substring(number.SearchStartIndex, number.SearchLength).AllIndexesOf("*");
                        foreach (var i in indexes)
                        {
                            GearList.First(g => g.Item1 == i + number.SearchStartIndex && g.Item2 == lineCounter - 1).Item3.Add(number);
                        }
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Blue, Index = number.Index });
                    }
                    else
                    {
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                    }
                }
                else
                {
                    consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                }
            }
            number = null;
            symbolSet = false;
        }
        lastLine = line;
        lastLineNumbers = currentLineNumbers;
        ConsoleCharacterLines.Add(consoleCharacters);
        lineCounter++;
    }
    
    public override void DoFinalThings()
    {
        Result = GearList.Where(g => g.Item3.Count == 2).Select(g => g.Item3.First().Value * g.Item3.Last().Value).Sum();
    }
}
