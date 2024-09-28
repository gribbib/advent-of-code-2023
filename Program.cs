// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
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
while ((line = reader.ReadLine()) != null){
    Tuple<int,int> firstNumberIndexAndValue = new (-1,-1);
    Tuple<int,int> lastNumberIndexAndValue = new (-1,-1);

    foreach (var item in numberDictionary)
    {
        var list = AllIndexesOf(line, item.Key);
        if (list.Any()){
            int minIndex = list.Min();
            if (minIndex < firstNumberIndexAndValue.Item1 || firstNumberIndexAndValue.Item1 == -1){
                firstNumberIndexAndValue = new (minIndex, item.Value);
            }
            int maxIndex = list.Max();
            if (maxIndex > lastNumberIndexAndValue.Item1){
                lastNumberIndexAndValue = new (maxIndex, item.Value);
            }
        }
    }
    int number = 0;
    if (firstNumberIndexAndValue.Item2 != -1 && lastNumberIndexAndValue.Item2 != -1){
        number = int.Parse($"{firstNumberIndexAndValue.Item2}{lastNumberIndexAndValue.Item2}");
    }
    result += number;
}

Console.WriteLine(result);

static List<int> AllIndexesOf(string str, string value) {
    if (String.IsNullOrEmpty(value))
        throw new ArgumentException("the string to find may not be empty", "value");
    List<int> indexes = new List<int>();
    for (int index = 0;; index += value.Length) {
        index = str.IndexOf(value, index);
        if (index == -1)
            return indexes;
        indexes.Add(index);
    }
}