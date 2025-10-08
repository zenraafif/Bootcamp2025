using System;

namespace Prime.Services
{
    public class PrimeService
    {
        // public bool IsPrime(int candidate)
        // {
        //     if (candidate < 2) // Handles -1, 0, and 1
        //     {
        //         return false;
        //     }
        //     throw new NotImplementedException("Please create a test first."); // Still awaits full prime logic
        // }

        public bool IsPrime(int candidate)
        {
            if (candidate < 2)
                return false;

            for (int i = 2; i <= Math.Sqrt(candidate); i++)
            {
                if (candidate % i == 0)
                    return false;
            }

            return true;
        }
    }
}