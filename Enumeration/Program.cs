/*
Enumeration (Enumerator) : interface IEnumerable dan IEnumerator
------------------------------------------------------
adalah cara menelusuri koleksi data satu per satu menggunakan Enumerator.
Enumerator adalah objek yang mengimplementasikan interface IEnumerator.

Enumerator memiliki:
    method      MoveNext() untuk pindah ke elemen berikutnya,
    property    Current untuk mendapatkan elemen saat ini,
    method      Reset() untuk mengembalikan posisi ke sebelum elemen pertama.

    Enumeration biasanya diakses lewat interface:
        IEnumerator → untuk “cursor” (penunjuk posisi saat ini).
        IEnumerable → untuk “kumpulan data” (bisa diiterasi). 

    GetEnumerator() mengembalikan objek IEnumerator.
*/

using System;
using System.Collections;

// Cursor / Enumerator
class MyEnumeratorName : IEnumerator
{
    private int[] data;
    private int position = -1; // mulai sebelum index pertama

    public MyEnumeratorName(int[] data)
    {
        this.data = data;
    }

    // Pindah ke elemen berikutnya. Method
    public bool MoveNext()
    {
        position++;
        return (position < data.Length);
    }

    // Kembalikan posisi ke sebelum elemen pertama. Method
    public void Reset()
    {
        position = -1;
    }

    // Dapatkan elemen saat ini. Property
    public object Current
    {
        get { return data[position]; }
    }
}

class MyCollectionName : IEnumerable
{
    private int[] numbers = { 10, 20, 30 };

    public IEnumerator GetEnumerator()
    {
        return new MyEnumeratorName(numbers);
    }
}

class Program
{
    static void Main()
    {
        // High-level enumeration dengan foreach
        Console.WriteLine("------- High-level Enumeration -------");
        MyCollectionName collection = new MyCollectionName();
        foreach (var item in collection)
        {
            Console.WriteLine(item);
        }
        
        // Low-level enumeration tanpa foreach
        Console.WriteLine("------- Low-level Enumeration without foreach -------");
        var enumeratorLowLevelForeach = collection.GetEnumerator();
        try
        {
            while (enumeratorLowLevelForeach.MoveNext())
            {
                var item = enumeratorLowLevelForeach.Current;
                Console.WriteLine(item);
            }
        }
        finally
        {
            // jika enumeratorLowLevelForeach implement IDisposable, akan di-dispose di sini
            (enumeratorLowLevelForeach as IDisposable)?.Dispose();
        }

        string word = "beer";

        Console.WriteLine("------- Enumerate string manual -------");
        using (var enumerator = word.GetEnumerator()) // dapatkan enumerator dari string
        {
            while (enumerator.MoveNext()) // maju ke karakter berikutnya
            {
                var element = enumerator.Current; // ambil karakter saat ini
                Console.WriteLine(element);
            }
        }

        Console.WriteLine("------- Enumerate list manual -------");
        var list = new List<int> { 1, 2, 3 };
        List<int> list2 = [1, 2, 3];
        List<int> listManual = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        var dict = new Dictionary<int, string>()
        {
            { 5, "five" },  // Key-value pair syntax
            { 10, "ten" }
        };


        using (var enumerator = list.GetEnumerator())
        {
            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                Console.WriteLine(element);
            }
        }
        


    }
}
