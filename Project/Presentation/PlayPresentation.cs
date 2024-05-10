using System.Text.Json;
using System.Globalization;

public static class PlayPresentation
{
    // returns the chosen date
    public static void DisplayViewings(List<Play> playOptions, string performanceId)
{
    Console.Clear();
    Console.WriteLine($"Plays for {App.Performances[performanceId].Name}\n------------------------------------------------------------------");
    Console.WriteLine($"|# |Location               |Date        |Time      |Hall         |");
    Console.WriteLine("------------------------------------------------------------------");

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

        Console.WriteLine($"|{i + 1,-2}|{locationName,-23}|{viewing.Date,-12}|{viewing.Time,-7}  |{hallName,-13}|");
    }
    Console.WriteLine("------------------------------------------------------------------");

    int chosenIndex = -1;
    while (chosenIndex == -1){

        Console.Write("Select a performance by entering its index or '0' to cancel: ");
        
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
                MainTicketSystem.CreateBookTicket(performanceId, chosenViewing.Date, chosenViewing.Time, chosenViewing.Hall, true);

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
    public static void AddPlayDetails(){
        string? playId = App.performancePresentation.PerformanceChoice("For what performance do you want to add a play?");
        if (playId == null) return;

        string location = App.locationPresentation.GetItem("Choose a location:", "Cancel");
        if (location == "null") return;

        string time;
        while (true){
            Console.Clear();
            Console.WriteLine("What time? [HH:MM]");
            time = Console.ReadLine() ?? "99:99";
            string[] times = time.Split(':');
            if (times.Length != 2) continue;
            if (!Int32.TryParse(times[0], out int hours)) continue;
            if (!Int32.TryParse(times[1], out int minutes)) continue;
            if (0 > hours || hours > 23) continue;
            if (0 > minutes || minutes > 59) continue;
            time = $"{time}:00";
            break;
        }

        DateTime date;
        while (true){
            Console.Clear();
            Console.WriteLine("What date? [DD/MM/YYYY]? (can't be today or in the past)");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out date)) continue;
            if (date < DateTime.Now.AddDays(1)) continue;
            break;
        }

        string hall = App.hallPresentation.GetItem("Choose a hall:", "Cancel", location);
        if (hall == "null") return;

        if (PlayLogic.AddPlay(location, time, date.ToString(@"dd\/MM\/yyyy"), hall, playId)) Console.WriteLine("Play has been added");
        else Console.WriteLine("Couldn't add play");
        Thread.Sleep(2500);
    }
}
