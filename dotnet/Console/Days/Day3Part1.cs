
class Day3Part1 : AbstractDays
{
    private string? lastLine;
    private List<PartNumber> lastLineNotUsedNumbers = new List<PartNumber>();
    public List<List<ConsoleItem>> ConsoleCharacterLines { get; private set; } = new List<List<ConsoleItem>>();

    public override void DoLoopThings(string line)
    {
        List<ConsoleItem> consoleCharacters = new List<ConsoleItem>();
        PartNumber? number = null;
        bool symbolSet = false;
        int indexCounter = 0;
        List<PartNumber> currentLineNotUsedNumbers = new List<PartNumber>();
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
            else if (character == '.')
            {
                if (number != null)
                {
                    if (symbolSet)
                    {
                        Result += number.Value;
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
                    }
                    else
                    {
                        //check former line if symbol is available
                        if (!String.IsNullOrEmpty(lastLine))
                        {
                            if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => !char.IsDigit(c) && c != '.'))
                            {
                                Result += number.Value;
                                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Blue, Index = number.Index });
                            }
                            else
                            {
                                currentLineNotUsedNumbers.Add(number);
                                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                            }
                        }
                        else
                        {
                            currentLineNotUsedNumbers.Add(number);
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
                if (number != null)
                {
                    Result += number.Value;
                    consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Cyan, Index = number.Index });
                }
                if (lastLineNotUsedNumbers.Any())
                {
                    foreach (var n in lastLineNotUsedNumbers)
                    {
                        if (!n.Added && n.IndexInRange(indexCounter))
                        {
                            Result += n.Value;
                            symbolConsoleColor = ConsoleColor.Green;
                            ConsoleCharacterLines.Last().Where(i => i.Index == n.Index).FirstOrDefault().Colour = ConsoleColor.Magenta;
                            n.Added = true;
                        }
                    }
                }
                consoleCharacters.Add(new ConsoleItem { Text = character.ToString(), Colour = symbolConsoleColor, Index = indexCounter });
                number = null;
            }
            indexCounter++;
        }

        if (number != null)
        {
            number.LastOnLine = true;
            if (symbolSet)
            {
                Result += number.Value;
                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
            }
            else
            {
                //check former line if symbol is available
                if (!String.IsNullOrEmpty(lastLine))
                {
                    if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => !char.IsDigit(c) && c != '.'))
                    {
                        Result += number.Value;
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Blue, Index = number.Index });
                    }
                    else
                    {
                        currentLineNotUsedNumbers.Add(number);
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                    }
                }
                else
                {
                    currentLineNotUsedNumbers.Add(number);
                    consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Red, Index = number.Index });
                }
            }
            number = null;
            symbolSet = false;
        }
        lastLine = line;
        lastLineNotUsedNumbers = currentLineNotUsedNumbers;
        ConsoleCharacterLines.Add(consoleCharacters);
    }

    public void PrintSumFromConsoleItems()
    {

        var sum = ConsoleCharacterLines.Sum(l => l
            .Where(i => i.Colour == ConsoleColor.DarkBlue || i.Colour == ConsoleColor.Blue || i.Colour == ConsoleColor.Cyan || i.Colour == ConsoleColor.Magenta)
            .Sum(i => long.Parse(i.Text)));

        Console.WriteLine(sum);
    }
    public override void DoFinalThings()
    {
    }
}
