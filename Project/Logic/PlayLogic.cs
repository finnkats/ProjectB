using System.Text.Json;
public static class PlayLogic
{
    public static void Choose(string playID){
        var AllViewings = PlayReader.ReadMovieOptionsFromJson(playID);
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

    public static List<DateTime> ConvertToValidDates(List<string> dateStringList)
    {
        List<DateTime> validDates = new List<DateTime>();

        foreach (var dateString in dateStringList.ToList()) // Iterate over a copy of the list
        {
            try
            {
                DateTime parsedDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                validDates.Add(parsedDate);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid date format: {dateString}");
                dateStringList.Remove(dateString); // Remove invalid date string from original list
            }
        }

        return validDates;
    }

    public static List<string> FilterMoviesDate(List<DateTime> playDateList)
    {
        List<string> dateListBeforeOneMonth = new List<string>();
        int counter = 1;

        foreach (var playDate in playDateList)
        {   
            DateTime currentDate = currentDate.Now.Date;
            DateTime oneMonthAhead = currentDate.AddMonths(1);

            if (playDate >= currentDate && playDate <= oneMonthAhead)
            {
                string dateString = playDate.ToString("dd/MM/yyyy");
                Console.WriteLine($"{counter}. {dateString}");
                dateListBeforeOneMonth.Add(dateString);
                counter++;
            }
        }

        return dateListBeforeOneMonth;
    }

}
