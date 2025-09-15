using System;
using System.IO;
using System.Threading;

public delegate void ProgressReporter(int percentComplete);

public class Util
{
    public static void HardWork(ProgressReporter p)
    {
        for (int i = 0; i < 10; i++)
        {
            // Panggil delegate (semua method di dalamnya akan dieksekusi)
            p.Invoke(i * 10);

            // Simulasi pekerjaan (delay 100 ms)
            Thread.Sleep(100);
        }
    }
}

public class Program
{
    // Method untuk menulis progress ke Console
    public static void WriteProgressToConsole(int percentComplete)
    {
        Console.WriteLine("Console: " + percentComplete + "%");
    }

    // Method untuk menulis progress ke File
    public static void WriteProgressToFile(int percentComplete)
    {
        File.WriteAllText("progress.txt", percentComplete.ToString());
    }

    public static void Main()
    {
        // Buat delegate instance, awalnya menunjuk ke console
        ProgressReporter p = new ProgressReporter(WriteProgressToConsole);

        // Tambahkan method lain ke delegate (multicast)
        p += new ProgressReporter(WriteProgressToFile);

        // Jalankan pekerjaan keras
        Util.HardWork(p);

        Console.WriteLine("Selesai!");
    }
}
