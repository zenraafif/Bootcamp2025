using System;

class Program
{
    static void Main()
    {
        Console.Write("Please enter a number : ");
        int input = Convert.ToInt32(Console.ReadLine());

        for (int x = 1; x <= input; x++)
        {
            string result = "";
            if (x % 3 == 0) result += "foo";
            if (x % 4 == 0) result += "baz";
            if (x % 5 == 0) result += "bar";
            if (x % 7 == 0) result += "jazz";
            if (x % 9 == 0 ) result += "huzz";
            if (result == "") result = x.ToString();
            
            Console.Write(result);

            if (x < input)
                Console.Write(", ");
        }
    }
}