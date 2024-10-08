public static class ConsoleExtensions
{

    public static void PrintToConsole(this List<List<ConsoleItem>> consoleCharacterLines)
    {

        foreach (var item in consoleCharacterLines)
        {
            foreach (var item2 in item)
            {

                Console.BackgroundColor = item2.Colour;
                Console.Write(item2.Text);
            }
            Console.WriteLine();
        }
    }
}