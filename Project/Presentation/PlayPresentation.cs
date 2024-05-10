using System.Text.Json;
using System.Globalization;

public static class PlayPresentation
{
    // returns the chosen date
    public static string? PrintDates(string selectedLocation, List<Play> playOptions){
        Console.Clear();
        (string? datesString, Dictionary<int, string>? datesOptions) = PlayLogic.GetDates(selectedLocation, playOptions);
        if (datesString == null || datesOptions == null){
            Console.WriteLine("No viewings for the play");
            Thread.Sleep(2500);
            return null;
        }
        Console.WriteLine(datesString);

        string? chosenDate = null;
        while (chosenDate == null){
            Console.Write("Select a date: ");
            string? dateChoice = Console.ReadLine();
    
            if (int.TryParse(dateChoice, out int chosenDateIndex) && datesOptions.ContainsKey(chosenDateIndex))
            {
                chosenDate = datesOptions[chosenDateIndex];
            }
            else Console.WriteLine("Invalid choice.");
        }
        return chosenDate;
    }

    // returns the chosen time
    public static string? PrintTimes(string selectedLocation, string chosenDate, List<Play> playOptions){
        Console.Clear();
        (string? timesString, Dictionary<int, string>? timesOptions) = PlayLogic.GetTimes(selectedLocation, chosenDate, playOptions);
        if (timesString == null || timesOptions == null){
            Console.WriteLine("No times for the play");
            Thread.Sleep(2500);
            return null;
        }
        Console.WriteLine(timesString);

        string? chosenTime = null;
        while (chosenTime == null){
            Console.Write("Select a time: ");
            string? timeChoice = Console.ReadLine();
            if (int.TryParse(timeChoice, out int chosenTimeIndex) && timesOptions.ContainsKey(chosenTimeIndex))
            {
                chosenTime = timesOptions[chosenTimeIndex];
            }
            else Console.WriteLine("Invalid choice.");
        }
        
        return chosenTime;
    }

    // Collects the data needed to add a play
    public static void AddPlayDetails(){
        string? playId = App.performancePresentation.PerformanceChoice("For what performance do you want to add a play?");
        if (playId == null) return;

        string location = App.locationPresentation.GetItem("Choose a location:", "Cancel");
        if (location == "null") return;

        string hall = App.hallPresentation.GetItem("Choose a hall:", "Cancel", location);
        if (hall == "null") {
            Console.WriteLine("Cancelling adding of play");
            return;
        }

        string date;
        while (true){
            Console.Clear();
            Console.WriteLine($"{App.Performances[playId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name}\n");
            Console.WriteLine("What date? [DD/MM/YYYY]? (can't be today or in the past)");
            string givenDate = Console.ReadLine() ?? "";
            if (!PlayLogic.ValidDate(givenDate)) continue;
            date = givenDate;
            break;
        }

        string time;
        while (true){
            Console.Clear();
            Console.WriteLine($"{App.Performances[playId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name} | {date}\n");
            Console.WriteLine("What time? [HH:MM]");
            time = Console.ReadLine() ?? "99:99";
            if (!PlayLogic.ValidTime(time)) continue;
            break;
        }

        Console.Clear();

        Console.WriteLine("Do you want to add play? (Y/N)");
        Console.WriteLine($"{App.Performances[playId].Name} | {App.Locations[location].Name} : {App.Halls[hall].Name} | {date} : {time}");
        string choice = Console.ReadLine() ?? "";
        if (!choice.ToLower().StartsWith('y')){
            Console.WriteLine("Cancelling adding of play");
            return;
        }
        Console.WriteLine();

        if (PlayLogic.AddPlay(location, time, date, hall, playId)) Console.WriteLine("Play has been added");
        else Console.WriteLine("Couldn't add play");
        Thread.Sleep(2500);
    }
}
