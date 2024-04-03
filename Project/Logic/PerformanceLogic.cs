public static class PerformanceLogic{
    public static bool AddPerformance(string name, List<string> genres, bool active){
        Performance NewPerformance = new(name, genres, active);
        var Performances = PerformanceDataAccess.ReadPerformances();
        foreach (var performance in Performances.Values){
            if (performance.Name.ToLower() == name.ToLower()) return false;
        }
        Performances.Add(AssignId(), NewPerformance);
        PerformanceDataAccess.WritePerformances(Performances);
        return true;
    }

    public static string AssignId(){
        var Performances = PerformanceDataAccess.ReadPerformances();
        int newId = Performances.Count();
        return $"ID{newId}";
    }
}
