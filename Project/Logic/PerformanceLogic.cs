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

    public static List<(string, string)>? FilteredPerformanceOptions(List<string> genreIDList)
    {   
        var PerformanceOptions = GetPerformanceOptions(true);
        List<(string, string)> FilteredPerformanceOptionsList = new();

        if (genreIDList.Count == 0)
        {
            return PerformanceOptions;
        }

        foreach (var performance in PerformanceOptions)
        {   
            if (HasGenre(performance.Item1, genreIDList))
            {
                FilteredPerformanceOptionsList.Add(performance);
            }
            
        }

        if (FilteredPerformanceOptionsList.Count == 0)
        {
            Console.WriteLine("No current performance have these genres");
        }

        return FilteredPerformanceOptionsList;
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
                List<string> currentGenres = new();
                foreach (var genreId in App.Performances[performance.Item1].Genres){
                    currentGenres.Add(App.Genres[genreId].Name);
                }
                currentGenres.Sort();
                string seperator = ", ";
                performanceString += $"[{String.Join(seperator, currentGenres)}]";
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
