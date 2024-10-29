// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");
var watch = Stopwatch.StartNew();
// Console.WriteLine(new Day1(){FileName = "puzzle-inputs/input-puzzle-1.txt"}.Run());

// Console.WriteLine(new Day2Part1(){FileName = "puzzle-inputs/input-puzzle-2.txt"}.Run());
// Console.WriteLine(new Day2Part2(){FileName = "puzzle-inputs/input-puzzle-2.txt"}.Run());

// var day31 = new Day3Part1(){FileName = "puzzle-inputs/input-puzzle-3.txt"};
// Console.WriteLine(day31.Run());
// day31.ConsoleCharacterLines.PrintToConsole();
// day31.PrintSumFromConsoleItems();

// var day32 = new Day3Part2(){FileName = "puzzle-inputs/input-puzzle-3.txt"};
// Console.WriteLine(day32.Run());
// day32.ConsoleCharacterLines.PrintToConsole();

// Console.WriteLine(new Day4Part1(){FileName = "puzzle-inputs/input-puzzle-4.txt"}.Run());
// Console.WriteLine(new Day4Part2(){FileName = "puzzle-inputs/input-puzzle-4.txt"}.Run());

// Console.WriteLine(new Day5Part1(){FileName = "puzzle-inputs/input-puzzle-5.txt"}.Run());
// Console.WriteLine(new Day5Part2() { FileName = "puzzle-inputs/input-puzzle-5.txt" }.Run());

// Console.WriteLine(new Day6Part1(){FileName = "puzzle-inputs/input-puzzle-6.txt"}.Run());
// Console.WriteLine(new Day6Part2(){FileName = "puzzle-inputs/input-puzzle-6.txt"}.Run());

// Console.WriteLine(new Day7Part1(){FileName = "puzzle-inputs/input-puzzle-7.txt"}.Run());
// Console.WriteLine(new Day7Part2(){FileName = "puzzle-inputs/input-puzzle-7.txt"}.Run());

Console.WriteLine(new Day8Part1(){FileName = "puzzle-inputs/input-puzzle-8.txt"}.Run());

watch.Stop();
Console.WriteLine($"Time taken: {watch.ElapsedMilliseconds} ms");