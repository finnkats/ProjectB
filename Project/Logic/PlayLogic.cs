using System.Text.Json;
public static class PlayLogic
{
    public static void Choose(string performanceId){
        List<Play> AllViewings;
        if (App.Plays.ContainsKey(performanceId)) AllViewings = App.Plays[performanceId];
        else AllViewings = new();

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

        MainTicketSystem.CreateBookTicket(performanceId, ViewingDate, ViewingTime, $"{ViewingLocation}: {ViewingHall}");
    }

    public static (string?, Dictionary<int, string>?) GetDates(string selectedLocation, List<Play> playOptions){
        if (playOptions.Count() == 0) return (null, null);
        string? datesString = "";
        datesString += "Available dates:\n";

        HashSet<string> availableDates = new HashSet<string>();
        foreach (var viewing in playOptions)
        {
            if (viewing.Location == selectedLocation)
            {
                availableDates.Add(viewing.Date);
            }
        }
        List<string> availableDatesOrdered = availableDates.Order().ToList();

        int dateCounter = 1;
        Dictionary<int, string> dateOptions = new Dictionary<int, string>();
        foreach (var date in availableDatesOrdered)
        {
            datesString += $"{dateCounter}: {date}\n";
            dateOptions.Add(dateCounter, date);
            dateCounter++;
        }
        
        if (datesString == "Available dates:\n") datesString = null;
        return (datesString, dateOptions);
    }

    public static (string?, Dictionary<int, string>?) GetTimes(string selectedLocation, string chosenDate, List<Play> playOptions){
        if (playOptions.Count() == 0) return (null, null);
        string? timesString = $"Available times on {chosenDate}:\n";
        int timeCounter = 1;
        Dictionary<int, string> timeOptions = new Dictionary<int, string>();

        var playOptionsOrdered = playOptions.OrderBy(performance => performance.Time).ToList();
        foreach (var viewing in playOptionsOrdered)
        {
            if (viewing.Location == selectedLocation && viewing.Date == chosenDate)
            {
                timesString += $"{timeCounter}: {viewing.Time} in {viewing.Hall}\n";
                timeOptions.Add(timeCounter, viewing.Time);
                timeCounter++;
            }
        }

        if (timesString == $"Available times on {chosenDate}:\n") timesString = null;
        return (timesString, timeOptions);
    }

    public static void AddNewId(string id){
        App.Plays.Add(id, new List<Play>());
        PlayDataAccess.UpdatePlays();
    }

    public static bool AddPlay(string location, string time, string date, string hall, string playId){
        if (!App.Plays.ContainsKey(playId)) return false;
        Play newPlay = new(location, time, date, hall, playId);
        App.Plays[playId].Add(newPlay);
        PlayDataAccess.UpdatePlays();

        return true;
    }
}
