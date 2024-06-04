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
            Console.Write("Enter the runtime in minutes:\n> ");
            string runtimeInput = Console.ReadLine() ?? "";
            bool runtimeBool = int.TryParse(runtimeInput, out runtime);
            if(!runtimeBool || runtime <= 0){Console.WriteLine("Invalid input. Please enter a valid number representing minutes.");}
            else if(runtimeBool){
                Console.Write($"Are you sure that the runtime will be in {runtime} minutes?\nPlease confirm by entering \'y\' (Yes) or \'n\' (No)\n> ");
                string confirmRuntime = Console.ReadLine() ?? "";
                if(confirmRuntime.ToLower() == "y"){break;}
                else if(confirmRuntime.ToLower() == "n"){continue;}
            }
        }
  
        var genres = App.genrePresentation.GetItemList();
        Console.WriteLine();

        bool active;
        while(true){
            Console.Write("Will the performance be currently active? \'y\' (Yes) or \'n\' (No)\n> ");
            string activeInput = Console.ReadLine()?.ToLower() ?? "";
            if (activeInput != "y" && activeInput != "n"){
                Console.WriteLine("Invalid input");
                continue;
            }
            active = activeInput == "y";
            break;
        }

        Console.Clear();
        if (!App.performanceLogic.AddPerformance(performanceName, runtime, genres, active)){
            Console.WriteLine("An error occured while adding performance.");
        }

        string seperator = ", ";
        List<string> currentGenres = new();
        genres.ForEach(genreId => currentGenres.Add(App.Genres[genreId].Name));
        Console.WriteLine($"Performance {performanceName} (" + (active ? "active" : "inactive") + $"), {runtime} minutes " +
                          $"with genres [{String.Join(seperator, currentGenres)}] has been added");
        Thread.Sleep(5000);
    }


    public void EditPerformanceStart(){
        string performanceId = PerformanceChoice("Choose the performance you want to edit (or performance you want to use for a new play): \n\n> ", false, true) ?? "";

        // Add a performance option was chosen
        if (performanceId == "add") {
            App.performancePresentation.AddPerformance();
            return;
        }

        while (true){
            int choice = EditObject(performanceId);
            if (choice == 0) return;
            if (choice == 2){
                Console.Clear();
                List<string> RemovedGenreIds = new();
                string sep = ", ";
                bool printBreadCrumb = true;
                var GenresCopy = Logic.Dict[performanceId].Genres.ToList();
                foreach (var genreId in Logic.Dict[performanceId].Genres){
                    if (printBreadCrumb)
                    {
                        Console.WriteLine($"Front Page -> Home Page -> Modify Performances -> {Logic.Dict[performanceId].Name} -> Change Genre\n");
                    }
                    printBreadCrumb = false;
                    Console.WriteLine($"Current Genres: {String.Join(sep, GenresCopy.Select(genre => App.Genres[genre].Name))}");
                    Console.Write($"Do you want '{Logic.Dict[performanceId].Name}' to keep the Genre '{App.Genres[genreId].Name}'? (Y/N) \n> ");
                    string keepGenre = Console.ReadLine()?.ToUpper() ?? "";
                    if (!keepGenre.StartsWith("Y")){
                        GenresCopy.Remove(genreId);
                        Console.WriteLine($"Removed {App.Genres[genreId].Name}");
                    }
                    Console.WriteLine();
                }
                Logic.Dict[performanceId].Genres = GenresCopy;

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
            else if (choice == 4){
                PlayPresentation.AddPlayDetails(performanceId);
            }
        }
    }

    // This is the catalogue, this should probably the base for future menu's
    public string? PerformanceChoice(string question, bool onlyActive=false, bool admin=false){
        // Clear the console
        Console.Clear();
        
        // Get the list of performance options based on whether only active performances are requested
        var PerformanceOptions = App.performanceLogic.GetPerformanceOptions(onlyActive);
        
        // Initialize variables for pagination
        int page = 1;
        int pages;
        int offset = 0;
        
        // Changed for now, because now an extra option is always added
        // // Determine the index of the exit option based on whether only active is true or false are requested
        //int exitOptionIndex = onlyActive ? 2 : 1;
        
        // Infinite loop to keep the menu running until an option is chosen or the user exits
        while (true){
            int printIndex = 1;
            // Clear the console
            Console.Clear();
            if (!admin)
            {
                Console.WriteLine("Front Page -> Home Page -> View Performances\n");
            }
            else
            {
                Console.WriteLine("Front Page -> Home Page -> Modify Performances\n");
            }
            
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

            // If onlyActive is true, this menu is in the "view performances" option, if its false, its in the "edit performances" option
            if (onlyActive){
                Console.WriteLine($"{PerformanceOptionsScope.Count + 1 + offset}: Filter Catalogue by Genres");
            } else {
                Console.WriteLine($"{PerformanceOptionsScope.Count + 1 + offset}: Add New Performance");
            }
            
            // Display exit option
            Console.WriteLine($"{PerformanceOptionsScope.Count + 2 + offset}: Exit");
            Console.Write(question);

            // Read user input and parse it as integer
            Int32.TryParse(Console.ReadLine(), out int choice);
            try {
                // Handle user choices
                if (onlyActive && choice == PerformanceOptionsScope.Count + 1 + offset){
                    // Filter performance options based on user-selected genres
                    List<string> genres = App.genrePresentation.GetItemList(filter: true);
                    var filteredPerformanceOptions = App.performanceLogic.FilteredPerformanceOptions(genres);
                    PerformanceOptions = filteredPerformanceOptions;
                    page = 1;
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
                if (choice == PerformanceOptionsScope.Count() + 1 + offset){ // The option filter or add was chosen
                    if (onlyActive) return null;
                    else return "add";
                } // User chooses to exit
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
