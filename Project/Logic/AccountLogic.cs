using System;
using System.Collections.Generic;
using System.Threading;

namespace Logic;
public static class AccountLogic
{
    public static void Login(Dictionary<string, AccountDataModel>? accountData = null)
    {
        // Check if a user is already logged in
        if (AccountPresentation.CheckLoggedIn()) return;

        if (accountData == null) accountData = AccountDataAccess.LoadAll();

        bool loginLoop = true;
        while (loginLoop)
        {
            var (loginName, loginPassword) = AccountPresentation.GetLoginDetails();
            bool found = false;

            foreach (var account in accountData.Values)
            {
                if (!CheckLogin(loginName, loginPassword, account)) continue;
                if (account.IsAdmin)
                {
                    // Add Admin features
                    App.HomePage.AddCurrentOption("Admin Features");

                    App.LoggedInUsername = loginName; // Set the LoggedInUsername property

                    AccountPresentation.PrintSuccess($"Logged in as administrator {account.Name}");
                    loginLoop = false;
                }
                else {
                    // Add Customer Logged-In options
                    App.HomePage.AddCurrentOption("View Tickets");
                    App.HomePage.AddCurrentOption("View Notifications");
                    App.HomePage.AddCurrentOption("Edit Account Settings");

                    AccountPresentation.PrintSuccess($"Welcome back {account.Name}");
                    loginLoop = false;
                }
                
                App.LoggedInUsername = loginName;
                // Remove sign in / up option from frontpage, and add logout
                App.FrontPage.RemoveCurrentOption("Sign in / up");
                App.FrontPage.AddCurrentOption("Logout");
                App.HomePage.SetToCurrentMenu();
                found = true;
                break;
            }

            if (!found)
            loginLoop = AccountPresentation.LoginFailure() ? true : false;
        }
    }

    public static bool CheckLogin(string? loginName, string? loginPassword, AccountDataModel account){
        return (account.Name == loginName && account.Password == loginPassword);
    }

    public static void Logout()
    {
        AccountPresentation.PrintLogout();
        
        App.LoggedInUsername = "Unknown";

        // Remove all options which has to do with someone being logged in
        App.FrontPage.RemoveCurrentOption("Logout");
        App.HomePage.RemoveCurrentOption("View Tickets");
        App.HomePage.RemoveCurrentOption("View Notifications");
        App.HomePage.RemoveCurrentOption("Edit Account Settings");
        App.HomePage.RemoveCurrentOption("Admin Features");

        App.FrontPage.AddCurrentOption("Sign in / up");
        App.FrontPage.SetToCurrentMenu();
    }
}
