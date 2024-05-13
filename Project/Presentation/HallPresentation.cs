using System.Globalization;

public class HallPresentation : PresentationBase<Hall>{
    public HallPresentation(LogicBase<Hall> logic) : base(logic){}

    public void AddHall(){
        Console.Clear();
        string? hallName = GetNameInput();
        if (hallName is null) return;
        Console.WriteLine();

        int hallSeats;
        while (true){
            hallSeats = -1;
            Console.WriteLine($"Enter seats for hall {hallName}\nQ to exit");
            string seatChoice = Console.ReadLine()?.ToLower() ?? "";
            if (seatChoice == "q"){
                Console.WriteLine("Exitting");
                return;
            }
            Int32.TryParse(seatChoice, out hallSeats);
            if (hallSeats <= 0){
                Console.WriteLine($"{seatChoice} is not a valid amount of seats\n");
                
            } else break;
        }
        Console.WriteLine();

        string locationId = App.locationPresentation.GetItem("In which location is this hall?", "No location yet");
        
        if (!App.hallLogic.AddHall(hallName, hallSeats, locationId)){
            Console.WriteLine("An error occurred while adding hall.");
            Thread.Sleep(3000);
            return;
        }

        string locationAdded = (locationId == "null") ? "null" : $" to location {App.Locations[locationId].Name}";
        Console.WriteLine($"Hall '{hallName}' with {hallSeats} seats has been added" + locationAdded);
        Thread.Sleep(5000);
    }

    public void EditHallStart(){
        string hallId = GetItem("Which hall do you want to edit?", "Exit", InEditMenu: true);

        if (hallId == "add"){
            App.hallPresentation.AddHall();
            return;
        }

        if (hallId == "null") hallId = "";
        while (true){
            int choice = EditObject(hallId);
            if (choice == 0) return;
            else if (choice == 2){
                string oldLocation = (Logic.Dict[hallId].LocationId == "null") ? $"No location": $"{App.Locations[Logic.Dict[hallId].LocationId].Name}";
                string newLocationId = App.locationPresentation.GetItem($"New location for {Logic.Dict[hallId].Name}, " +
                                                            $"currently: {oldLocation}:\n", "Remove hall from location");
                Logic.Dict[hallId].LocationId = newLocationId;
                DataAccess.UpdateItem<Hall>();
                Console.WriteLine($"Successfully changed '{Logic.Dict[hallId].Name}' location from '{oldLocation}' to " +
                                  ((newLocationId == "null") ? $"'No location'": $"'{App.Locations[newLocationId].Name}'"));
                Thread.Sleep(6000);
            }
        }
    }
}
