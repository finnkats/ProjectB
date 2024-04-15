public static class HallPresentation {
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

    public static string GetHall(string locationId){
        if (!App.Locations.ContainsKey(locationId)) return "null";
        List<string> HallsOrdered = App.Locations[locationId].Halls;
        HallsOrdered = HallsOrdered.OrderBy(hall => hall).ToList();
        int choice = -1;
        int index = 1;
        string halls = "";
        while (true){
            Console.Clear();
            Console.WriteLine("Choose a hall:");
            foreach (var hall in HallsOrdered){
                halls += $"{index++}: {hall}\n";
            }
            halls += $"{index}: Cancel";
            Console.WriteLine(halls);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    continue;
                }
                return HallsOrdered[choice - 1];
            } catch (ArgumentOutOfRangeException){
                if (choice == HallsOrdered.Count) return "null";
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
            }
        }
    }

    public static List<string> GetHalls(string LocationId = ""){
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
}
