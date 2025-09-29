using System;
using System.Threading;

namespace ThreadingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Started");

            // Creating threads with custom names for easier identification
            Thread t1 = new Thread(Method1) { Name = "Thread1" };
            Thread t2 = new Thread(Method2) { Name = "Thread2" };
            Thread t3 = new Thread(Method3) { Name = "Thread3" };

            t1.Start(); // Begins execution of Method1 on Thread1
            t2.Start(); // Begins execution of Method2 on Thread2
            t3.Start(); // Begins execution of Method3 on Thread3

            Console.WriteLine("Main Thread Ended");
            Console.Read(); // Keeps console open
        }

        static void Method1() { /* ... */ }
        static void Method2() { /* ... with Thread.Sleep(10000) ... */ }
        static void Method3() { /* ... */ }
    }
}