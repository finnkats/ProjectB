public class LocationPresentation : PresentationBase<Location>{
    public LocationPresentation(LogicBase<Location> logic) : base(logic) {}

    public void AddLocation(){
        Console.Clear();
        string? locationName = GetNameInput();
        if (locationName is null) return;
        Console.WriteLine();

        var Halls = App.hallPresentation.GetItemList();
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
                    Logic.Dict[locationId].Halls = App.hallPresentation.GetItemList(locationId);
                }
                
                DataAccess.UpdateItem<Location>();
                Console.WriteLine("Successfully changed halls");
                Thread.Sleep(1500);
            }
        }
    }
}
