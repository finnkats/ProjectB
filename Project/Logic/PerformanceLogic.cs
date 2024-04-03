public static class PerformanceLogic{
    public static bool AddPerformance(string name, List<string> genres, bool active){
        Performance NewPerformance = new(name, genres, active);
        var Performances = PerformanceDataAccess.ReadPerformances();
        foreach (var performance in Performances.Values){
            if (performance.Name.ToLower() == name.ToLower()) return false;
        }
        string AssignedId = AssignId();
        Performances.Add(AssignedId, NewPerformance);
        PerformanceDataAccess.WritePerformances(Performances);
        PlayLogic.AddNewId(AssignedId);
        return true;
    }

    public static string AssignId(){
        var Performances = PerformanceDataAccess.ReadPerformances();
        int newId = Performances.Count();
        return $"ID{newId}";
    }

    public static bool ChangeName(string name, string id, Dictionary<string, Performance> Performances){
        foreach (var performance in Performances.Values){
            if (performance.Name == name) return false;
        }

        Performances[id].Name = name;
        PerformanceDataAccess.WritePerformances(Performances);
        //PlayLogic.ChangeName(id, name);
        return true;
    }

    public static void ChangeGenres(List<string> genres, string id, Dictionary<string, Performance> Performances){
        Performances[id].Genres = genres;
        PerformanceDataAccess.WritePerformances(Performances);
        return;
    }

    public static void ChangeActive(string id, Dictionary<string, Performance> Performances){
        Performances[id].Active = !(Performances[id].Active);
    }
}
