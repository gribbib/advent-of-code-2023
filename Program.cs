// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
using var reader = new StreamReader("input-puzzle-1.txt");
string line = "";
int result = 0;
while ((line = reader.ReadLine()) != null){
    var numberString = new String(line.Where(Char.IsDigit).ToArray());
    int number = Convert.ToInt32($"{numberString.First()}{numberString.Last()}");
    result += number;
}

Console.WriteLine(result);