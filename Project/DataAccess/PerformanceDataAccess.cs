using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class PerformanceDataAccess
{
    public static Dictionary<string, Performance> ReadPerformances(){
        var PerformancesJson = File.ReadAllText(@"DataSources/performances.json");
        var Performances = JsonSerializer.Deserialize<Dictionary<string, Performance>>(PerformancesJson);
        return Performances ?? new Dictionary<string, Performance>();
    }

    public static void UpdatePerformances(){
        var PerformancesJson = JsonSerializer.Serialize(App.Performances);
        File.WriteAllText(@"DataSources/performances.json", PerformancesJson);
    }
}
