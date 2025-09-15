using System;
using System.Collections;
using System.Collections.Generic;

// Enumerator (cursor) - handles moving through the collection
class MyEnumerator<T> : IEnumerator<T>
{
    private T[] _items;   // the collection
    private int _position = -1; // current position (start before first)

    public MyEnumerator(T[] items)
    {
        _items = items;
    }

    // Current element (generic version)
    public T Current
    {
        get
        {
            if (_position < 0 || _position >= _items.Length)
                throw new InvalidOperationException();
            return _items[_position];
        }
    }

    // Non-generic Current (required by IEnumerator)
    object IEnumerator.Current => Current;

    // Move to next element
    public bool MoveNext()
    {
        if (_position < _items.Length - 1)
        {
            _position++;
            return true;
        }
        return false;
    }

    // Reset position to before the first element
    public void Reset()
    {
        _position = -1;
    }

    // Cleanup (not needed for array, but required by interface)
    public void Dispose()
    {
        // No unmanaged resources, so nothing to clean up
    }
}

// Enumerable (sequence) - provides enumerator
class MyEnumerable<T> : IEnumerable<T>
{
    private T[] _items;

    public MyEnumerable(T[] items)
    {
        _items = items;
    }

    // Generic enumerator
    public IEnumerator<T> GetEnumerator()
    {
        return new MyEnumerator<T>(_items);
    }

    // Non-generic enumerator (required by IEnumerable)
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Demo program
class Program
{
    static void Main()
    {
        var numbers = new MyEnumerable<int>(new int[] { 10, 20, 30, 40, 50, 60 });

        Console.WriteLine("Iterating custom enumerable:");
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
