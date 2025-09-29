using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Main thread mulai...");

        // Antrikan beberapa pekerjaan ke ThreadPool
        for (int i = 1; i <= 5; i++)
        {
            int taskNum = i; // perlu variabel lokal agar tidak tertimpa
            ThreadPool.QueueUserWorkItem(DoWork, taskNum);
        }

        Console.WriteLine("Main thread lanjut mengerjakan hal lain...");

        // Tunggu sebentar agar thread pool sempat menyelesaikan tugas
        Thread.Sleep(2000);
        Console.WriteLine("Main thread selesai.");
    }

    // Method yang akan dieksekusi di thread pool
    static void DoWork(object? state)
    {
        int taskNum = (int)state!;
        Console.WriteLine($"Task {taskNum} mulai di Thread {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(500); // simulasi kerja
        Console.WriteLine($"Task {taskNum} selesai di Thread {Thread.CurrentThread.ManagedThreadId}");
    }
}
