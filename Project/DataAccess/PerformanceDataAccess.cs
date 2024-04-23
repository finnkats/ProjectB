using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class PerformanceDataAccess
{
    // Dictionary where the key is a string of ID ("ID0") and its value is an Performance object
    // and puts it into App.Performances
    public static Dictionary<string, Performance> ReadPerformances(){
        var PerformancesJson = File.ReadAllText(@"DataSources/performances.json");
        var Performances = JsonSerializer.Deserialize<Dictionary<string, Performance>>(PerformancesJson);
        return Performances ?? new Dictionary<string, Performance>();
    }

    // Updates performances.json with App.Performances
    public static void UpdatePerformances(){
        var PerformancesJson = JsonSerializer.Serialize(App.Performances);
        File.WriteAllText(@"DataSources/performances.json", PerformancesJson);
    }
}
