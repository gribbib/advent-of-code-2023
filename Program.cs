// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

Console.WriteLine(Day3Part1());

static List<int> AllIndexesOf(string str, string value)
{
    if (String.IsNullOrEmpty(value))
        throw new ArgumentException("the string to find may not be empty", "value");
    List<int> indexes = new List<int>();
    for (int index = 0; ; index += value.Length)
    {
        index = str.IndexOf(value, index);
        if (index == -1)
            return indexes;
        indexes.Add(index);
    }
}

int Day1()
{
    using var reader = new StreamReader("input-puzzle-1.txt");
    string line = "";
    int result = 0;
    Dictionary<string, int> numberDictionary = new Dictionary<string, int> {
        {"zero", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
        {"0", 0},
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        };
    while ((line = reader.ReadLine()) != null)
    {
        Tuple<int, int> firstNumberIndexAndValue = new(-1, -1);
        Tuple<int, int> lastNumberIndexAndValue = new(-1, -1);

        foreach (var item in numberDictionary)
        {
            var list = AllIndexesOf(line, item.Key);
            if (list.Any())
            {
                int minIndex = list.Min();
                if (minIndex < firstNumberIndexAndValue.Item1 || firstNumberIndexAndValue.Item1 == -1)
                {
                    firstNumberIndexAndValue = new(minIndex, item.Value);
                }
                int maxIndex = list.Max();
                if (maxIndex > lastNumberIndexAndValue.Item1)
                {
                    lastNumberIndexAndValue = new(maxIndex, item.Value);
                }
            }
        }
        int number = 0;
        if (firstNumberIndexAndValue.Item2 != -1 && lastNumberIndexAndValue.Item2 != -1)
        {
            number = int.Parse($"{firstNumberIndexAndValue.Item2}{lastNumberIndexAndValue.Item2}");
        }
        result += number;
    }
    return result;
}

static void CheckColourThreshold(string line, string colour, int colourThreshold)
{
    var list = AllIndexesOf(line, colour);
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
    var list = AllIndexesOf(line, colour);
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

class ConsoleItem
{
    public string Text { get; set; }
    public ConsoleColor Colour { get; set; }
    public int Index { get; set; }
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