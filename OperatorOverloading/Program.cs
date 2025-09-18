/*
    Operator Overloading in C#
    - Operator overloading memungkinkan kita untuk mendefinisikan ulang
      perilaku operator untuk tipe data yang kita buat sendiri (custom types).
    - Dengan operator overloading, kita bisa membuat kode yang lebih intuitif
      dan mudah dibaca ketika bekerja dengan objek dari tipe data tersebut.
    - Untuk meng-overload operator, kita perlu mendefinisikan metode STATIC
      dengan kata kunci `operator` diikuti oleh operator yang ingin di-overload.
    - Beberapa operator yang umum di-overload antara lain: +, -, *, /, ==, !=, <, >, dll.
    - Ketika meng-overload operator, pastikan bahwa perilaku
      operator tersebut make sense untuk tipe data yang kita buat. Misalnya,
        operator `+` untuk tipe data `Vector` bisa diartikan sebagai penjumlahan
        vektor, sedangkan untuk tipe data `String` bisa diartikan sebagai
        penggabungan string.

    - Jika meng-overload operator `==`, kita juga harus meng-overload operator `!=`
        dan override metode `Equals` serta `GetHashCode` untuk memastikan konsistensi.
    - Operator overloading harus dilakukan di dalam definisi tipe data (class/struct).

    - Sebuah operator overload harus berupa public static method di dalam class atau struct 
        yang tipenya digunakan pada operator itu.

    - Operator khusus yang tidak boleh dioverload:
        1. Assignment (=)
        2. Conditional (&&, ||)
        3. Ternary (?:)
        4. Member access (., ->, [])
        5. Type testing (is, as)
        6. Pointer-related (*, &, sizeof, typeof)
*/


using System;

class Complex
{
    public double Real { get; }
    public double Imaginer { get; }

    public Complex(double real, double imaginer)
    {
        Real = real;
        Imaginer = imaginer;
    }

    // Overload operator +
    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex(c1.Real + c2.Real, c1.Imaginer + c2.Imaginer);
    }

    // Overload operator -
    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex(c1.Real - c2.Real, c1.Imaginer - c2.Imaginer);
    }

    // Overload operator ==
    public static bool operator ==(Complex c1, Complex c2)
    {
        return (c1.Real == c2.Real) && (c1.Imaginer == c2.Imaginer);
    }

    // Overload operator !=
    public static bool operator !=(Complex c1, Complex c2)
    {
        return !(c1 == c2);
    }

    // Override Equals dan GetHashCode jika overload == dan !=
    public override bool Equals(object obj)
    {
        if (obj is Complex c)
            return this == c;
        return false;
    }

    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginer.GetHashCode();
    }

    public override string ToString() => $"{Real} + {Imaginer}i";
}

class Program
{
    static void Main()
    {
        Complex a = new Complex(2, 3);
        Complex b = new Complex(1, 4);

        Console.WriteLine(a + b);  // 3 + 7i
        Console.WriteLine(a - b);  // 1 - 1i
        Console.WriteLine(a == b); // False
    }
}
