public class LocationPresentation : PresentationBase<Location>{
    public LocationPresentation(LogicBase<Location> logic) : base(logic) {}

    public void AddLocation(){
        Console.Clear();
        string? locationName = GetNameInput();
        if (locationName is null) return;
        Console.WriteLine();

        var Halls = App.hallPresentation.GetUnlinkedHalls();
        if (!App.locationLogic.AddLocation(locationName, Halls)){
            Console.WriteLine("An error occured while adding location.");
            Thread.Sleep(2500);
            return;
        }

        string seperator = ", ";
        List<string> currentHalls = new();
        Halls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
        Console.WriteLine($"Location '{locationName}' with halls [{String.Join(seperator, currentHalls)}] has been added");
        Thread.Sleep(5000);
    }

    public void EditLocationStart(){
        string locationId = GetLocation("Which location do you want to edit?", "Cancel");
        if (locationId == "null") locationId = "";
        while (true){
            int choice = EditObject(locationId);
            if (choice == 0) return;
            if (choice == 2){
                Console.Clear();
                List<string> currentHalls = new();
                Logic.Dict[locationId].Halls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
                List<string> RemovedHallIds = new();
                foreach (var hallId in Logic.Dict[locationId].Halls){
                    Console.WriteLine($"Do you want to remove '{App.Halls[hallId].Name}' from '{Logic.Dict[locationId].Name}'? (Y/N)");
                    string removeHall = Console.ReadLine()?.ToUpper() ?? "";
                    if (removeHall.StartsWith("Y")){
                        App.Halls[hallId].LocationId = "null";
                        currentHalls.Remove(App.Halls[hallId].Name);
                        RemovedHallIds.Add(hallId);
                    }
                    Console.WriteLine();
                }
                RemovedHallIds.ForEach(hallId => Logic.Dict[locationId].Halls.Remove(hallId));

                string seperator = ", ";
                Console.WriteLine($"Current Halls: [{String.Join(seperator, currentHalls)}]\n" +
                                  $"Do you want to add halls to '{Logic.Dict[locationId].Name}'? (Y/N)"); 
                                  
                string addHall = Console.ReadLine()?.ToUpper() ?? "";
                if (addHall.StartsWith("Y")){
                    Logic.Dict[locationId].Halls = App.hallPresentation.GetUnlinkedHalls(locationId);
                }

                LocationDataAccess.UpdateLocations();
                Console.WriteLine("Successfully changed halls");
                Thread.Sleep(1500);

            }
        }
    }

    // Similar to GetGenre / GetHall etc.
    public string GetLocation(string question = "In which location is this hall?\n", string exit = "No location yet"){
        List<(string, string)> LocationsOrdered = new();
        foreach (var location in App.Locations){
            LocationsOrdered.Add((location.Key, location.Value.Name));
        }
        LocationsOrdered = LocationsOrdered.OrderBy(location => location.Item2).ToList();

        while(true){
            int choice = -1;
            Console.Clear();
            Console.WriteLine(question);
            int index = 1;
            string locations = "";
            foreach (var location in LocationsOrdered){
                locations += $"{index++}: {location.Item2}\n";
            }
            locations += $"\n{index}: " + exit;
            Console.WriteLine(locations);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                } else {
                    return LocationsOrdered[choice - 1].Item1;
                }
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == LocationsOrdered.Count){
                    return "null";
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }
}
