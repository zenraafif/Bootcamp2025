using System;

class Program
{
    static void Main()
    {
        Console.Write("Please enter a number : ");
        int input = Convert.ToInt32(Console.ReadLine());

        for (int x = 1; x <= input; x++)
        {
            if (x % 3 == 0 && x % 5 == 0)
                Console.Write("foobar");
            else if (x % 5 == 0)
                Console.Write("bar");
            else if (x % 3 == 0)
                Console.Write("foo");
            else
                Console.Write(x);
                
            if (x < input)
                Console.Write(", ");
        }
    }
}