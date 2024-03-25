using System.Text.Json;
public class MovieSchedule
{
    public List<string> LocationOptions = new() {"Kruispunt", "Zuidplein"};
    public string SelectLocation(){

        for (int i = 0; i < LocationOptions.Count; i++) {
            Console.WriteLine($"{i + 1}. {LocationOptions[i]}");
            }

            Console.Write("Selecteer een optie: ");
            string ?choice = Console.ReadLine();

            int index;
            if (int.TryParse(choice, out index) && index >= 1 && index <= LocationOptions.Count) {
                return LocationOptions[index - 1];
            } else {
                Console.WriteLine("Ongeldige keuze.");
                return SelectLocation();
            }
        }
    private void DisplayMovieOptions(string selectedLocation, Dictionary<string, List<MovieViewing>> movieOptions)
{
    Console.WriteLine($"Movies at {selectedLocation}:");
    foreach (var kvp in movieOptions)
    {
        foreach (var viewing in kvp.Value)
        {
            if (viewing.Location == selectedLocation)
            {
                Console.WriteLine($"{viewing.Naam} in {viewing.Location}");
            }
        }
    }

    Console.WriteLine("Available dates:");
    HashSet<string> availableDates = new HashSet<string>();
    foreach (var kvp in movieOptions)
    {
        foreach (var viewing in kvp.Value)
        {
            if (viewing.Location == selectedLocation)
            {
                availableDates.Add(viewing.Date);
            }
        }
    }
    int dateCounter = 1;
    Dictionary<int, string> dateOptions = new Dictionary<int, string>();
    foreach (var date in availableDates)
    {
        Console.WriteLine($"{dateCounter}. {date}");
        dateOptions[dateCounter] = date;
        dateCounter++;
    }

    Console.Write("Selecteer een datum: ");
    string? dateChoice = Console.ReadLine();
    if (!int.TryParse(dateChoice, out int chosenDateIndex) || !dateOptions.ContainsKey(chosenDateIndex))
    {
        Console.WriteLine("Ongeldige keuze.");
        return;
    }
    string chosenDate = dateOptions[chosenDateIndex];

    Console.WriteLine($"Available times on {chosenDate}:");
    int timeCounter = 1;
    Dictionary<int, string> timeOptions = new Dictionary<int, string>();
    foreach (var kvp in movieOptions)
    {
        foreach (var viewing in kvp.Value)
        {
            if (viewing.Location == selectedLocation && viewing.Date == chosenDate)
            {
                Console.WriteLine($"{timeCounter}. {viewing.Time} in room {viewing.Zaal}");
                timeOptions[timeCounter] = viewing.Time;
                timeCounter++;
            }
        }
    }

    Console.Write("Selecteer een tijd: ");
    string? timeChoice = Console.ReadLine();
    if (!int.TryParse(timeChoice, out int chosenTimeIndex) || !timeOptions.ContainsKey(chosenTimeIndex))
    {
        Console.WriteLine("Ongeldige keuze.");
        return;
    }
    string chosenTime = timeOptions[chosenTimeIndex];

    Console.WriteLine($"You have chosen {chosenTime} on {chosenDate} at {selectedLocation}");

    // Get movie viewings for the selected location, date, and time
    var selectedMovieViewings = movieOptions.SelectMany(kv => kv.Value)
        .Where(viewing => viewing.Location == selectedLocation && viewing.Date == chosenDate && viewing.Time == chosenTime);

    // Check if any movie viewings are found for the selected parameters
    if (!selectedMovieViewings.Any())
    {
        Console.WriteLine("No movie viewings found for the selected location, date, and time.");
        return;
    }

    // Retrieve the room/zaal information from the first viewing
    string? chosenZaal = selectedMovieViewings.First().Zaal;

    TicketInfo ticketInfo = new TicketInfo
            {
                Location = selectedLocation,
                Date = chosenDate,
                Time = chosenTime,
                Zaal = chosenZaal
            };
        // Serialize TicketInfo to JSON
        string jsonString = JsonSerializer.Serialize(ticketInfo);

        // Write JSON to file
        string ticketFilePath = "ticket.json"; // Specify the file path
        File.WriteAllText(ticketFilePath, jsonString);

        Console.WriteLine("Ticket information saved to ticket.json");

}
}

