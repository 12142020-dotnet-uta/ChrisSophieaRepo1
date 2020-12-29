using System;

namespace StringManipulationChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            string userInputString; //this will hold your users message
            int elementNum;         //this will hold the element number within the messsage that your user indicates
            char char1;             //this will hold the char value that your user wants to search for in the message.
            string fName;           //this will hold the users first name
            string lName;           //this will hold the users last name
            string userFullName;    //this will hold the users full name;

            //
            //
            //implement the required code here and within the methods below.
            //
            //
            Console.WriteLine("--String to Upper Test--");
            Console.WriteLine("Enter word to make uppercase: ");
            userInputString = Console.ReadLine();
            StringToUpper(userInputString);

            Console.WriteLine("--String to Lower Test--");
            Console.WriteLine("Enter word to make lowercase: ");
            userInputString = Console.ReadLine();
            StringToLower(userInputString);

            Console.WriteLine("--Trim String Test--");
            Console.WriteLine("Enter string to trim whitespace: ");
            userInputString = Console.ReadLine();
            StringTrim(userInputString);

            Console.WriteLine("--String Substring Test--");
            Console.WriteLine("Enter word: ");
            userInputString = Console.ReadLine();
            Console.WriteLine("Enter substring index: ");
            elementNum = Int32.Parse(Console.ReadLine());
            StringSubstring(userInputString, elementNum);

            Console.WriteLine("--String Search Char Test--");
            Console.WriteLine("Enter string: ");
            userInputString = Console.ReadLine();
            Console.WriteLine("Enter char to find it's index: ");
            char1 = char.Parse(Console.ReadLine());
            SearchChar(userInputString, char1);

            Console.WriteLine("--String Concat Test--");
            Console.WriteLine("Enter first name: ");
            fName = Console.ReadLine();
            Console.WriteLine("Enter last name: ");
            lName = Console.ReadLine();
            ConcatNames(fName, lName);

        }

        // This method has one string parameter. 
        // It will:
        // 1) change the string to all upper case, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringToUpper(string x)
        {
            string result = x.ToUpper();
            Console.WriteLine(result);
            return result;
        }


        // This method has one string parameter. 
        // It will:
        // 1) change the string to all lower case, 
        // 2) print the result to the console and 
        // 3) return the new string.        
        public static string StringToLower(string x)
        {
            string result = x.ToLower();
            Console.WriteLine(result);
            return result;
        }

        // This method has one string parameter. 
        // It will:
        // 1) trim the whitespace from before and after the string, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringTrim(string x)
        {
            char toTrim = ' ';
            string result = x.Trim(toTrim);
            Console.WriteLine(result);
            return result;
        }

        // This method has two parameters, one string and one integer. 
        // It will:
        // 1) get the substring based on the integer received, 
        // 2) print the result to the console and 
        // 3) return the new string.
        public static string StringSubstring(string x, int elementNum)
        {
            string result = x.Substring(elementNum);
            Console.WriteLine(result);
            return result;
        }

        // This method has two parameters, one string and one char.
        // It will:
        // 1) search the string parameter for the char parameter
        // 2) return the index of the char.
        public static int SearchChar(string userInputString, char x)
        {
            Console.WriteLine(userInputString.IndexOf(x));
            return userInputString.IndexOf(x);
        }

        // This method has two string parameters.
        // It will:
        // 1) concatenate the two strings with a space between them.
        // 2) return the new string.
        public static string ConcatNames(string fName, string lName)
        {
            Console.WriteLine(fName + " " + lName);
            return fName + " " + lName;
        }



    }//end of program
}
