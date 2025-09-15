using System;

readonly struct Point
{
    public readonly int x;
    readonly int y;

    public Point()
    {
        x = 1;
        y = 1;
    }

    public override string ToString() => $"x = {x}, y = {y}";
}

struct Titik
{
    public string a = "111";
    string b;

    public Titik() => b = "222";

    public override string ToString() => $"x = {a}, y = {b}";
}

// --- Perbandingan dengan ref struct ---
ref struct PointRef
{
    public int X, Y;
}

// var points = new PointRef[100];           // Error: Arrays live on the heap
// class MyClass { PointRef P; }             // Error: Class fields live on the heap
// object obj = new PointRef();              // Error: Boxing sends to the heap


class Program
{
    static void Main()
    {

        Point p1 = new Point();
        // p1.x = 10; // ERROR: Cannot assign to 'x' because it is readonly
        Console.WriteLine("p1 (new Point()): " + p1);


        Titik p2 = new Titik();
        p2.a = "10";
        Console.WriteLine("p2 (default): " + p2);

        Point p3 = default(Point);
        Console.WriteLine("p3 (default(Point)): " + p3);

        Titik p4 = default;
        Console.WriteLine("p4 (default): " + p4);

        PointRef pr = new PointRef { X = 5, Y = 10 };
        Console.WriteLine($"PointRef: X = {pr.X}, Y = {pr.Y}");
    }
}
