using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    public static Dictionary<string, AccountDataModel> LoadAll(string? jsonPath = null)
    {   
        // If a value is not passed as a parameter, execute this
        if (jsonPath == null)
        {
            jsonPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/AccountData.json"));
        }

        string json = File.ReadAllText(jsonPath);
        var accountDataList = JsonSerializer.Deserialize<Dictionary<string, AccountDataModel>?>(json); // Convert to dictionary, with AccountDataModel object as value
        return accountDataList == null ? new Dictionary<string, AccountDataModel>() : accountDataList; 
    }

    public static void WriteAll(Dictionary<string, AccountDataModel> Accounts, string? jsonPath = null){
        if (jsonPath == null)
        {
            jsonPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/AccountData.json"));
        }

        string json = JsonSerializer.Serialize(Accounts);
        File.WriteAllText(jsonPath, json);
    }
}
