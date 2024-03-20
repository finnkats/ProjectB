using System;
using System.Linq;

namespace Logic
{
    public static class AccountLogic
    {
        public static void Login()
        {
            bool loginLoop = true;
            bool adminAccount = false;
            bool customerAccount = true;

            while (loginLoop) // If login data doesn't exist, it keeps asking until it's right
            {
                Console.WriteLine("Name: ");
                string loginName = Console.ReadLine(); // Asks for input

                Console.WriteLine("Password: "); // Asks for input
                string loginPassword = Console.ReadLine();

                var accountDataList = AccountDataAccess.LoadAll(); // List with dicts gets stored in 'accountDataList' variable

                bool found = false;

                foreach (var accountData in accountDataList)
                {
                    // Checks if admin login data is correct
                    if (accountData.Admin != null && accountData.Admin.AdminName == loginName && accountData.Admin.AdminPassword == loginPassword)
                    {
                        found = true;
                        adminAccount = true;
                        Console.WriteLine("Login successful!");
                        Console.WriteLine($"Logged in as administrator {accountData.Admin.AdminName}");
                        loginLoop = false; // Exit the login loop
                        break;
                    }

                    // Loops through list of account data from Customers key
                    foreach (var customer in accountData.Customers)
                    {
                        // Checks if customer login data is correct
                        if (customer.Name == loginName && customer.Password == loginPassword)
                        {
                            found = true;
                            customerAccount = true;
                            Console.WriteLine("Login successful!");
                            Console.WriteLine($"Welcome back {customer.Name}");
                            loginLoop = false; // Exit the login loop
                            break;
                        }
                    }

                    if (found)
                        break;
                }

                if (!found)
                {
                    Console.WriteLine("Invalid name or password. Please try again.\n");
                }
            }
        }
    }
}
