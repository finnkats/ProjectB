using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class PerformanceDataAccess
{
    public static Dictionary<string, Performance> ReadPerformances(){
        var PerformancesJson = File.ReadAllText(@"DataSources/performances.json");
        var Performances = JsonSerializer.Deserialize<Dictionary<string, Play>>(PerformancesJson);
        return Performances;
    }

    public static void WritePerformances(Dictionary<string, Performance> performances){
        var PerformancesJson = JsonSerializer.Serialize(performances);
        File.WriteAllText(@"DataSources/performances.json", JsonFormat);
    }
}
