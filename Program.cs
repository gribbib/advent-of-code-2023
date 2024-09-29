// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

Console.WriteLine(new Day1(){FileName = "puzzle-inputs/input-puzzle-1.txt"}.Run());
Console.WriteLine(Day3Part2());

static void CheckColourThreshold(string line, string colour, int colourThreshold)
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
            throw new IndexOutOfRangeException();
        }
    }
}

static int GetMinimumColourThreshold(string line, string colour)
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

int Day2Part1()
{
    var reader = new StreamReader("input-puzzle-2.txt");
    string line = "";
    int result = 0;
    int redThreshold = 12;
    int greenThreshold = 13;
    int blueThreshold = 14;
    int gameCounter = 0;
    while ((line = reader.ReadLine()) != null)
    {
        gameCounter++;
        try
        {
            CheckColourThreshold(line, "red", redThreshold);
            CheckColourThreshold(line, "blue", blueThreshold);
            CheckColourThreshold(line, "green", greenThreshold);

            //if the code runs till here the game is valid and should added to the result
            result += gameCounter;
        }
        catch (IndexOutOfRangeException)
        {
            continue;
        }
    }

    return result;
}

int Day2Part2()
{
    var reader = new StreamReader("input-puzzle-2.txt");
    string line;
    int result = 0;
    while ((line = reader.ReadLine()) != null)
    {
        int power = GetMinimumColourThreshold(line, "red") *
            GetMinimumColourThreshold(line, "blue") *
            GetMinimumColourThreshold(line, "green");

        //if the code runs till here the game is valid and should added to the result
        result += power;
    }

    return result;
}

int Day3Part1()
{
    var reader = new StreamReader("input-puzzle-3.txt");
    string line, lastLine = "";
    int result = 0;
    List<PartNumber> lastLineNotUsedNumbers = new List<PartNumber>();
    List<List<ConsoleItem>> consoleCharacterLines = new List<List<ConsoleItem>>();
    while ((line = reader.ReadLine()) != null)
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
                        result += number.Value;
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
                    }
                    else
                    {
                        //check former line if symbol is available
                        if (!String.IsNullOrEmpty(lastLine))
                        {
                            if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => !char.IsDigit(c) && c != '.'))
                            {
                                result += number.Value;
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
                    result += number.Value;
                    consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.Cyan, Index = number.Index });
                }
                if (lastLineNotUsedNumbers.Any())
                {
                    foreach (var n in lastLineNotUsedNumbers)
                    {
                        if (!n.Added && n.IndexInRange(indexCounter))
                        {
                            result += n.Value;
                            symbolConsoleColor = ConsoleColor.Green;
                            consoleCharacterLines.Last().Where(i => i.Index == n.Index).FirstOrDefault().Colour = ConsoleColor.Magenta;
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
                result += number.Value;
                consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
            }
            else
            {
                //check former line if symbol is available
                if (!String.IsNullOrEmpty(lastLine))
                {
                    if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => !char.IsDigit(c) && c != '.'))
                    {
                        result += number.Value;
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
        consoleCharacterLines.Add(consoleCharacters);
    }

    foreach (var item in consoleCharacterLines)
    {
        foreach (var item2 in item)
        {

            Console.BackgroundColor = item2.Colour;
            Console.Write(item2.Text);
        }
        Console.WriteLine();
    }

    var sum = consoleCharacterLines.Sum(l => l
        .Where(i => i.Colour == ConsoleColor.DarkBlue || i.Colour == ConsoleColor.Blue || i.Colour == ConsoleColor.Cyan || i.Colour == ConsoleColor.Magenta)
        .Sum(i => long.Parse(i.Text)));

    Console.WriteLine(sum);

    return result;
}

int Day3Part2()
{
    var reader = new StreamReader("puzzle-inputs/input-puzzle-3.txt");
    string line, lastLine = "";
    int result = 0;
    List<PartNumber> lastLineNumbers = new List<PartNumber>();
    List<List<ConsoleItem>> consoleCharacterLines = new List<List<ConsoleItem>>();
    List<Tuple<int, int, List<PartNumber>>> gearList = new List<Tuple<int, int, List<PartNumber>>>();
    int lineCounter = 0;
    while ((line = reader.ReadLine()) != null)
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
                        gearList.First(g => g.Item1 == number.Index - 1 && g.Item2 == lineCounter).Item3.Add(number);
                        consoleCharacters.Add(new ConsoleItem { Text = number.ValueString, Colour = ConsoleColor.DarkBlue, Index = number.Index });
                    }
                    else
                    {
                        //check former line if symbol is available
                        if (!String.IsNullOrEmpty(lastLine) && gearList.Any())
                        {
                            if (lastLine.Substring(number.SearchStartIndex, number.SearchLength).Any(c => c == '*'))
                            {
                                var indexes = lastLine.Substring(number.SearchStartIndex, number.SearchLength).AllIndexesOf("*");
                                foreach (var i in indexes)
                                {
                                    gearList.First(g => g.Item1 == i + number.SearchStartIndex && g.Item2 == lineCounter - 1).Item3.Add(number);
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
                gearList.Add(new Tuple<int, int, List<PartNumber>>(indexCounter, lineCounter, numberList));
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
                            consoleCharacterLines.Last().Where(i => i.Index == n.Index).FirstOrDefault().Colour = ConsoleColor.Magenta;
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
                gearList.First(g => g.Item1 == number.Index - 1 && g.Item2 == lineCounter).Item3.Add(number);
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
                            gearList.First(g => g.Item1 == i + number.SearchStartIndex && g.Item2 == lineCounter - 1).Item3.Add(number);
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
        consoleCharacterLines.Add(consoleCharacters);
        lineCounter++;
    }


    var test = gearList.Where(g => g.Item3.Count < 2);
    foreach (var t in test)
    {
        consoleCharacterLines.SelectMany(l => l).First(c => c.Index == t.Item1 && c.Line == t.Item2).Colour = ConsoleColor.White;
    }

    foreach (var item in consoleCharacterLines)
    {
        foreach (var item2 in item)
        {

            Console.BackgroundColor = item2.Colour;
            Console.Write(item2.Text);
        }
        Console.WriteLine();
    }

    var sum = consoleCharacterLines.Sum(l => l
        .Where(i => i.Colour == ConsoleColor.DarkBlue || i.Colour == ConsoleColor.Blue || i.Colour == ConsoleColor.Cyan || i.Colour == ConsoleColor.Magenta)
        .Sum(i => long.Parse(i.Text)));

    Console.WriteLine(sum);

    //gear ration sum
    result = gearList.Where(g => g.Item3.Count == 2).Select(g => g.Item3.First().Value * g.Item3.Last().Value).Sum();

    return result;
}
class ConsoleItem
{
    public string Text { get; set; }
    public ConsoleColor Colour { get; set; }
    public int Index { get; set; }
    public int Line { get; set; }
}
class PartNumber
{
    public int Index { get; set; } = -1;
    public bool LastOnLine { get; set; }
    public int SearchLength { get { return ValueString == null ? 0 : ValueString.Length + (LastOnLine || Index == 0 ? 1 : 2); } }
    public int SearchStartIndex { get { return Index == 0 ? 0 : Index - 1; } }
    public string? ValueString { get; set; }
    public int Value
    {
        get
        {
            if (String.IsNullOrEmpty(ValueString))
            {
                return 0;
            }
            else
            {
                return int.Parse(ValueString);
            }
        }
    }
    public bool Added { get; set; } = false;

    internal bool IndexInRange(int requestedIndex)
    {
        return ValueString == null ? false : requestedIndex >= Index - 1 && requestedIndex <= Index + ValueString.Length;
    }
}