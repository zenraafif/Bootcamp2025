using System;
using System.Collections.Generic;

public class Stack<T> // Declares a type parameter T
{
    int position;
    T[] data = new T[100]; // Array of type T

    public void Push(T obj) => data[position++] = obj; // Accepts type T
    public T Pop() => data[--position];             // Returns type T
}


class StackGeneric
{
    public static void Main()
    {
        Stack<int> s = new Stack<int>();

        s.Push(1000);
        s.Push(2000);
        s.Push(3000);
        s.Push(4000);


        int x = s.Pop();
        int y = s.Pop();
        int z = s.Pop();
        s.Push(2137);
        int a = s.Pop();
        int b = s.Pop();

        Console.WriteLine($"x = {x}, y = {y}, z = {z}, a = {a}, b = {b}");

        // foreach (var item in s)
        // {
        //     Console.WriteLine(item);
        // }

    }
}