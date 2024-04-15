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
                    continue;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Enter amount of seats in hall:");
            if (!Int32.TryParse(Console.ReadLine(), out int Seats)){
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

            string locationAdded = (locationId == null) ? "" : $" to location {App.Locations[locationId].Name}";
            Console.WriteLine($"Hall {Name} with {Seats} seats has been added" + locationAdded);
            Thread.Sleep(2500);
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

        while(true){
            LocationHalls.Sort();
            int choice = -1;
            Console.Clear();
            Console.WriteLine($"Current halls: [{String.Join(',', LocationHalls)}]");
            Console.WriteLine("Which halls belong to this location?\n");
            string halls = "";
            int index = 1;
            foreach (var hall in HallsOrdered){
                if (LocationHalls.Contains(hall.Item1)) continue;
                halls += $"{index++}: {hall.Item2}\n";
            }
            halls += $"\n{index}: Confirm";

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                } else {
                    LocationHalls.Add(HallsOrdered[choice].Item1);
                    HallsOrdered.RemoveAt(choice);
                }
            } catch (ArgumentOutOfRangeException){
                if (choice == HallsOrdered.Count){
                    Console.WriteLine($"Chosen halls [{String.Join(',', LocationHalls)}]");
                    return LocationHalls;
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }
}
