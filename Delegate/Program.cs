using System;


class Test
{
    //int Square(int x) => x * x; //short form
    public static int Square2(int x)
    {
        return x * x;
    }
    // ---------------STATIC METHOD------------------
    // public int Square(int x)
    // {
    //     return x * x;
    // }
    // ---------------INSTANCE METHOD------------------
}


class DelegateExample
{
    // delegate void Transformer2(int x);
    // static void Square(int x)
    // {
    //     Console.WriteLine(x * x);

    // }
    // ---------------DELEGATES------------------

    // delegate int Transformer(int x);

    // static int Square(int x) => x * x;
    // static int Cube(int x) => x * x * x;

    // static void Transform(int[] values, Transformer t) // 't' is a delegate parameter
    // {
    //     for (int i = 0; i < values.Length; i++)
    //         values[i] = t(values[i]); // Invoke the plug-in method
    // }
    // ----------------PLUGIN DELEGATES-----------------

    delegate int Transformer(int x);// Declare a delegate type
    // ---------------STATIC AND INSTANCE METHOD------------------


    public static void Main()
    {
        // Transformer2 tDel = Square;    // Create delegate instance
        // tDel(3);         // Invoke delegate
        // ----------------DELEGATES-----------------


        // int[] values = { 1, 2, 3 };
        // // Transform(values, Square); // Use Square method as the plug-in
        // Transform(values, Cube);// Use Cube method as the plug-in
        // foreach (int i in values)
        //     Console.Write(i + "  ");
        // ----------------PLUGIN DELEGATES-----------------

        Transformer tDel = new Transformer(Test.Square2);// Create delegate instance
        int result = tDel.Invoke(10); // Invoke delegate
        Console.WriteLine(result);
        // // ---------------STATIC METHOD------------------

        // Test test = new Test();// Create instance of Test class
        // Transformer t = new Transformer(test.Square);// Create delegate instance
        // int result = t.Invoke(10);// Invoke delegate
        // Console.WriteLine(result);
        // ---------------INSTANCE METHOD------------------

    }
}
