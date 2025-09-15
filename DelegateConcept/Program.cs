using System;

class Program
{
    // 1. Delegate Declaration
    public delegate void MyDelegate(string message);

    // 2. Methods that match the delegate "signature(bentuk) string return void"
    static void SayHello(string name)
    {
        Console.WriteLine("Hello, " + name);
    }

    static void SayGoodbye(string name)
    {
        Console.WriteLine("Goodbye, " + name);
    }

    static void Main()
    {
        // 3. Make delegate instances and use them
        MyDelegate del;

        del = SayHello; 
        del("Budi");    

        del = SayGoodbye; 
        del("Budi");      
    }
}
