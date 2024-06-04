public class LocationPresentation : PresentationBase<Location>{
    public LocationPresentation(LogicBase<Location> logic) : base(logic) {}

    public void AddLocation(){
        Console.Clear();
        string? locationName = GetNameInput();
        if (locationName is null) return;
        Console.WriteLine();

        var Halls = App.hallPresentation.GetItemList(extraInfo: "\nNote:\nIf the halls for this location have not been added to the program yet;\n" +
                                                     "Simply choose 'confirm', and the hall can be linked when either adding a new hall or in the edit menu\n");
        if (!App.locationLogic.AddLocation(locationName, Halls)){
            Console.WriteLine("An error occured while adding location.");
            Thread.Sleep(2500);
            return;
        }

        string seperator = ", ";
        List<string> currentHalls = new();
        Halls.ForEach(hallId => currentHalls.Add(App.Halls[hallId].Name));
        Console.WriteLine($"Location '{locationName}' with halls [{String.Join(seperator, currentHalls)}] has been added");
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
    }

    public void EditLocationStart(){
        string locationId = GetItem("Which location do you want to edit?", "Exit", InEditMenu: true);

        if (locationId == "add"){
            App.locationPresentation.AddLocation();
            return;
        }

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
                    Console.Write($"Do you want to remove '{App.Halls[hallId].Name}' from '{Logic.Dict[locationId].Name}'? (Y/N)\n\n> ");
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
                                  $"Do you want to add halls to '{Logic.Dict[locationId]}'? (Y/N)"); 
                                  
                string addHall = Console.ReadLine()?.ToUpper() ?? "";
                if (addHall.StartsWith("Y")){
                    Logic.Dict[locationId].Halls = App.hallPresentation.GetItemList(locationId);
                }
                
                DataAccess.UpdateItem<Location>();
                string hallsString = Logic.Dict[locationId].Halls.Count > 0 
                                 ? string.Join(", ", Logic.Dict[locationId].Halls.Select(hallId => App.Halls[hallId].Name)) 
                                 : "null";
                LocationLogic.logger.LogAction("Location halls updated", $"{Logic.Dict[locationId].Name}, Halls: {hallsString}");

                Console.WriteLine("Successfully changed halls");
                Thread.Sleep(1500);
            }
        }
    }
}
