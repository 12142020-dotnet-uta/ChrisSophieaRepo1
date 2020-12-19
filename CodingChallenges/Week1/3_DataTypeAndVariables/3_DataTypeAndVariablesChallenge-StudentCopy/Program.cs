using System;

namespace _3_DataTypeAndVariablesChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            byte b = 3;
            Console.WriteLine(b);
            sbyte sb = -3;
            Console.WriteLine(sb);
            int i = -2147483648;
            Console.WriteLine(i);
            uint ui = 4294967295;
            Console.WriteLine(ui);
            short s = -32768;
            Console.WriteLine(s);
            ushort us = 65535;
            Console.WriteLine(us);
            long l = -9223372036854775808;
            Console.WriteLine(l);
            ulong ul = 18446744073709551615;
            Console.WriteLine(ul);
            float f = -3.402823e38F;
            Console.WriteLine(f);
            double d = -1.79769313486232e307D;
            Console.WriteLine(d);
            char c = 'c';
            Console.WriteLine(c);
            string st = "String";
            Console.WriteLine(st);
            string st2 = "1121";
            Console.WriteLine(st2);
            bool boo = true;
            Console.WriteLine(boo);
            decimal dec = 3.1415179M;
            Console.WriteLine(dec);

            Console.WriteLine(Text2Num(st2));



        }

        public static int Text2Num(string numText)
        {
            int outputNumber = Int32.Parse(numText);
            return outputNumber;
        }
    }
}
