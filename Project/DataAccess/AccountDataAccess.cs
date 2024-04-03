using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    public static Dictionary<string, Account> LoadAll()
    {   
        string json = File.ReadAllText(@"DataSources/accounts.json");
        var accountDataList = JsonSerializer.Deserialize<Dictionary<string, Account>?>(json); // Convert to dictionary, with AccountDataModel object as value
        return accountDataList == null ? new Dictionary<string, Account>() : accountDataList; 
    }

    public static void WriteAll(Dictionary<string, Account> Accounts, string? jsonPath = null){
        if (jsonPath == null)
        {
            jsonPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
        }

        string json = JsonSerializer.Serialize(Accounts);
        File.WriteAllText(jsonPath, json);
    }
}
