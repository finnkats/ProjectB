using System.Text.Json;
using System.Globalization;
public static class PlayLogic
{
    //  This is the start of creating a ticket
    public static void Choose(string performanceId){
        // Checks if logged in user is no one, (means user should login)
        // or if admin is logged in (should now be able to buy a ticket)
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

        // Gets a list of plays from a performance from the given performanceId
        List<Play> AllViewings;
        if (App.Plays.ContainsKey(performanceId)) AllViewings = App.Plays[performanceId];
        else AllViewings = new();
        AllViewings = OneMonthFilter(AllViewings);
        
        // Gets the location
        string ViewingLocation = App.locationPresentation.GetItem("Select a location:", "Exit");
        if (ViewingLocation == "null") return;

        // Gets the date
        string? ViewingDate = PlayPresentation.PrintDates(ViewingLocation, AllViewings);
        if (ViewingDate == null) return;

        // Gets the time
        string? ViewingTime = PlayPresentation.PrintTimes(ViewingLocation, ViewingDate, AllViewings);
        if (ViewingTime == null) return;

        // Gets the hall
        string ViewingHall = "";
        foreach (var viewing in AllViewings){
            if (viewing.Date == ViewingDate && viewing.StartTime == ViewingTime){
                ViewingHall = viewing.Hall;
                break;
            }
        }

        // Creates the ticket
        foreach (Play play in AllViewings) {
            if (play.Location == ViewingLocation && play.Date == ViewingDate && play.StartTime == ViewingTime && play.Hall == ViewingHall) {
                if (play.BookedSeats == App.Halls[ViewingHall].Seats) {
                    Console.WriteLine("Selected Play is full");
                    Thread.Sleep(2500);
                    return;
                }
                play.BookedSeats += 1;
            }
        }
        MainTicketSystem.CreateBookTicket(performanceId, ViewingDate, ViewingTime, ViewingHall, true);
    }

    // returns a string (which is basically a menu)
    // and a Dictionary of int and string (where int is the index of the option shown in the menu) and
    // string is the string of the date
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

    // Does the same as GetDates but then with times
    public static (string?, Dictionary<int, string>?) GetTimes(string selectedLocation, string chosenDate, List<Play> playOptions){
        if (playOptions.Count() == 0) return (null, null);
        string? timesString = $"Available times on {chosenDate}:\n";
        int timeCounter = 1;
        Dictionary<int, string> timeOptions = new Dictionary<int, string>();

        var playOptionsOrdered = playOptions.OrderBy(performance => performance.StartTime).ToList();
        foreach (var viewing in playOptionsOrdered)
        {
            if (viewing.Location == selectedLocation && viewing.Date == chosenDate)
            {
                timesString += $"{timeCounter}: {viewing.StartTime} in {App.Halls[viewing.Hall].Name}\n";
                timeOptions.Add(timeCounter, viewing.StartTime);
                timeCounter++;
            }
        }

        if (timesString == $"Available times on {chosenDate}:\n") timesString = null;
        return (timesString, timeOptions);
    }

    public static bool AddPlay(string location, string startTime, string date, string hall, string playId){
        if (!App.Plays.ContainsKey(playId)) return false;
        Play newPlay = new(location, startTime, date, hall, playId);
        App.Plays[playId].Add(newPlay);
        DataAccess.UpdateList<Play>();

        return true;
    }

    // Gets a list of plays and returns all plays which are less than 1 month in the future
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

    public static List<Play> GetPlaysFromPresentations(string playID){
        if (App.Plays.ContainsKey(playID)){
            return App.Plays[playID];
        } else {
            return new List<Play>();
        }
    }

    public static bool IsHallAvailable(string location, DateTime date, string startTime, string hall){
        TimeSpan parsedStartTime = TimeSpan.Parse(startTime);

        DateTime proposedStartDateTime = date.Add(parsedStartTime);

        // Check if there are existing plays in the same hall at the same time
        foreach (var playList in App.Plays.Values){
            foreach(var play in playList){
                if (play.Location == location && play.Hall == hall){
                    string existingPlayStartStr = $"{play.Date} {play.StartTime}";
                    string existingPlayEndStr = $"{play.Date} {play.EndTime}";
                    DateTime existingPlayStart = DateTime.Parse(existingPlayStartStr);
                    DateTime existingPlayEnd = DateTime.Parse(existingPlayEndStr);
                    int? currentRuntime = App.performanceLogic.GetRuntime(play.PerformanceId);
                    // DateTime existingPlayEnd = existingPlayStart.AddMinutes((double)currentRuntime!);

                    // Check for time overlap
                    // Console.WriteLine($"Check:\n{existingPlayStart}\n{existingPlayEnd}");
                    // Thread.Sleep(10000);
                    if (proposedStartDateTime <= existingPlayEnd && proposedStartDateTime.AddMinutes((double)currentRuntime!) >= existingPlayStart){
                        return false; // Hall is not available
                    }
                }
            }
        }
        return true; // Hall is available
    }
}
