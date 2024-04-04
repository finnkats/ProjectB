using System.Text.Json;
using System.Globalization;

public static class PlayPresentation
{
    // TODO: add method that gets all locations and puts them in a list
    public static List<string> LocationOptions = new() {"Theater het Kruispunt", "Theater Zuidplein"};
    public static string SelectLocation(){
        Console.Clear();
        // Loop through the available location options and display them to the user
        for (int i = 0; i < LocationOptions.Count; i++) {
            Console.WriteLine($"{i + 1}. {LocationOptions[i]}");
            }

            Console.Write("Select an option: ");
            string ?choice = Console.ReadLine();

            int index;
            // Check if the user input is a valid integer index within the range of options
            if (int.TryParse(choice, out index) && index >= 1 && index <= LocationOptions.Count) {
            // If the input is valid, return the selected location
                return LocationOptions[index - 1];
            } else {
                Console.WriteLine("Invalid choice.");
                return SelectLocation();
        }
    }


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

    public static void AddPlayDetails(){
        string? playId = PerformancePresentation.PerformanceChoice("For what performance do you want to add a play?");
        if (playId == null) return;

        string location;
        while (true){
            Console.Clear();
            Console.WriteLine("What location?\n1: Theater het Kruispunt\n2: Theater Zuidplein");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1){
                location = "Theater het Kruispunt";
                break;
            } else if (choice == 2){
                location = "Theater Zuidplein";
                break;
            }
        }

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
            time = $"{hours}:{minutes}:00";
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

        // for now
        string hall = "THEATERZAAL";

        if (PlayLogic.AddPlay(location, time, date.ToString(@"MM\/dd\/yyyy"), hall, playId)) Console.WriteLine("Play has been added");
        else Console.WriteLine("Couldn't add play");
        Thread.Sleep(2500);
    }
}
