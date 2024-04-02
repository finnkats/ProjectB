using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class AddPerformance
{
    public static void AddToJson(Performance performance)
    {
        string ID = this.IdAssigner();
        var PerformanceDict = new Dictionary<string, Performance>{ID, performance};

    var JsonFormat = JsonSerializer.Serialize(PerformanceDict);

        File.WriteAllText(@"../../DataSources/Performance.json", JsonFormat);
    }

    // Rewrite code for more clarity. Check for empty files and use FileIsNotNull method.
    public static string IdAssigner()
    {
        var PerformancesJson = File.ReadAllText(@"../../DataSources/Performance.json");
        var performances = JsonSerializer.Deserialize<Dictionary<string, Play>>(PerformancesJson);

        if (performances == null)
        {
            return "ID1";
        }
        else
        {
            int CurrentID = 0;
            foreach (KeyValuePair<string, Play> performance in performances)
            {
                string NumberString = Regex.Match(performance.Key, @"\d+").Value; //haalt het nummer van uit de ID als string
                int ID = Int32.Parse(NumberString);

                if (CurrentID < ID) {
                        CurrentID = ID;
                    }
            }

            CurrentID = CurrentID + 1;
            string ReturnID = $"ID: {CurrentID}";

            return ReturnID;
        }
    }
}
