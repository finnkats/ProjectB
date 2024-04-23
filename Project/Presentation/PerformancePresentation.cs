using System;

public static class PerformancePresentation
{
    // Similar to other Presentation file comments (previous ones)
    public static void AddPerformance(){
        Console.Clear();
        Console.WriteLine("Enter a performance you want to add.");
        Console.WriteLine("Performance name: ");
        string? performanceName = Console.ReadLine();
        Console.Clear();
  
        var genres = GenrePresentation.GetGenres();

        Console.WriteLine("Will the performance be currently active?");
        Console.WriteLine("\n1. Yes");
        Console.WriteLine("2. No");

        bool active = (Console.ReadLine() == "1");

        Console.Clear();
        if (PerformanceLogic.AddPerformance(performanceName ?? "", genres, active)){
            Console.WriteLine($"Performance {performanceName} has been added");
        } else {
            Console.WriteLine($"Performance {performanceName} already exists");
        }

        Thread.Sleep(3500);
        Console.Clear();
    }

    // This is the catalogue, this should probably the base for future menu's
    public static string? PerformanceChoice(string question, bool onlyActive=false){
    // Clear the console
    Console.Clear();
    
    // Get the list of performance options based on whether only active performances are requested
    var PerformanceOptions = PerformanceLogic.GetPerformanceOptions(onlyActive);
    
    // Initialize variables for pagination
    int page = 1;
    int pages;
    int offset = 0;
    // Determine the index of the exit option based on whether only active is true or false are requested
    int exitOptionIndex = onlyActive ? 2 : 1;
    
    // Infinite loop to keep the menu running until an option is chosen or the user exits
    while (true){
        // Clear the console
        Console.Clear();
        
        // Calculate the total number of pages based on the number of performance options
        pages = (PerformanceOptions.Count + 4) / 5;
        if (pages <= 1) offset = 0; // If there's only one page, reset the offset
        
        // Get the subset of performance options to display on the current page
        var PerformanceOptionsScope = PerformanceOptions.Skip(0 + (5 * (page - 1))).Take(5).ToList();
        if (PerformanceOptionsScope.Count == 0) Console.WriteLine("No current performance have these genres");
        
        // Display the performance options for the current page
        foreach (var performanceOption in PerformanceOptionsScope){
            Console.WriteLine(performanceOption.Item2);
        }
        Console.WriteLine();

        // Display pagination options if necessary
        if (offset == 2 || PerformanceOptions.Count > 5){
            Console.WriteLine($"Page {page}/{pages}");
            Console.WriteLine($"{PerformanceOptionsScope.Count + 1}: Next page");
            Console.WriteLine($"{PerformanceOptionsScope.Count + 2}: Previous page");
            offset = 2;
        }
        // Display filter option if only active performances are requested
        if (onlyActive){
            Console.WriteLine($"{PerformanceOptionsScope.Count + 1 + offset}: Filter");
        }
        
        // Display exit option
        Console.WriteLine($"{PerformanceOptionsScope.Count + exitOptionIndex + offset}: Exit\n");
        Console.WriteLine(question);

        // Read user input and parse it as integer
        Int32.TryParse(Console.ReadLine(), out int choice);
        try {
            // Handle user choices
            if (onlyActive && choice == PerformanceOptionsScope.Count + 1 + offset){
                // Filter performance options based on user-selected genres
                List<string> genres = GenrePresentation.GetGenres(question: "What genres are you interested in?");
                var filteredPerformanceOptions = PerformanceLogic.FilteredPerformanceOptions(genres);
                PerformanceOptions = filteredPerformanceOptions;
            }else if (choice == PerformanceOptionsScope.Count + 2 + offset){
                // Return null if user chooses to exit
                return null;
            } else{
                // Return the performance ID if a specific performance is chosen
                string performanceId = PerformanceOptionsScope[choice - 1].Item1;
                return performanceId;
            }
        } catch (ArgumentOutOfRangeException) {
            // Handle out of range exceptions
            if (choice == PerformanceOptionsScope.Count() + 1 + offset) return null; // User chooses to exit
            else if (offset == 2){
                // Handle pagination options
                if (choice == PerformanceOptionsScope.Count() + 1){
                    page = (page + 1 > pages) ? 1 : page + 1; // Move to the next page
                } else if (choice == PerformanceOptionsScope.Count() + 2){
                    page = (page - 1 < 1) ? pages : page - 1; // Move to the previous page
                }
            }
        }
    }
}


    // Similar to other Presentation file comments (previous ones)
    public static void EditPerformanceStart(){
        string? performanceId = PerformanceChoice("Choose the performance you want to edit:");
        if (performanceId == null) return;
        EditPerformance(performanceId);
    }

    // Similar to other Presentation file comments (previous ones)
    public static void EditPerformance(string performanceId){
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{App.Performances[performanceId].Name}\"");
            List<string> currentGenres = new();
            foreach (var genreId in App.Performances[performanceId].Genres){
                currentGenres.Add(App.Genres[genreId].Name);
            }
            string seperator = ", ";
            Console.WriteLine($"2: Change genres: [{String.Join(seperator, currentGenres)}]");
            Console.WriteLine($"3: Change active status \"{App.Performances[performanceId].Active}\"");
            Console.WriteLine("4: Exit\n");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1){
                Console.Clear();
                Console.WriteLine("Enter new name:\n");
                if (PerformanceLogic.ChangeName(Console.ReadLine() ?? "", performanceId, App.Performances)) Console.WriteLine("Successfully changed name");
                else Console.WriteLine("Couldn't change name");
                Thread.Sleep(2500);
            }
            else if (choice == 2){
                Console.Clear();
                List<string> RemovedGenreIds = new();
                foreach (var genreId in App.Performances[performanceId].Genres){
                    Console.WriteLine($"Do you want to remove '{App.Genres[genreId].Name}' from '{App.Performances[performanceId].Name}'? (Y/N)");
                    string removeGenre = Console.ReadLine()?.ToUpper() ?? "";
                    if (removeGenre.StartsWith("Y")){
                        currentGenres.Remove(App.Genres[genreId].Name);
                        RemovedGenreIds.Add(genreId);
                    }
                    Console.WriteLine();
                }
                RemovedGenreIds.ForEach(genreId => App.Performances[performanceId].Genres.Remove(genreId));

                Console.Clear();
                List<string> genres = GenrePresentation.GetGenres(performanceId);
                PerformanceLogic.ChangeGenres(genres, performanceId, App.Performances);
                Console.WriteLine("Successfully changed genres");
                Thread.Sleep(2500);
            }
            else if (choice == 3){
                PerformanceLogic.ChangeActive(performanceId, App.Performances);
                Console.WriteLine("Successfully changed active status");
                Thread.Sleep(2500);
            }
            else if (choice == 4){
                return;
            }
        }
    }
}
