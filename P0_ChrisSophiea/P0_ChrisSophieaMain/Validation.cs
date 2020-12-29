using System;
using System.Collections.Generic;
using System.Linq;

namespace P0_ChrisSophiea
{
    public class Validation
    {
        DAOMethodsImpl db = new DAOMethodsImpl();
        public int vMainMenu()
        {
            int mainResponse;
            do
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("\n\t1. Login\n\t2. View Users\n\t3. Quit");
                //call a method to validate user input.
                if (!int.TryParse(Console.ReadLine(), out mainResponse) || mainResponse < 1 || mainResponse > 3)
                {
                    Console.WriteLine("\nInvalid Response. Please select from menu above");
                }
                // string logInOrQuit = Console.ReadLine();
                // bool logInOrQuitBool = int.TryParse(logInOrQuit, out logInOrQuitInt);
            } while (mainResponse != 1 && mainResponse != 2 && mainResponse != 3);// loop runs till the user selects 1 or 2
            return mainResponse;
        }

        public Customer vLoginMenu()
        {
            Customer user = null;
            do
            {
                Console.WriteLine("\n --- Login Menu ---");
                Console.WriteLine("Enter First Name, Last Name, Email");
                Console.Write("\tFirst Name: ");
                string fnameEntered = Console.ReadLine();
                Console.Write("\tLast Name: ");
                string lnameEntered = Console.ReadLine();
                Console.Write("\tEmail: ");
                string emailEntered = Console.ReadLine();
                if (!(fnameEntered is string) || fnameEntered.Length < 2 || fnameEntered.Length > 50)
                {
                    Console.WriteLine("\nFirst Name entered isn't valid.");
                }
                else if (!(lnameEntered is string) || lnameEntered.Length < 2 || lnameEntered.Length > 50)
                {
                    Console.WriteLine("\nLast Name entered isn't valid.");
                }
                else if (!(emailEntered is string) || emailEntered.Length < 7 || fnameEntered.Length > 50 || !(emailEntered.Contains("@")))
                {
                    Console.WriteLine("\nEmail entered is not valid.");
                }
                else
                {
                    user = new Customer()
                    {
                        Fname = fnameEntered,
                        Lname = lnameEntered,
                        EmailAddress = emailEntered
                    };
                }
            } while (user == null);
            return user;

        }

        public int vCustomerMenu()
        {
            int menuResponse;
            do
            {
                Console.WriteLine("\n--- Customer Menu ---");
                Console.WriteLine("\t1. View your past orders");
                Console.WriteLine("\t2. Shop");
                Console.WriteLine("\t3. Logout");
                if (!int.TryParse(Console.ReadLine(), out menuResponse) || menuResponse < 1 || menuResponse > 3)
                {
                    Console.WriteLine("\nInvalid input. Please select from menu above");
                }
            } while (menuResponse < 1 || menuResponse > 3);
            return menuResponse;
        }

        public Store vStoreMenu()
        {
            int storeResponse;
            int logout;
            int viewOrders;
            List<int> storeIds = new List<int>();

            do
            {
                Console.WriteLine("\n--- Select Store Location ---");
                foreach (Store s in db.GetAllStores())
                {
                    storeIds.Add(s.StoreId);
                    Console.WriteLine($"\t{s}");
                }

                logout = storeIds[storeIds.Count - 1] + 1;
                viewOrders = storeIds[storeIds.Count - 1] + 2;
                Console.WriteLine($"\t{logout}. Logout");


                if (!int.TryParse(Console.ReadLine(), out storeResponse) || !storeIds.Contains(storeResponse) && storeResponse != logout && storeResponse != viewOrders)
                {
                    Console.WriteLine("\nInvalid response. Enter a store number.");
                }
            } while (!storeIds.Contains(storeResponse) && storeResponse != logout);
            return db.GetStoreById(storeResponse);
        }

        public int vItemCategoryMenu()
        {
            int categoryResponse;
            do
            {
                Console.WriteLine("\n--- Shop Inventory by Item Category ---");
                Console.WriteLine("Please choose an option: ");
                Console.WriteLine("\n\t1. All\n\t2. Consoles\n\t3. Games\n\t4. Accessories\n\t5. View Orders From Store \n\t6. Return to Customer Menu");

                //call a method to validate user input.
                if (!int.TryParse(Console.ReadLine(), out categoryResponse) || categoryResponse < 1 || categoryResponse > 6)
                {
                    Console.WriteLine("Invalid Response. Please select from menu above");
                }
                // string logInOrQuit = Console.ReadLine();
                // bool logInOrQuitBool = int.TryParse(logInOrQuit, out logInOrQuitInt);
            } while (categoryResponse < 1 || categoryResponse > 6);// loop runs till the user selects 1 or 2
            return categoryResponse;
        }

        public string vShopMenu(ICollection<Inventory> inventory)
        {
            List<int> inventoryIds = new List<int>();
            string input;
            int shopMenuResponse;
            foreach (Inventory i in inventory)
            {
                inventoryIds.Add(i.InventoryId);
            }

            do
            {
                Console.Write("\n\tEnter the ID # of the item you wish to purchase,");
                Console.Write("\n\tor 'back' to return to category menu");
                Console.WriteLine("\n\tor 'check out' to complete your purchase.");
                input = Console.ReadLine();
                int.TryParse(input, out shopMenuResponse);
                if (!inventoryIds.Contains(shopMenuResponse) && !input.Equals("back", StringComparison.OrdinalIgnoreCase) && !input.Equals("check out", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nInvalid Response. Please select from options above or type 'back' to return or 'check out' to check out.");
                }

            } while (!inventoryIds.Contains(shopMenuResponse) && !input.Equals("back", StringComparison.OrdinalIgnoreCase) && !input.Equals("check out", StringComparison.OrdinalIgnoreCase));
            return input;
        }
    }
}