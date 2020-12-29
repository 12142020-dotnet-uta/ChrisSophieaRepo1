using System;

namespace _4_MethodsChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1
            string name = GetName();
            GreetFriend(name);

            //2
            double result1 = GetNumber();
            double result2 = GetNumber();
            int action1 = GetAction();
            double result3 = DoAction(result1, result2, action1);

            System.Console.WriteLine($"The result of your mathematical operation is {result3}.");


        }

        public static string GetName()
        {
            Console.WriteLine("Enter name: ");
            return Console.ReadLine();
        }

        public static void GreetFriend(string name)
        {
            //Greeting should be: Hello, nameVar. You are my friend
            //Ex: Hello, Jim. You are my friend
            Console.WriteLine("Hello, " + name + ". You are my friend.");
        }

        public static double GetNumber()
        {
            Console.WriteLine("Enter a number: ");
            string userInput = Console.ReadLine();
            double number;

            if (!Double.TryParse(userInput, out number))
            {
                throw new FormatException();
            }
            return number;
        }

        public static int GetAction()
        {
            Console.WriteLine("Enter the number corresponding to the mathmatical operation you want. 1)add 2)subtract, 3)multiply, or 4)divide: ");
            //Should throw FormatException if the user did not input a number
            int number;
            if (!Int32.TryParse(Console.ReadLine(), out number))
            {
                throw new FormatException();
            }
            return number;
        }

        public static double DoAction(double x, double y, int z)
        {
            switch (z)
            {
                case 1: return (x + y);
                case 2: return (x - y);
                case 3: return (x * y);
                case 4: return (x / y);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
