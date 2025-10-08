﻿namespace Calculator
{
    public class MathService
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }
        public int Multiply(int a, int b)
        {
            return a * b;
        }
        public double Divide(int a, int b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return (double)a / b;
        }
    }
}

// public class MathService
// {
//     public int Add(int a, int b) => a + b;
//     public int Subtract(int a, int b) => a - b;
//     public int Multiply(int a, int b) => a * b;
//     public double Divide(int a, int b)
//     {
//         if (b == 0)
//             throw new DivideByZeroException();
//         return (double)a / b;
//     }
// }
