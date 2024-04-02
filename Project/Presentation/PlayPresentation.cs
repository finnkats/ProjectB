using System.Text.Json;
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
}
