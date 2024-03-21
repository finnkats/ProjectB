using System;
using System.Collections.Generic;
using System.Threading;

namespace Logic;
public static class AccountLogic
{
    public static void Login(List<AccountDataModel>? accountData = null)
    {
        // Check if a user is already logged in
        if (AccountPresentation.CheckLoggedIn()) return;

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
            var (loginName, loginPassword) = AccountPresentation.GetLoginDetails();
            bool found = false;

            foreach (var data in accountDataList)
            {
                if (CheckAdmin(loginName, loginPassword, data))
                {
                    found = true;
                    // Add Admin features
                    App.HomePage.AddCurrentOption("Admin Features");

                    App.LoggedInUsername = loginName; // Set the LoggedInUsername property

                    AccountPresentation.PrintSuccess($"Logged in as administrator {loginName}");
                    loginLoop = false;
                }

                if (data.Customers != null){
                    foreach (var customer in data.Customers)
                    {
                        if (CheckCustomer(loginName, loginPassword, customer))
                        {
                            found = true;
                            // Add Customer Logged-In options
                            App.HomePage.AddCurrentOption("View Tickets");
                            App.HomePage.AddCurrentOption("View Notifications");
                            App.HomePage.AddCurrentOption("Edit Account Settings");

                            AccountPresentation.PrintSuccess($"Welcome back {customer.Name}");
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

            if (!found) AccountPresentation.LoginFailure();
        }
    }

    public static bool CheckAdmin(string? loginName, string? loginPassword, AccountDataModel data){
        if (data.Admin != null && data.Admin.AdminName == loginName && data.Admin.AdminPassword == loginPassword){
            return true;
        }
        return false;
    }

    public static bool CheckCustomer(string? loginName, string? loginPassword, AccountDataModel.CustomerAccount customer){
        if (customer.Name == loginName && customer.Password == loginPassword){
            return true;
        }
        return false;
    }

    public static void Logout()
    {
        AccountPresentation.PrintLogout();
        
        App.LoggedInUsername = null;

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
