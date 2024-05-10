using System;

public class PerformancePresentation : PresentationBase<Performance>{
    public PerformancePresentation(LogicBase<Performance> Logic) : base(Logic) {}

    public void AddPerformance(){
        Console.Clear();
        string? performanceName = GetNameInput();
        if (performanceName is null) return;
        Console.WriteLine();

        int runtime;
        while(true){
            Console.Write("Enter the runtime in minutes:\n>");
            string runtimeInput = Console.ReadLine() ?? "";
            bool runtimeBool = int.TryParse(runtimeInput, out runtime);
            if(!runtimeBool){Console.WriteLine("Invalid input. Please enter a valid number representing minutes.");}
            else if(runtimeBool){
                Console.WriteLine($"Are you sure that the runtime will be in {runtime} minutes?\n>");
                string confirmRuntime = Console.ReadLine() ?? "";
                if(confirmRuntime.ToLower() == "y"){break;}
                else if(confirmRuntime.ToLower() == "n"){continue;}
                else{Console.WriteLine("Please confirm by entering \'y\' (Yes) or \'n\' (No)");}
            }
        }
  
        var genres = App.genrePresentation.GetItemList();
        Console.WriteLine();

        Console.WriteLine("Will the performance be currently active?\n1. Yes\n2. Exit\nAnything else. No");
        string activeInput = Console.ReadLine() ?? "";
        if (activeInput == "2") return;
        bool active = activeInput == "1";

        Console.Clear();
        if (!App.performanceLogic.AddPerformance(performanceName, runtime, genres, active)){
            Console.WriteLine("An error occured while adding performance.");
        }

        string seperator = ", ";
        List<string> currentGenres = new();
        genres.ForEach(genreId => currentGenres.Add(App.Genres[genreId].Name));
        Console.WriteLine($"Performance {performanceName} (" + (active ? "active" : "inactive") + $"), {runtime} minutes" +
                          $"with genres [{String.Join(seperator, currentGenres)}] has been added");
        Thread.Sleep(5000);
    }


    public void EditPerformanceStart(){
        string performanceId = PerformanceChoice("Choose the performance you want to edit:") ?? "";
        while (true){
            int choice = EditObject(performanceId);
            if (choice == 0) return;
            if (choice == 2){
                Console.Clear();
                List<string> currentGenres = new();
                foreach (var genreId in Logic.Dict[performanceId].Genres){
                    currentGenres.Add(App.Genres[genreId].Name);
                }
                List<string> RemovedGenreIds = new();
                foreach (var genreId in Logic.Dict[performanceId].Genres){
                    Console.WriteLine($"Do you want to remove '{App.Genres[genreId].Name}' from '{Logic.Dict[performanceId].Name}'? (Y/N)");
                    string removeGenre = Console.ReadLine()?.ToUpper() ?? "";
                    if (removeGenre.StartsWith("Y")){
                        currentGenres.Remove(App.Genres[genreId].Name);
                        RemovedGenreIds.Add(genreId);
                    }
                    Console.WriteLine();
                }
                RemovedGenreIds.ForEach(genreId => Logic.Dict[performanceId].Genres.Remove(genreId));

                Console.Clear();
                List<string> genres = App.genrePresentation.GetItemList(performanceId);
                App.performanceLogic.ChangeGenres(genres, performanceId);
                Console.WriteLine("Successfully changed genres");
                Thread.Sleep(2500);
            }
            else if (choice == 3){
                App.performanceLogic.ChangeActive(performanceId);
                Console.WriteLine("Successfully changed active status");
                Thread.Sleep(2500);
            }
        }
    }

    // This is the catalogue, this should probably the base for future menu's
    public string? PerformanceChoice(string question, bool onlyActive=false){
        // Clear the console
        Console.Clear();
        
        // Get the list of performance options based on whether only active performances are requested
        var PerformanceOptions = App.performanceLogic.GetPerformanceOptions(onlyActive);
        
        // Initialize variables for pagination
        int page = 1;
        int pages;
        int offset = 0;
        // Determine the index of the exit option based on whether only active is true or false are requested
        int exitOptionIndex = onlyActive ? 2 : 1;
        
        // Infinite loop to keep the menu running until an option is chosen or the user exits
        while (true){
            int printIndex = 1;
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
                Console.WriteLine($"{printIndex++}: " + performanceOption.Item2);
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
                    List<string> genres = App.genrePresentation.GetItemList();
                    var filteredPerformanceOptions = App.performanceLogic.FilteredPerformanceOptions(genres);
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
}
