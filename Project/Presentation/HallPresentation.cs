using System.Globalization;

public static class HallPresentation {
    // Similar to other Presentation file comments (previous ones)
    public static void AddHall(){
        while(true){
            Console.Clear();
            Console.WriteLine("Enter hall name:");
            string Name = Console.ReadLine() ?? "";
            if (Name == ""){
                Console.Write("Invalid name");
                Thread.Sleep(2500);
                continue;
            }
            foreach (var hall in App.Halls){
                if (hall.Value.Name.ToLower() == Name.ToLower()){
                    Console.Write($"Hall with name {Name} already exists");
                    Thread.Sleep(3000);
                    return;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Enter amount of seats in hall:");
            if (!Int32.TryParse(Console.ReadLine(), out int Seats) || Seats <= 0){
                Console.WriteLine("Invalid amount of seats");
                Thread.Sleep(2500);
                continue;
            }
            
            string locationId = LocationPresentation.GetLocation();

            if (!HallLogic.AddHall(Name, Seats, locationId)){
                Console.WriteLine("Error occurred while adding hall. Try again");
                Thread.Sleep(2500);
                continue;
            }
    
            Console.Clear();
            string locationAdded = (locationId == "null") ? "null" : $" to location {App.Locations[locationId].Name}";
            Console.WriteLine($"Hall '{Name}' with {Seats} seats has been added" + locationAdded);
            Thread.Sleep(5000);
            break;
        }
    }

    // GetHall is similar to GetGenre
    public static string GetHall(string locationId = ""){
        List<(string, string)> HallsOrdered = new();
        if (locationId != ""){
            if (!App.Locations.ContainsKey(locationId)) return "null";
            App.Locations[locationId].Halls.ForEach(hallId => HallsOrdered.Add((hallId, App.Halls[hallId].Name)));
        } else {
            foreach (var hall in App.Halls){
                HallsOrdered.Add((hall.Key, hall.Value.Name));
            }
        }
        HallsOrdered = HallsOrdered.OrderBy(hall => hall.Item2).ToList();

        int index = 1;
        string halls = "";
        while (true){
            int choice = -1;
            Console.Clear();
            Console.WriteLine("Choose a hall:");
            foreach (var hall in HallsOrdered){
                string hallLocationId = App.Halls[hall.Item1].LocationId;
                halls += $"{index++}: {hall.Item2}";
                halls += (hallLocationId == "null") ? "\tNo location\n" : $"\t({App.Locations[hallLocationId].Name})\n";
            }
            halls += $"\n{index}: Cancel";
            Console.WriteLine(halls);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    continue;
                }
                return HallsOrdered[choice - 1].Item1;
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == HallsOrdered.Count) return "null";
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
            }
        }
    }

    // GetUnlinkedHalls is similar to GetGenres
    public static List<string> GetUnlinkedHalls(string LocationId = ""){
        if (LocationId != "" && !App.Locations.ContainsKey(LocationId)) LocationId = "";
        List<string> LocationHalls = (LocationId == "") ? new() : App.Locations[LocationId].Halls;

        List<(string, string)> HallsOrdered = new();
        foreach (var hall in App.Halls){
            if (hall.Value.LocationId != "null") continue;
            HallsOrdered.Add((hall.Key, hall.Value.Name));
        }
        HallsOrdered = HallsOrdered.OrderBy(hall => hall.Item2).ToList();
        string seperator = ", ";

        while(true){
            LocationHalls.Sort();
            int choice = -1;
            Console.Clear();
            List<string> currentHalls = new();
            LocationHalls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
            Console.WriteLine($"Current halls: [{String.Join(seperator, currentHalls)}]\n");
            Console.WriteLine("Which halls belong to this location?");
            string halls = "";
            int index = 1;
            foreach (var hall in HallsOrdered){
                if (LocationHalls.Contains(hall.Item1)) continue;
                halls += $"{index++}: {hall.Item2}\n";
            }
            halls += $"\n{index}: Confirm";
            Console.WriteLine(halls);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    Thread.Sleep(2500);
                } else {
                    LocationHalls.Add(HallsOrdered[choice - 1].Item1);
                    HallsOrdered.RemoveAt(choice - 1);
                }
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == HallsOrdered.Count){
                    return LocationHalls;
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }

    // Similar to other Presentation file comments (previous ones)
    public static void EditHallStart(){
        string hallId = GetHall();
        if (hallId == "null") return;
        EditHall(hallId);
    }

    // Similar to other Presentation file comments (previous ones)
    public static void EditHall(string hallId){
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{App.Halls[hallId].Name}\"");
            Console.WriteLine($"2: Change amount of seats \"{App.Halls[hallId].Seats}\"");
            Console.WriteLine($"3: Change location this hall is linked to \"" + 
                                ((App.Halls[hallId].LocationId == "null") ? "No location" : App.Locations[App.Halls[hallId].LocationId].Name)
                                + "\"");
            Console.WriteLine("4: Exit\n");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1"){
                Console.Clear();
                Console.WriteLine($"Enter new name for '{App.Halls[hallId].Name}':");
                string oldName = App.Halls[hallId].Name;
                string newName = Console.ReadLine() ?? "";
                if (!HallLogic.ChangeName(hallId, newName)){
                    Console.WriteLine($"Couldn't change name, either invalid or '{newName}' already exists");
                } else {
                    Console.WriteLine($"Successfully changed '{oldName}' to '{newName}'");
                }
                Thread.Sleep(4000);

            } else if (choice == "2"){
                Console.Clear();
                Console.WriteLine($"Enter new amount of seats for '{App.Halls[hallId].Name}', currently: {App.Halls[hallId].Seats}:");
                int oldSeats = App.Halls[hallId].Seats;
                if (!Int32.TryParse(Console.ReadLine(), out int newSeats)){
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(2000);
                    continue;
                }
                if (!HallLogic.ChangeSeats(hallId, newSeats)){
                    Console.WriteLine($"Couldn't change seats, value too low");
                } else {
                    Console.WriteLine($"Successfully changed '{oldSeats}' to '{newSeats}'");
                }
                Thread.Sleep(4000);

            } else if (choice == "3"){
                string oldLocation = (App.Halls[hallId].LocationId == "null") ? $"No location": $"{App.Locations[App.Halls[hallId].LocationId].Name}";
                string newLocationId = LocationPresentation.GetLocation($"New location for {App.Halls[hallId].Name}, " +
                                                            $"currently: {oldLocation}:\n", "Remove hall from location");
                App.Halls[hallId].LocationId = newLocationId;
                HallDataAccess.UpdateHalls();
                Console.WriteLine($"Successfully changed '{App.Halls[hallId].Name}' location from '{oldLocation}' to " +
                                  ((newLocationId == "null") ? $"'No location'": $"'{App.Locations[newLocationId].Name}'"));
                Thread.Sleep(6000);

            } else if (choice == "4"){
                return;
            } else {
                Console.WriteLine("Invalid input");
                Thread.Sleep(2000);
            }
        }
    }
}
