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
