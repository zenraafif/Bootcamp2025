using System;

class Person : IComparable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public int CompareTo(Person other)
    {
        if (other == null) return 1;
        return this.Age.CompareTo(other.Age);
    }
}

class Program
{
    static void Main()
    {
        int x = 5, y = 10;
        Console.WriteLine(Max(x, y)); 

        string s1 = "Apple", s2 = "Orange";
        Console.WriteLine(Max(s1, s2));

        double d1 = 3.14, d2 = 2.71;
        Console.WriteLine(Max(d1, d2));

        var p1 = new Person { Name = "Alice", Age = 25 };
        var p2 = new Person { Name = "Bob", Age = 30 };

        var older = MaxOld(p1, p2);
        Console.WriteLine($"Older person: {older.Name}");
    }

    static T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    static T MaxOld<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }
}
