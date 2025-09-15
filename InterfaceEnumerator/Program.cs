using System;

public interface IEnumerator
{
    bool MoveNext();
    object Current { get; }
    void Reset();
}

internal class Countdown : IEnumerator
{
    int count = 11;

    public bool MoveNext() => count-- > 1;
    public object Current => count;
    public void Reset() { throw new NotSupportedException(); }
}

class Program
{
    static void Main()
    {
        IEnumerator e = new Countdown();

        while (e.MoveNext())
        {
            Console.WriteLine(e.Current);
        }

        Console.WriteLine();
    }
}
