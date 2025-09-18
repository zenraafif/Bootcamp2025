using System;

struct Number
{
    // (1) Field menyimpan nilai integer
    public int Value;

    // (2) Constructor: dipanggil saat membuat Number baru
    public Number(int value)
    {
        Value = value; // simpan nilai ke field
    }

    // (3) Overload operator '+'
    //     - static: method dipanggil tanpa instance khusus oleh compiler
    //     - signature: menerima dua operand Number dan mengembalikan Number
    //     Ketika program menulis "a + b" (dengan a,b bertipe Number),
    //     compiler akan memanggil method ini.
    public static Number operator +(Number a, Number b)
    {
        // (4) Logika operator: jumlahkan field Value masing-masing operand
        int sum = a.Value + b.Value;

        // (5) Kembalikan Number baru dengan hasil penjumlahan
        return new Number(sum);
    }

    // (6) Override ToString agar mudah terbaca saat dicetak
    public override string ToString()
    {
        return Value.ToString();
    }
}


class Program
{
    static void Main()
    {
        // (A) Membuat dua instance Number
        Number n1 = new Number(5);   // panggil constructor -> Value = 5
        Number n2 = new Number(10);  // panggil constructor -> Value = 10

        // (B) Menggunakan operator '+' yang sudah dioverload
        //     Compiler menerjemahkan ini ke: Number.operator+(n1, n2)
        Number result = n1 + n2;     // memanggil method operator+, menghasilkan Number(15)
     // Number result = Number.operator+(n1, n2); // cara panggil manual
     


        // (C) Menampilkan hasil ke konsol
        //     Interpolasi string memanggil ToString() pada n1, n2, result
        Console.WriteLine($"{n1} + {n2} = {result}");
        // Output yang diharapkan: "5 + 10 = 15"
    }
}
