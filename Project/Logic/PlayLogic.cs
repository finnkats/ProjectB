using System.Text.Json;
using System.Globalization;
public static class PlayLogic
{
    public static void Choose(string performanceId){
        if(App.LoggedInUsername == "Unknown"){
            bool readyToPay = TicketLoginPresentation.ChooseLoginOption();
            if(!readyToPay){
                return;
            }
        } else if (App.LoggedInUsername == "Admin123"){
            Console.WriteLine("Can't buy tickets as admin");
            Thread.Sleep(2500);
            return;
        }

        List<Play> AllViewings;
        if (App.Plays.ContainsKey(performanceId)) AllViewings = App.Plays[performanceId];
        else AllViewings = new();
        AllViewings = OneMonthFilter(AllViewings);
        
        string ViewingLocation = LocationPresentation.GetLocation("Select a location:", "Exit");
        if (ViewingLocation == "null") return;

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

        MainTicketSystem.CreateBookTicket(performanceId, ViewingDate, ViewingTime, ViewingHall, true);
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
                timesString += $"{timeCounter}: {viewing.Time} in {App.Halls[viewing.Hall].Name}\n";
                timeOptions.Add(timeCounter, viewing.Time);
                timeCounter++;
            }
        }

        if (timesString == $"Available times on {chosenDate}:\n") timesString = null;
        return (timesString, timeOptions);
    }

    public static void AddNewId(string id){
        App.Plays.Add(id, new List<Play>());
        DataAccess.UpdateList<Play>();
    }

    public static bool AddPlay(string location, string time, string date, string hall, string playId){
        if (!App.Plays.ContainsKey(playId)) return false;
        Play newPlay = new(location, time, date, hall, playId);
        App.Plays[playId].Add(newPlay);
        DataAccess.UpdateList<Play>();

        return true;
    }

    public static List<Play> OneMonthFilter(List<Play> Plays){
        DateTime OneMonthDate = DateTime.Now.Date.AddMonths(1);
        List<Play> FilteredPlays = new();
        foreach (var play in Plays){
            DateTime.TryParseExact(play.Date, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime playDate);
            if (playDate < OneMonthDate){
                FilteredPlays.Add(play);
            }
        }
        return FilteredPlays;
    }
}
