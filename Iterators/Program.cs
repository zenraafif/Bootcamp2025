/*
    Iterators in C#
    - Iterators adalah cara mudah untuk membuat 
        enumerator (yang mengimplementasi interface IEnumerable dan IEnumerator)
    - Dengan iterators, kita tidak perlu membuat class enumerator secara manual
    - Iterators dapat digunakan pada method, property, atau indexer
    - Iterators dapat mengembalikan tipe data IEnumerable atau IEnumerator
            yield return    ---> mengembalikan elemen satu per satu
            yield break     ---> menghentikan iterasi
    - yield return, compiler akan otomatis membuat enumerator (IEnumerator) di balik layar.
*/
using System;
using System.Collections.Generic;

class Program
{
    // Iterator method yang menghasilkan bilangan genap sampai batas tertentu
    static IEnumerable<int> GetEvenNumbers(int max)
    {
        // Loop dari 0 sampai max
        for (int i = 0; i <= max; i++)
        {
            if (i % 2 == 0)
            {
                // yield return = kembalikan elemen satu per satu
                yield return i;
            }
        }

        // yield break = hentikan enumerasi (opsional, loop sudah selesai otomatis di sini)
        yield break;
    }

    // Iterator property: menghasilkan nama hari dalam seminggu
    public static IEnumerable<string> DaysOfWeek
    {
        get
        {
            yield return "Monday";
            yield return "Tuesday";
            yield return "Wednesday";
            yield return "Thursday";
            yield return "Friday";
            yield return "Saturday";
            yield return "Sunday";
        }
    }

    // Contoh lain: Iterator method untuk deret Fibonacci
    IEnumerable<int> Fibs(int fibCount) // Returns IEnumerable<int>
    {
        for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
        {
            yield return prevFib; // Yields the current Fibonacci number
            int newFib = prevFib + curFib;
            prevFib = curFib;
            curFib = newFib;
        }
    }

    // New iterator that filters for even numbers
    IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
    {
        foreach (int x in sequence) // Iterates over the input sequence
        {
            if ((x % 2) == 0)
            {
                yield return x; // Yields only even numbers
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("---- Iterator Method: Even Numbers ----");
        // Enumerasi bilangan genap sampai 10
        foreach (int num in GetEvenNumbers(10))
        {
            Console.WriteLine(num); // Output: 0, 2, 4, 6, 8, 10
        }

        Console.WriteLine("---- Iterator Property: Days of Week ----");
        // Enumerasi hari dalam seminggu
        foreach (string day in DaysOfWeek)
        {
            Console.WriteLine(day);
        }
        Console.WriteLine("---- Iterator Method: Fibonacci Series ----");
        // Enumerasi deret Fibonacci  
        Program p = new Program();
        foreach (int fib in p.Fibs(6)) // Consumes the sequence produced by Fibs
        {
            Console.Write(fib + "  ");
        }
    }
}


/*
    try-catch-finally Restrictions:
    - yield return atau yield break TIDAK BOLEH berada di dalam blok try-catch-finally 
    - yield return atau yield break BOLEH berada di dalam blok try, tapi tanpa catch atau finally
    - yield return atau yield break BOLEH berada di dalam blok catch atau finally, tapi tanpa try
    - Tujuannya untuk menghindari kompleksitas dalam pengelolaan state enumerator
    - Jika perlu penanganan error, letakkan di luar iterator method
    - Jika perlu cleanup, gunakan blok finally di luar iterator method
*/
