using System;
using System.Collections.Generic;
using System.Threading;

namespace Logic;
public static class AccountLogic
{
    public static void Login(List<AccountDataModel>? accountData = null)
    {
        // Check if a user is already logged in
        if (!string.IsNullOrEmpty(App.LoggedInUsername))
        {
            Console.WriteLine("You are already logged in as: " + App.LoggedInUsername);
            return; // Prevent further login attempts
        }

        List<AccountDataModel> accountDataList;

        if (accountData == null)
        {
            accountDataList = AccountDataAccess.LoadAll();
        }
        else
        {
            accountDataList = accountData;
        }

        bool loginLoop = true;
        while (loginLoop)
        {
            Console.WriteLine("Name: ");
            string? loginName = Console.ReadLine();

            Console.WriteLine();

            Console.WriteLine("Password:");
            string? loginPassword = Console.ReadLine();

            bool found = false;

            foreach (var data in accountDataList)
            {
                if (data.Admin != null && data.Admin.AdminName == loginName && data.Admin.AdminPassword == loginPassword)
                {
                    found = true;
                    // Add Admin features
                    App.HomePage.AddCurrentOption("Admin Features");
                    
                    App.LoggedInUsername = loginName; // Set the LoggedInUsername property
                    Console.WriteLine("\nLogin successful!");
                    Thread.Sleep(1500);
                    Console.WriteLine($"Logged in as administrator {data.Admin.AdminName}");
                    Thread.Sleep(2000);
                    loginLoop = false;
                }

                if (data.Customers != null){
                    foreach (var customer in data.Customers)
                    {
                        if (customer.Name == loginName && customer.Password == loginPassword)
                        {
                            found = true;
                            // Add Customer Logged-In options
                            App.HomePage.AddCurrentOption("View Tickets");
                            App.HomePage.AddCurrentOption("View Notifications");
                            App.HomePage.AddCurrentOption("Edit Account Settings");

                            Console.WriteLine("\nLogin successful!");
                            Thread.Sleep(1500);
                            Console.WriteLine($"Welcome back {customer.Name}");
                            Thread.Sleep(2000);
                            loginLoop = false;
                            break;
                        }
                    }
                }
                
                if (found)
                {
                    App.LoggedInUsername = loginName;
                    // Remove sign in / up option from frontpage, and add logout
                    App.FrontPage.RemoveCurrentOption("Sign in / up");
                    App.FrontPage.AddCurrentOption("Logout");
                    App.HomePage.SetToCurrentMenu();
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Invalid name or password. Please try again.\n");
            }
        }
    }

    public static void Logout()
    {
        // Check if a user is logged in
        if (!string.IsNullOrEmpty(App.LoggedInUsername))
        {
            Console.WriteLine($"Logging out user: {App.LoggedInUsername}");
            App.LoggedInUsername = null; // Clear the logged-in user
            Thread.Sleep(1500);


            // Remove all options which has to do with someone being logged in
            App.FrontPage.RemoveCurrentOption("Logout");
            App.HomePage.RemoveCurrentOption("View Tickets");
            App.HomePage.RemoveCurrentOption("View Notifications");
            App.HomePage.RemoveCurrentOption("Edit Account Settings");
            App.HomePage.RemoveCurrentOption("Admin Features");

            App.FrontPage.AddCurrentOption("Sign in / up");
            App.FrontPage.SetToCurrentMenu();
        }
        else
        {
            Console.WriteLine("No user is currently logged in.");
        }
    }
}
