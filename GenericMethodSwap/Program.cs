class Program
{
    static void Main()
    {
        int x = 5;
        int y = 10;
        Console.WriteLine($"Before: x = {x}, y = {y}");

        Swap(ref x, ref y);

        Console.WriteLine($"After:  x = {x}, y = {y}");

        string s1 = "Apple", s2 = "Orange";
        Console.WriteLine($"\nBefore: s1 = {s1}, s2 = {s2}");

        Swap(ref s1, ref s2);

        Console.WriteLine($"After:  s1 = {s1}, s2 = {s2}");
    }

    // ref = mengubah a atau b di dalam method, 
    // perubahan itu langsung memengaruhi variabel asli di luar method
    static void Swap<Z>(ref Z a, ref Z b)
    {
        Z temp = a;
        a = b;
        b = temp;
    }
}
