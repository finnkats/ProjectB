public class PerformanceLogic : LogicBase<Performance>{
    // Again, same story as other Logic
    public bool AddPerformance(string name, List<string> genres, bool active){
        if (name == "") return false;
        foreach (var performance in App.Performances.Values){
            if (performance.Name.ToLower() == name.ToLower()) return false;
        }

        Performance NewPerformance = new(name, genres, active);
        string AssignedId = GetID();
        App.Performances.Add(AssignedId, NewPerformance);
        PerformanceDataAccess.UpdatePerformances();
        PlayLogic.AddNewId(AssignedId);
        return true;
    }

    // Similar to other logic
    public void ChangeGenres(List<string> genres, string id, Dictionary<string, Performance> Performances){
        Performances[id].Genres = genres;
        PerformanceDataAccess.UpdatePerformances();
        return;
    }

    // Changes active value
    public void ChangeActive(string id, Dictionary<string, Performance> Performances){
        Performances[id].Active = !Performances[id].Active;
    }

    // Checks if a performance contains a genre from the given list of genres
    public bool HasGenre(string? performanceID = null, List<string>? genreIDList = null) // 'performance.Value.Genres;' contains a string of GenreID'S
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

    // Returns a list of performanceId, string made for printing
    // containing the genres from the list
    public List<(string, string)> FilteredPerformanceOptions(List<string> genreIDList)
    {   
        var PerformanceOptions = GetPerformanceOptions(true);
        List<(string, string)> FilteredPerformanceOptionsList = new();

        if (genreIDList.Count == 0)
        {
            return PerformanceOptions;
        }

        int performanceIndex = 1;
        foreach (var performance in PerformanceOptions)
        {   
            if (HasGenre(performance.Item1, genreIDList))
            {
                // overwrites index of the option that will be printed for the menu
                string performanceOptionString = $"{performanceIndex++}: {performance.Item2.Split(':')[1]}";
                FilteredPerformanceOptionsList.Add((performance.Item1, performanceOptionString));
            }
            
        }

        return FilteredPerformanceOptionsList;
    }

    // Returns a list of performanceId, string made for printing,
    // if onlyActive is true, it only contains Active performances
    public List<(string, string)> GetPerformanceOptions(bool onlyActive){
        int index = 0;
        // list of id, performance string
        List<(string, string)> PerformanceOptions = new();
        // list of id, performance
        List<(string, Performance)> PerformancesOrdered = new();

        // Adds the performances to PerformancesOrdered
        foreach (KeyValuePair<string, Performance> performance in App.Performances){
            if (onlyActive && !performance.Value.Active) continue;
            PerformancesOrdered.Add((performance.Key, performance.Value));
        }
        // Sorts the performances by alphabet 
        PerformancesOrdered = PerformancesOrdered.OrderBy(performance => performance.Item2.Name).ToList();

        // Goes over the performances and then adds the (id, string (made for printing)) to PerformanceOptions
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
        
        // Returns the string, which is basically a menu
        return PerformanceOptions;
    }


    public void PerformanceCatalogue(){
        Console.Clear();
        string? performanceId = App.performancePresentation.PerformanceChoice("Pick a performance for which you want to buy a ticket:", true);
        if (performanceId == null) return;
        PlayLogic.Choose(performanceId);
    }
}
