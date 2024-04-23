using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    // Dictionary where the key is a string of ID ("ID0") and its value is an Account object
    // and puts it into App.Accounts
    public static Dictionary<string, Account> ReadAccounts()
    {   
        string json = File.ReadAllText(@"DataSources/accounts.json");
        var accountDataList = JsonSerializer.Deserialize<Dictionary<string, Account>?>(json); // Convert to dictionary, with AccountDataModel object as value
        return accountDataList == null ? new Dictionary<string, Account>() : accountDataList; 
    }

    // Updates accounts.json with App.Accounts
    public static void UpdateAccounts(){
        string jsonPath = @"DataSources/accounts.json";

        string json = JsonSerializer.Serialize(App.Accounts);
        File.WriteAllText(jsonPath, json);
    }
}
