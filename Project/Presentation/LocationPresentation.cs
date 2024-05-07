public static class LocationPresentation {
    public static void AddLocation(){
        while (true){
            Console.Clear();
            Console.WriteLine("Enter name for location:");
            string Name = Console.ReadLine() ?? "";
            if (Name == ""){
                Console.Write("Invalid name");
                Thread.Sleep(2500);
                continue;
            }
            foreach (var location in App.Locations){
                if (location.Value.Name.ToLower() == Name.ToLower()){
                    Console.Write($"Location with name {Name} already exists");
                    Thread.Sleep(3000);
                    return;
                }
            }

            var Halls = HallPresentation.GetUnlinkedHalls();

            if (!LocationLogic.AddLocation(Name, Halls)){
                Console.WriteLine("An error occured while adding location. Try again");
                Thread.Sleep(2500);
                continue;
            }

            Console.Clear();
            string seperator = ", ";
            List<string> currentHalls = new();
            Halls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
            Console.WriteLine($"Location '{Name}' with halls [{String.Join(seperator, currentHalls)}] has been added");
            Thread.Sleep(5000);
            break;
        }
    }

    public static string GetLocation(string question = "In which location is this hall?\n", string exit = "No location yet"){
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

    public static void EditLocationStart(){
        string locationId = GetLocation("Which location do you want to edit?", "Cancel");
        if (locationId == "null") return;
        EditLocation(locationId);
    }

    public static void EditLocation(string locationId){
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{App.Locations[locationId].Name}\"");
            List<string> currentHalls = new();
            App.Locations[locationId].Halls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
            string seperator = ", ";
            Console.WriteLine($"2: Change halls [{String.Join(seperator, currentHalls)}]");
            Console.WriteLine("3: Exit\n");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1"){
                Console.Clear();
                Console.WriteLine($"Enter new name for '{App.Locations[locationId].Name}':");
                string oldName = App.Locations[locationId].Name;
                string newName = Console.ReadLine() ?? "";
                if (!LocationLogic.ChangeName(locationId, newName)){
                    Console.WriteLine($"Couldn't change name, either invalid or '{newName}' already exists");
                } else {
                    Console.WriteLine($"Successfully changed '{oldName}' to '{newName}'");
                }
                Thread.Sleep(4000);

            } else if (choice == "2"){
                Console.Clear();
                List<string> RemovedHallIds = new();
                foreach (var hallId in App.Locations[locationId].Halls){
                    Console.WriteLine($"Do you want to remove '{App.Halls[hallId].Name}' from '{App.Locations[locationId].Name}'? (Y/N)");
                    string removeHall = Console.ReadLine()?.ToUpper() ?? "";
                    if (removeHall.StartsWith("Y")){
                        App.Halls[hallId].LocationId = "null";
                        currentHalls.Remove(App.Halls[hallId].Name);
                        RemovedHallIds.Add(hallId);
                    }
                    Console.WriteLine();
                }
                RemovedHallIds.ForEach(hallId => App.Locations[locationId].Halls.Remove(hallId));


                Console.WriteLine($"Current Halls: [{String.Join(seperator, currentHalls)}]\n" +
                                  $"Do you want to add halls to '{App.Locations[locationId].Name}'? (Y/N)"); 
                                  
                string addHall = Console.ReadLine()?.ToUpper() ?? "";
                if (addHall.StartsWith("Y")){
                    App.Locations[locationId].Halls = HallPresentation.GetUnlinkedHalls(locationId);
                }

                LocationDataAccess.UpdateLocations();
                Console.WriteLine("Successfully changed halls");
                Thread.Sleep(1500);

            } else if (choice == "3"){
                return;
            } else {
                Console.WriteLine("Invalid choice");
                Thread.Sleep(3000);
            }
        }
    }
}
