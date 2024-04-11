public static class PerformanceLogic{
    public static bool AddPerformance(string name, List<string> genres, bool active){
        Performance NewPerformance = new(name, genres, active);
        foreach (var performance in App.Performances.Values){
            if (performance.Name.ToLower() == name.ToLower()) return false;
        }
        string AssignedId = AssignId();
        App.Performances.Add(AssignedId, NewPerformance);
        PerformanceDataAccess.UpdatePerformances();
        PlayLogic.AddNewId(AssignedId);
        return true;
    }

    public static string AssignId(){
        int newId = App.Performances.Count();
        return $"ID{newId}";
    }

    public static List<(string, string)> GetPerformanceOptions(bool onlyActive){
        int index = 0;
        // id, performance string
        List<(string, string)> PerformanceOptions = new();
        List<(string, Performance)> PerformancesOrdered = new();

        foreach (KeyValuePair<string, Performance> performance in App.Performances){
            if (onlyActive && !performance.Value.Active) continue;
            PerformancesOrdered.Add((performance.Key, performance.Value));
        }
        PerformancesOrdered = PerformancesOrdered.OrderBy(performance => performance.Item2.Name).ToList();

        foreach (var performance in PerformancesOrdered){
            if (onlyActive && !performance.Item2.Active) continue;
            string performanceString = $"{(index++ % 5) + 1}: {performance.Item2.Name}".PadRight(40);
            if (onlyActive){
                string genres = String.Join(", ", performance.Item2.Genres);
                performanceString += $"[{genres}]";
            }
            PerformanceOptions.Add((performance.Item1, performanceString));
        }
        
        return PerformanceOptions;
    }

    public static void PerformanceCatalogue(){
        Console.Clear();
        string? performanceId = PerformancePresentation.PerformanceChoice("Pick a performance for which you want to buy a ticket:", true);
        if (performanceId == null) return;
        PlayLogic.Choose(performanceId);
    }

    public static bool ChangeName(string name, string id, Dictionary<string, Performance> Performances){
        foreach (var performance in Performances.Values){
            if (performance.Name == name) return false;
        }

        Performances[id].Name = name;
        PerformanceDataAccess.UpdatePerformances();
        return true;
    }

    public static void ChangeGenres(List<string> genres, string id, Dictionary<string, Performance> Performances){
        Performances[id].Genres = genres;
        PerformanceDataAccess.UpdatePerformances();
        return;
    }

    public static void ChangeActive(string id, Dictionary<string, Performance> Performances){
        Performances[id].Active = !Performances[id].Active;
    }
}
