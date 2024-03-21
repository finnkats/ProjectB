using System;
using System.Collections.Generic;

namespace Logic
{
    public static class AccountLogic
    {
        public static void Login(List<AccountDataModel> accountData = null)
        {
            bool loginLoop = true; // Variable used to keep asking name and password, in case name or password are wrong
            bool adminAccount = false; // Variable used to check if user who logged in is admin or not
            bool customerAccount = true; // Variable used to check if user who logged in is customer or not

            List<AccountDataModel> accountDataList;

            if (accountData == null)
            {
                accountDataList = AccountDataAccess.LoadAll(); // Load data from JSON file
            }
            else
            {
                accountDataList = accountData; // Use provided account data, if it exists.
            }

            while (loginLoop)  // While true
            {
                Console.WriteLine("Name: ");
                string loginName = Console.ReadLine(); // Input

                Console.WriteLine("Password: ");
                string loginPassword = Console.ReadLine(); // Input

                bool found = false; 

                foreach (var data in accountDataList) // Loops through data list
                {   
                    // If account data is correct, execute this
                    if (data.Admin != null && data.Admin.AdminName == loginName && data.Admin.AdminPassword == loginPassword)
                    {
                        found = true;
                        adminAccount = true;
                        Console.WriteLine("Login successful!");
                        Console.WriteLine($"Logged in as administrator {data.Admin.AdminName}");
                        loginLoop = false;
                        break;
                    }

                    // Loops through the "CustomerAccount" value, which is a list with customer account data
                    foreach (var customer in data.Customers)
                    {   
                        // If account data is correct, execute this
                        if (customer.Name == loginName && customer.Password == loginPassword)
                        {
                            found = true;
                            customerAccount = true;
                            Console.WriteLine("Login successful!");
                            Console.WriteLine($"Welcome back {customer.Name}");
                            loginLoop = false;
                            break;
                        }
                    }

                    if (found)
                        break;
                }

                if (!found)
                {
                    Console.WriteLine("Invalid name or password. Please try again.\n");
                    break; // Breaks because of unit test, after it's merged remove this
                }
            }
        }
    }
}
