/*
    - Di C#, Value Type (seperti int, double, bool, struct, DateTime, dll) 
        tidak bisa bernilai null secara default.
    - Nullable Value Type memiliki dua properti penting:
        HasValue ---> true kalau ada nilai.
        Value    ---> untuk mengambil nilai (kalau ada).
    - Untuk membuat Nullable Value Type, kita bisa menggunakan:
        1. Nullable<T> (cara panjang)
        2. T? (cara pendek)
    - Untuk memeriksa apakah Nullable Value Type memiliki nilai, kita bisa menggunakan properti HasValue.
    - Untuk mengambil nilai dari Nullable Value Type, kita bisa menggunakan properti Value.
    - Jika kita mencoba mengakses properti Value ketika HasValue bernilai false, 
      maka akan terjadi InvalidOperationException.
    - Kita juga bisa menggunakan operator null-coalescing (??) untuk memberikan nilai default
        jika Nullable Value Type bernilai null.

*/
//Nullable<T> 
Nullable<int> a     = new Nullable<int>();  //cara panjang
int? b              = null;                 // cara pendek

Nullable<int> c;  // cara panjang
int? d;           // cara singkat


// Null-Coalescing Operator (??)
int? e = null;
int f = e ?? -1;   // jika x null, maka y = -1
Console.WriteLine(f); // output: -1

// Null-Conditional Operator (?.)
int? g = 10;
Console.WriteLine(g?.ToString()); // output: "10"

int? h = null;
Console.WriteLine(h?.ToString()); // output: ""


System.Text.StringBuilder sb = null;
int? length = sb?.ToString().Length; // 'sb' is null, so 'length' becomes null
Console.WriteLine($"SB = {length}");


int length2 = sb?.ToString().Length ?? 0; // If 'sb' is null, 'length' becomes 0 instead of null
Console.WriteLine($"SB2 = {length2}");




// Boxing dan Unboxing dengan Nullable Value Type
int? i = 5;
object obj = i; // Boxing
int? j = (int?)obj; // Unboxing
Console.WriteLine(j.HasValue); // True
Console.WriteLine(j.Value);    // 5

// Menggunakan 'as' dengan Nullable Value Type
object o = "string"; 
int? x = o as int?; // 'o' is not an int, so 'x' becomes null
Console.WriteLine(x.HasValue); // False


