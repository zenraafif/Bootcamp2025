using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        // =============================
        // BAGIAN 1: Bool <-> String
        // =============================
        string s = true.ToString();     // Convert bool -> string
        bool b = bool.Parse(s);         // Convert string -> bool
        Console.WriteLine($"String: {s}, Bool: {b}");

        // =============================
        // BAGIAN 2: Numeric parsing dengan TryParse
        // =============================
        bool failure = int.TryParse("qwerty", out int i1); // gagal parsing
        bool success = int.TryParse("123", out int i2);    // berhasil parsing
        Console.WriteLine($"Failure: {failure}, i1: {i1}");
        Console.WriteLine($"Success: {success}, i2: {i2}");

        bool wasParsed = int.TryParse("456", out int result);
        Console.WriteLine($"Was parsed: {wasParsed}, Result: {result}");

        // Jika hanya peduli valid / tidak, abaikan hasil dengan "_"
        bool isValidInput = int.TryParse("789", out int _);
        Console.WriteLine($"Is valid input: {isValidInput}");

        // =============================
        // BAGIAN 3: Culture-specific parsing
        // =============================
        // Catatan: hasil tergantung culture default sistem
        Console.WriteLine(double.Parse("1.234"));   // Bisa jadi 1234 (jika '.' = pemisah ribuan)
        Console.WriteLine(double.Parse("1,234"));   // Bisa jadi 1.234 (jika ',' = decimal)

        // Gunakan InvariantCulture agar konsisten
        double x = double.Parse("1.234", CultureInfo.InvariantCulture); 
        string y = 1.234.ToString(CultureInfo.InvariantCulture);        
        Console.WriteLine($"Invariant parse: {x}, ToString: {y}");

        // =============================
        // BAGIAN 4: Custom NumberFormatInfo
        // =============================
        NumberFormatInfo f = new NumberFormatInfo();
        f.CurrencySymbol = "RPiah";          // Simbol mata uang custom
        f.CurrencyDecimalDigits = 3;      // 3 angka desimal
        f.CurrencyGroupSeparator = "_";   // Pemisah ribuan pakai underscore

        Console.WriteLine(3.ToString("C", f));        // $$3.000
        Console.WriteLine(1234567.ToString("C", f));  // $$1_234_567.000
    }
}
