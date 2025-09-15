using System;

class Program
{
    public delegate void OutputHandler(string text);

    static void ProcessData(string data, OutputHandler dell)
    {
        dell(data);
    }

    static void PrintToConsole(string text)
    {
        Console.WriteLine("Console: " + text);
    }

    static void PrintWithStars(string text)
    {
        Console.WriteLine("*** " + text + " ***");
    }

    static void ToUpperCase(string text)
    {
        Console.WriteLine(text.ToUpper());
    }

    static void Main()
    {
        OutputHandler del;

        del = PrintToConsole;
        del("Printing to console");

        del = PrintWithStars;
        del("Printing with stars");

        ProcessData("Hello, World!", ToUpperCase);
    }
}
