using System;
using System.Collections;
using System.Xml.Serialization;

class StackNonGeneric
{
    static public void Main()
    {
        Stack s = new Stack();

        s.Push("Zenra");
        s.Push("Afif");
        s.Push(null);
        s.Push(10);
        s.Push(49.6);

        foreach (var item in s)
        {
            Console.WriteLine(item);
        }
    }

    
}