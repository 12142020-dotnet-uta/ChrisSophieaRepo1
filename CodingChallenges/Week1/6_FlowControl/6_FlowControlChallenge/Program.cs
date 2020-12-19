using System;

namespace _6_FlowControl
{
    public class Program
    {
        //create global variables to hold users login data.
        public static string username;
        public static string password;
        public static int temp;
        public static bool login;

        static void Main(string[] args)
        {

            Register();
            login = Login();
            while (!login)
            {
                Console.WriteLine("Login info incorrect. Try again.");
                login = Login();
            }
            int temperature = GetValidTemperature();
            GetTemperatureTernary(temperature);
            GiveActivityAdvice(temperature);




        }

        // This method gets a valid temperaturebetween -40 asnd 135 inclusive 
        // and returns the valid int.
        public static int GetValidTemperature()
        {

            Console.Write("Enter a valid temperature (-40 to 135): ");

            if (!int.TryParse(Console.ReadLine(), out temp))
            {
                Console.WriteLine("Not a valid input.");
                GetValidTemperature();
            }
            else if (temp < -40 || temp > 135)
            {
                Console.WriteLine($"{temp} is out of the valid range.");
                GetValidTemperature();
            }
            return temp;
        }

        // This method has one int parameter
        // It gives outdoor activity advice and temperature opinion based on 20 degree
        // increments starting at -20 and ending at 135 
        // n < -20 = hella cold
        // -20 <= n < 0 = pretty cold
        //  0 <= n < 20 = cold
        // 20 <= n < 40 = thawed out
        // 40 <= n < 60 = feels like Autumn
        // 60 <= n < 80 = perfect outdoor workout temperature
        // 80 <= n < 90 = niiice
        // 90 <= n < 100 = hella hot
        // 100 <= n < 135 = hottest
        public static void GiveActivityAdvice(int temp)
        {
            string result = temp < -20 ? "Hella cold!" :
                            temp < 0 ? "Pretty cold." :
                            temp < 20 ? "Cold." :
                            temp < 40 ? "Thawed out." :
                            temp < 60 ? "Feels like Autumn." :
                            temp < 80 ? "Perfect outdoor workout temperature." :
                            temp < 90 ? "Niiice. Bundle up and go ice skating." :
                            temp < 100 ? "Hella hot." : "Hottest.";

            Console.WriteLine(result);
        }

        // This method gets a username and password from the user
        // and stores that data in the global variables of the 
        // names in the method.
        public static void Register()
        {
            Console.WriteLine("--Registration--");
            Console.Write("Enter Username: ");
            username = Console.ReadLine();
            Console.Write("Enter Password: ");
            password = Console.ReadLine();

        }

        // This method gets username and password from the user and
        // compares them with the username and password global variables
        // or the names provided. If the password and username match,
        // the method returns true. If they do not match, the user is 
        // prompted again for the username and password.
        public static bool Login()
        {
            Console.WriteLine("--Login--");
            Console.Write("Enter Username: ");
            string loginUser = Console.ReadLine();
            Console.Write("Enter Password: ");
            string loginPassword = Console.ReadLine();

            if (loginUser == username && loginPassword == password) return true;
            return false;
        }

        // This method as one int parameter.
        // It chack is the int is <=42, between 
        // 43 and 78 inclusive, or > 78.
        // For each temperature range, a different 
        // advice is given. 
        public static void GetTemperatureTernary(int temp)
        {
            string result = temp <= 42 ? $"{temp} is too cold!" :
                            temp <= 78 ? $"{temp} is an ok temperature." : $"{temp} is too hot!";


            Console.WriteLine(result);
        }
    }//end of Program()
}
