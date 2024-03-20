using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    public static List<AccountDataModel> LoadAll(string jsonPath = null)
    {
        if (jsonPath == null)
        {
            jsonPath = @"DataSources/AccountData.json";
        }

        string json = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<List<AccountDataModel>>(json);
    }
}
