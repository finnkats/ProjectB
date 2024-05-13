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

        string startTime;
        while (true){
            Console.Clear();
            Console.WriteLine("What time? [HH:MM]");
            startTime = Console.ReadLine() ?? "99:99";
            string[] times = startTime.Split(':');
            if (times.Length != 2) continue;
            if (!Int32.TryParse(times[0], out int hours)) continue;
            if (!Int32.TryParse(times[1], out int minutes)) continue;
            if (0 > hours || hours > 23) continue;
            if (0 > minutes || minutes > 59) continue;
            startTime = $"{startTime}:00";
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
        if(!PlayLogic.IsHallAvailable(DateTime.Parse(startTime), date, hall)){return;}

        if (PlayLogic.AddPlay(location, startTime, date.ToString(@"dd\/MM\/yyyy"), hall, playId)) Console.WriteLine("Play has been added");
        else Console.WriteLine("Couldn't add play");
        Thread.Sleep(2500);
    }
}
