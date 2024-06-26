using System;
using System.Collections.Generic;
using System.Threading;

namespace Logic;
public static class AccountLogic
{
    public static Logger logger = new AccountLogger();
    // This function can be called with or without login info
    public static void Login(string inputName = "", string inputPassword = "")
    {
        // Check if a user is already logged in
        if (AccountPresentation.CheckLoggedIn()) return;

        bool loginLoop = true;
        while (loginLoop)
        {
            string loginName, loginPassword;
            // See if the function got login info, otherwise ask for it
            if (inputName != "") (loginName, loginPassword) = (inputName, inputPassword);
            else (loginName, loginPassword) = AccountPresentation.GetLoginDetails();
            bool found = false;

            foreach (var account in App.Accounts.Values)
            {
                // Check if the current account is the correct account and validates the name and password
                if (!CheckLogin(loginName, loginPassword, account)) continue;
                if (account.IsAdmin)
                {
                    App.FrontPage.AddCurrentOption("Admin Features");
                    App.FrontPage.RemoveCurrentOption("Home Page");
                    App.LoggedInUsername = loginName; // Set the LoggedInUsername property

                    AccountPresentation.PrintSuccess($"Logged in as administrator {account.Name}");
                    loginLoop = false;
                    Console.Clear();
                }
                else
                {
                    // Add Customer Logged-In options
                    App.HomePage.AddCurrentOption("View Orders");
                    App.HomePage.AddCurrentOption("View Notifications");
                    App.HomePage.AddCurrentOption("Edit Account Settings");

                    AccountPresentation.PrintSuccess($"Welcome back {account.Name}");
                    loginLoop = false;
                }

                App.LoggedInUsername = loginName;
                logger.LogAction("Logged in");
                // Remove sign in / up option from frontpage, and add logout
                if (!account.IsAdmin) NotificationLogic.UpdateNotificationOption(true);
                App.FrontPage.RemoveCurrentOption("Log In / Create Account");
                App.FrontPage.AddCurrentOption("Log Out");
                if(account.IsAdmin) App.AdminFeatures.SetToCurrentMenu();
                else App.HomePage.SetToCurrentMenu();
                found = true;
                break;
            }

            if (!found)
            {
                if (inputName != "") return;
                loginLoop = AccountPresentation.LoginFailure(loginName) ? true : false;
            }
        }
    }

    public static bool CheckLogin(string? loginName, string? loginPassword, Account account)
    {
        return (account.Name == loginName && account.Password == loginPassword);
    }

    public static void Logout()
    {
        logger.LogAction("Logged out");
        AccountPresentation.PrintLogout();

        App.LoggedInUsername = "Unknown";

        // Remove all options which has to do with someone being logged in
        App.FrontPage.RemoveCurrentOption("Log Out");
        App.FrontPage.RemoveCurrentOption("Admin Features");
        App.HomePage.RemoveCurrentOption("View Orders");
        App.HomePage.RemoveCurrentOption("View Notifications");
        App.HomePage.RemoveCurrentOption("Edit Account Settings");
        App.HomePage.RemoveCurrentOption("Admin Features");
        NotificationLogic.UpdateNotificationOption(false);

        App.FrontPage.AddCurrentOption("Home Page");
        App.FrontPage.AddCurrentOption("Log In / Create Account");
        App.FrontPage.SetToCurrentMenu();
    }

    public static void CreateAccount()
    {
        (string name, string password) = AccountPresentation.GetLoginDetails(true);



        if (!AccountPresentation.DoubleCheckPassword(password)) return;
        if (name == "null" || name == ""){
            Console.WriteLine("\nInvalid account information\n\nPress any key to continue");
            Console.ReadKey();
            return;
        }



        if (App.Accounts.ContainsKey(name) || name == "Unknown")
        {
            AccountPresentation.PrintMessage("\n\nAccount with that name already exists.");
            return;
        }
        App.Accounts.Add(name, new Account(name, password, false));
        App.Notifications.Add(name, new List<Notification>());
        DataAccess.UpdateItem<Account>();

        logger.LogAction("Account created", new { Name = name });
        
        AccountPresentation.PrintMessage("\nAccount has been created.");
        Thread.Sleep(1000);
        AccountLogic.Login(name, password);
    }
}
