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

    public static bool HasGenre(string performanceID = null, List<string> genreIDList = null) // 'performance.Value.Genres;' contains a string of GenreID'S
    {                                                                                         // 'performance.Key' is the performanceID
        if (performanceID == null || genreIDList == null)
        {
            return false;
        }

        Performance performance = App.Performances[performanceID];

        foreach (string genreID in performance.Genres)  // Looping through the genres list, which contains GenreID'S
        {
            if (genreIDList.Contains(genreID))  // If the passed Genre ID list contains the genreID; return true
            {
                return true;
            }
        }

        return false;
    }

    private static List<(string, string)> AssignStringGenre(List<(string, Performance)> PerformancesOrdered, bool onlyActive, int index)
    // Made latter part of 'GetPerformanceOptions' a new method as the avoid code repetition
    {   
        List<(string, string)> PerformanceOptions = new();
        foreach (var performance in PerformancesOrdered){
            if (onlyActive && !performance.Item2.Active) continue;
            string performanceString = $"{(index++ % 5) + 1}: {performance.Item2.Name}".PadRight(40);
            if (onlyActive){
                List<string> currentGenres = new();
                foreach (var genreId in App.Performances[performance.Item1].Genres){
                    currentGenres.Add(App.Genres[genreId].Name);
                }
                currentGenres.Sort();
                string separator = ", ";
                performanceString += $"[{String.Join(separator, currentGenres)}]";
            }
            PerformanceOptions.Add((performance.Item1, performanceString));
        }
        return PerformanceOptions;
    }

    public static List<(string, string)> GetPerformanceOptions(bool onlyActive, List<(string, Performance)> filteredPerformances = null){
        int index = 0;
        // id, performance string
        List<(string, Performance)> PerformancesOrdered = new();
        List<(string, Performance)> filteredPerformancesOrdered = new(); // To order 'filteredPerformances' alphabetically

        foreach (KeyValuePair<string, Performance> performance in App.Performances){    // Iterate through performance dictionary
            if (onlyActive && !performance.Value.Active) continue;  // If onlyActive is true and performance.Value.Active (activeness of the performance) is false, skip this loop
            if (onlyActive && filteredPerformances != null && filteredPerformances.Count > 0)
            {
                filteredPerformancesOrdered = filteredPerformances.OrderBy(performance => performance.Item2.Name).ToList();
                return (filteredPerformancesOrdered, onlyActive, index);
            }
            PerformancesOrdered.Add((performance.Key, performance.Value));  // Adds the ID and performance object as a tuple to the PerformancesOrdered list
        }
        PerformancesOrdered = PerformancesOrdered.OrderBy(performance => performance.Item2.Name).ToList();  // Performances get ordered alphabetically by name
        return AssignStringGenre(PerformancesOrdered, onlyActive, index);
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
