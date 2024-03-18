using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class AccountDataAccess
{
    static string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/AccountData.json"));

    public static List<AccountDataModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AccountDataModel>>(json);
    }
}
