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
                if (location.Value.Name == Name){
                    Console.Write($"Location with name {Name} already exists");
                    Thread.Sleep(3000);
                    continue;
                }
            }

            var Halls = HallPresentation.GetHalls();

            if (!LocationLogic.AddLocation(Name, Halls)){
                Console.WriteLine("An error occured while adding location. Try again");
                Thread.Sleep(2500);
                continue;
            }

            string seperator = ", ";
            Console.WriteLine($"Location {Name} with halls [{String.Join(seperator, Halls)}] has been added");
            Thread.Sleep(5000);
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

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                } else {
                    return LocationsOrdered[choice].Item1;
                }
            } catch (ArgumentOutOfRangeException){
                if (choice == LocationsOrdered.Count){
                    return "null";
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }
}
