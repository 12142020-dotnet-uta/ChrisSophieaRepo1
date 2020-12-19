using System;

namespace _8_LoopsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UseFor();
            UseDoWhile();
            UseWhile();
        }

        public static void UseFor()
        {
            Console.WriteLine("\nOdd Numbers 0-50: ");
            for (int i = 0; i <= 50; ++i)
            {
                if (i % 2 != 0)
                {
                    Console.Write(i + " ");
                }
            }
        }

        public static void UseDoWhile()
        {
            Console.WriteLine("\nEven Numbers 0-50: ");
            int x = 0;
            do
            {
                if (x % 2 == 0)
                {
                    Console.Write(x + " ");
                }
                ++x;
            } while (x <= 50);
            Console.WriteLine();


        }

        public static void UseWhile()
        {
            Console.WriteLine("\nMultiples of 3 up to 100, skipping multiples of 15: ");
            int x = 3;
            while (x <= 100)
            {
                if (x % 3 == 0)
                {
                    if (x % 15 == 0)
                    {
                        Console.WriteLine("skipping this number.");
                        ++x;
                    }
                    Console.WriteLine(x);
                    ++x;
                }
                ++x;

            }
        }
    }
}
// 2. create a do/while loop that displays the even integers from 0 to 50.
// 3. create a while loop that displays the multiples of 3 integers from 0 to 100. 
//     1. Design the loop so that when every multiple of 3 and 5 coincide(like 15, 30, etc), you print "skipping this number" instead of the number.
//     2. Design the loop so that when you get above 100 you automatically stop the loop with a break statement.