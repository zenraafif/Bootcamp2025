using System;
using System.Collections.Generic;

class StackGeneric
{
    public static void Main()
    {
        Stack<int> s = new Stack<int>();

        s.Push(1000);
        s.Push(2000);
        s.Push(3000);
        s.Push(4000);


        foreach (var item in s)
        {
            Console.WriteLine(item);
        }
    }
}