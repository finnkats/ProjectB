using System;
using System.Collections.Generic;

namespace Logic
{
    public static class AccountLogic
    {
        public static void Login(List<AccountDataModel> accountData = null)
        {
            bool loginLoop = true;
            bool adminAccount = false;
            bool customerAccount = true;

            List<AccountDataModel> accountDataList;

            if (accountData == null)
            {
                accountDataList = AccountDataAccess.LoadAll(); // Load data from JSON file
            }
            else
            {
                accountDataList = accountData; // Use provided account data
            }

            while (loginLoop)
            {
                Console.WriteLine("Name: ");
                string loginName = Console.ReadLine(); // Input

                Console.WriteLine("Password: ");
                string loginPassword = Console.ReadLine(); // Input

                bool found = false;

                foreach (var data in accountDataList)
                {
                    if (data.Admin != null && data.Admin.AdminName == loginName && data.Admin.AdminPassword == loginPassword)
                    {
                        found = true;
                        adminAccount = true;
                        Console.WriteLine("Login successful!");
                        Console.WriteLine($"Logged in as administrator {data.Admin.AdminName}");
                        loginLoop = false;
                        break;
                    }

                    foreach (var customer in data.Customers)
                    {
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
                    break;
                }
            }
        }
    }
}
