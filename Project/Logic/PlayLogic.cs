using System.Text.Json;
public static class PlayLogic
{
    public static void Choose(string playID){
        var AllViewings = PlayReader.ReadMovieOptionsFromJson("DataSources/plays.json", playID);
        string ViewingLocation = PlayPresentation.SelectLocation();
        string? ViewingDate = PlayPresentation.PrintDates(ViewingLocation, AllViewings);
        if (ViewingDate == null) return;
        string? ViewingTime = PlayPresentation.PrintTimes(ViewingLocation, ViewingDate, AllViewings);
        if (ViewingTime == null) return;

        string ViewingHall = "";
        foreach (var viewing in AllViewings){
            if (viewing.Date == ViewingDate && viewing.Time == ViewingTime){
                ViewingHall = viewing.Hall;
                break;
            }
        }

        Console.Clear();
        // TODO: get play name from ID
        MainTicketSystem.CreateBookTicket(playID, ViewingDate, ViewingTime, $"{ViewingLocation}: {ViewingHall}");

        // For now
        MainTicketSystem.ShowTicketInfo();
        Thread.Sleep(10000);
    }

    public static (string?, Dictionary<int, string>?) GetDates(string selectedLocation, List<Play> playOptions){
        if (playOptions.Count() == 0) return (null, null);
        string datesString = "";
        datesString += "Available dates:\n";

        HashSet<string> availableDates = new HashSet<string>();
        foreach (var viewing in playOptions)
        {
            if (viewing.Location == selectedLocation)
            {
                availableDates.Add(viewing.Date);
            }
        }

        int dateCounter = 1;
        Dictionary<int, string> dateOptions = new Dictionary<int, string>();
        foreach (var date in availableDates)
        {
            datesString += $"{dateCounter}: {date}\n";
            dateOptions.Add(dateCounter, date);
            dateCounter++;
        }
        
        return (datesString, dateOptions);
    }

    public static (string?, Dictionary<int, string>?) GetTimes(string selectedLocation, string chosenDate, List<Play> playOptions){
        if (playOptions.Count() == 0) return (null, null);
        string timesString = $"Available times on {chosenDate}:\n";
        int timeCounter = 1;
        Dictionary<int, string> timeOptions = new Dictionary<int, string>();
        foreach (var viewing in playOptions)
        {
            if (viewing.Location == selectedLocation && viewing.Date == chosenDate)
            {
                timesString += $"{timeCounter}: {viewing.Time} in {viewing.Hall}\n";
                timeOptions.Add(timeCounter, viewing.Time);
                timeCounter++;
            }
        }

        return (timesString, timeOptions);
    }
}
