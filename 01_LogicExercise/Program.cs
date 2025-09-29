using System;

public class PrintNumber
{
    public List<(int number, string specialNumber)> rules = new List<(int, string)>();

    public void AddRule(int number, string specialNumber)
    {
        rules.Add((number, specialNumber));
    }

    public List<string> Generate(int maximum)

    {
        List<string> results = new List<string>();

        for (int i = 1; i <= maximum; i++)
        {
            string result = "";

            foreach (var thisRule in rules)
            {
                if (i % thisRule.number == 0)
                {
                    result += thisRule.specialNumber;
                }
            }

            if (result == "")
            {
                result = i.ToString();
            }

            results.Add(result);
        }

        return results;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Please enter a number : ");
        int input = Convert.ToInt32(Console.ReadLine());

        PrintNumber _printNumber = new PrintNumber();

        _printNumber.AddRule(3, "foo");
        _printNumber.AddRule(4, "baz");
        _printNumber.AddRule(5, "bar");
        _printNumber.AddRule(7, "jazz");
        _printNumber.AddRule(9, "huzz");

        List<string> res = _printNumber.Generate(input);

        Console.WriteLine(string.Join(",", res));
    }
}



