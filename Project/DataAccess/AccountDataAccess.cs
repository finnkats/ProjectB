using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    public static List<AccountDataModel> LoadAll(string jsonPath = null)
    {   
        // If a value is not passed as a parameter, execute this
        if (jsonPath == null)
        {
            jsonPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/AccountData.json"));
        }

        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<List<AccountDataModel>>(json); // Convert to list, with AccountDataModel objects
    }
}
