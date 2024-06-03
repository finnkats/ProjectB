using System.Text.Json;
using System.Globalization;

public static class PlayPresentation
{
    // returns the chosen date
    public static void DisplayViewings(List<Play> playOptions, string performanceId)
    {
        Console.Clear();
        Console.WriteLine($"Front Page -> Home Page -> View Performances -> {App.Performances[performanceId].Name}\n");
        Console.WriteLine($"Plays for {App.Performances[performanceId].Name}\n----------------------------------------------------------------------");
        Console.WriteLine($"|# |Location               |Date        |Time          |Hall         |");
        Console.WriteLine("----------------------------------------------------------------------");

        // Sort the playOptions by location
        playOptions.Sort((a, b) => string.Compare(App.Locations[a.Location].Name, App.Locations[b.Location].Name));

        for (int i = 0; i < playOptions.Count; i++)
        {
            var viewing = playOptions[i];
            string locationName = App.Locations[viewing.Location].Name;
            string hallName = App.Halls[viewing.Hall].Name;
            
            // Truncate strings if they are too long
            if (locationName.Length > 29)
                locationName = $"{locationName.Substring(0, 26)}...";
            if (hallName.Length > 14)
                hallName = $"{hallName.Substring(0, 10)}...";

            Console.WriteLine($"|{i + 1,-2}|{locationName,-23}|{viewing.Date,-12}|{viewing.StartTime} - {viewing.EndTime} |{hallName,-13}|");
        }
        Console.WriteLine("----------------------------------------------------------------------");

        int chosenIndex = -1;
        while (chosenIndex == -1){

            Console.Write("Select a performance by entering its index or '0' to cancel\n\n> ");
            
            string? choice = Console.ReadLine();
            if (int.TryParse(choice, out int index)){
                if (index == 0){
                    Console.WriteLine("Cancelled.");
                    Thread.Sleep(1500);
                    return;
                }else if (index > 0 && index <= playOptions.Count){
                    // Get the chosen viewing
                    var chosenViewing = playOptions[index - 1];

                    // Here you'd create the ticket with the chosen details
                    HashSet<int> seats = LayoutPresentation.ChooseSeats(chosenViewing);
                    if (seats.Count == 0){
                        Console.WriteLine("Canceling ticket purchase");
                        return;
                    }
                    MainTicketSystem.CreateBookTicket(performanceId, chosenViewing.Date, chosenViewing.StartTime, chosenViewing.Hall, seats);

                    chosenIndex = index - 1;
                }else{
                    Console.WriteLine("Invalid index.");
                }
            }else{
                Console.WriteLine("Invalid input.");
            }
        }
    }

    // Collects the data needed to add a play
    public static void AddPlayDetails(string performanceId){
        if (performanceId == null) return;

        string location = App.locationPresentation.GetItem("Choose a location:", "Cancel");
        if (location == "null") return;

        string hall = App.hallPresentation.GetItem("Choose a hall:", "Cancel", location);
        if (hall == "null") {
            Console.WriteLine("Cancelling adding of play");
            return;
        }

        // to make hall available
        
        string date;
        while (true){
            Console.Clear();
            Console.WriteLine($"Front Page -> Admin Features -> Edit Performances -> Choosing a location -> Choosing a hall -> Choosing a date\n");
            Console.WriteLine($"{App.Performances[performanceId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name}\n");
            Console.Write("What date? [DD/MM/YYYY]? (can't be today or in the past) \n\n> ");
            string givenDate = Console.ReadLine() ?? "";
            if (!PlayLogic.ValidDate(givenDate)) continue;
            date = givenDate;
            break;
        }
        
        string startTime;
        while (true){
            Console.Clear();
            Console.WriteLine($"Front Page -> Admin Features -> Edit Performances -> Choosing a location -> Choosing a hall -> Choosing a date -> Choosing a time\n");
            Console.WriteLine($"{App.Performances[performanceId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name} | {date}\n");
            Console.Write("What time? [HH:MM] \n\n> ");
            startTime = Console.ReadLine() ?? "99:99";
            if (!PlayLogic.ValidTime(startTime)) continue;
            break;
        }

        if (!PlayLogic.IsHallAvailable(location, DateTime.Parse(date, new CultureInfo("nl-NL")), startTime, hall)){
            Console.WriteLine($"{App.Halls[hall].Name} is not available at the selected {date} : {startTime}. Please choose different details.");
            Thread.Sleep(5000);
            return; // Exit the method if hall is not available
        }
        Console.Clear();
        Console.WriteLine($"Front Page -> Admin Features -> Edit Performances -> Choosing a location -> Choosing a hall -> Choosing a date -> Choosing a time -> Adding a play\n");
        Console.WriteLine($"{App.Performances[performanceId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name} | {date} : {startTime}");
        Console.Write("Do you want to add play? (Y/N)\n> ");
        string choice = Console.ReadLine() ?? "";
        if (!choice.ToLower().StartsWith('y')){
            Console.WriteLine("Cancelling adding of play");
            return;
        }

        if (PlayLogic.AddPlay(location, startTime, date, hall, performanceId)) Console.WriteLine("Play has been added");
        else Console.WriteLine("Couldn't add play");
        Thread.Sleep(2500);
    }

}
