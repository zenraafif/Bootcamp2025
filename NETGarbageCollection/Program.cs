// using System;

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("=== Garbage Collection Demo ===");

//         // Membuat objek besar agar masuk Large Object Heap
//         byte[] bigArray = new byte[100_000]; // > 85 KB
//         Console.WriteLine(GC.GetGeneration(bigArray));


//         // Objek kecil (akan ada di Gen 0)
//         object smallObj1 = new object();
//         object smallObj2 = new object();

//         Console.WriteLine("Generation dari objek kecil:");
//         Console.WriteLine($"smallObj1: Gen {GC.GetGeneration(smallObj1)}");
//         Console.WriteLine($"smallObj2: Gen {GC.GetGeneration(smallObj2)}");

//         // Memaksa GC untuk collect Generation 0
//         Console.WriteLine("\nMemanggil GC.Collect(0)...");
//         GC.Collect(0);
//         GC.WaitForPendingFinalizers();

//         Console.WriteLine("Generation setelah GC.Collect(0):");
//         Console.WriteLine($"smallObj1: Gen {GC.GetGeneration(smallObj1)}");
//         Console.WriteLine($"smallObj2: Gen {GC.GetGeneration(smallObj2)}");

//         // Hilangkan referensi → objek tidak lagi punya root
//         smallObj1 = null;

//         Console.WriteLine("\nMemanggil full GC...");
//         GC.Collect(); // semua generasi
//         GC.WaitForPendingFinalizers();

//         try
//         {
//             Console.WriteLine($"smallObj1 sudah dihapus? {GC.GetGeneration(smallObj1)}");
//         }
//         catch (Exception)
//         {
//             Console.WriteLine("smallObj1 sudah tidak ada (null).");
//         }

//         Console.WriteLine($"smallObj2 masih hidup di Gen {GC.GetGeneration(smallObj2)}");
//         Console.WriteLine($"bigArray (LOH) ada di Gen {GC.GetGeneration(bigArray)}");

//         Console.WriteLine("\nSelesai.");
//     }
// }

// THE UPPERCODE IS GARBAGE COLLECTION EXAMPLE

using System;
using System.Runtime;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Demo Compacting ===");

        // Alokasikan big array (masuk LOH)
        byte[] bigArray1 = new byte[100_000];
        byte[] bigArray2 = new byte[100_000];

        Console.WriteLine($"Gen bigArray1: {GC.GetGeneration(bigArray1)}");
        Console.WriteLine($"Gen bigArray2: {GC.GetGeneration(bigArray2)}");

        // Hilangkan referensi ke satu array
        bigArray1 = null;

        Console.WriteLine("\nSebelum compact, ada 1 array dihapus (bigArray1).");

        // Memaksa GC collect tanpa compact LOH
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("GC.Collect() dipanggil tanpa compact LOH.");

        // Sekarang paksa compact LOH
        Console.WriteLine("\nMemaksa Compact LOH...");
        GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("GC.Collect() dengan compact LOH selesai.");
    }
}
