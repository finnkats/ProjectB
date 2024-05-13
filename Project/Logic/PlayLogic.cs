using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Threading;

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
        AllViewings = FilterFullPlays(AllViewings);
        
        // Display all viewings and allow user to choose
        PlayPresentation.DisplayViewings(AllViewings, performanceId);

    }

    public static bool AddPlay(string location, string time, string date, string hall, string playId){
        if (!App.Plays.ContainsKey(playId)) return false;
        if (!ValidDate(date)) return false;
        if (!ValidTime(time)) return false;
        time += ":00";
        Play newPlay = new(location, time, date, hall, playId);
        App.Plays[playId].Add(newPlay);
        DataAccess.UpdateList<Play>();
        App.ArchivedPlays[playId].Add(new ArchivedPlay(location, time, date, hall, playId));
        DataAccess.UpdateList<ArchivedPlay>();
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

    public static void RemoveOutdatedPlays(){
        foreach (var playList in App.Plays.Values){
            // Get the time for 1 hour in the future
            DateTime dateLimit = DateTime.Now.Add(DateTime.Now.TimeOfDay).AddHours(1);
            // Loop backwards over list, so removing wont cause errors
            for (int i = playList.Count - 1; i >= 0; i--){
                if (!DateTime.TryParse($"{playList[i].Date} {playList[i].Time}", out DateTime playDate)) continue;
                if (playDate > dateLimit) continue;
                playList.RemoveAt(i);
            }
        }
        DataAccess.UpdateList<Play>();
    }

    public static bool ValidTime(string givenTime){
        string[] times = givenTime.Split(':');
        if (times.Length != 2) return false;
        if (!Int32.TryParse(times[0], out int hours)) return false;
        if (!Int32.TryParse(times[1], out int minutes)) return false;
        if (0 > hours || hours > 23) return false;
        if (0 > minutes || minutes > 59) return false;
        return true;
    }

    public static bool ValidDate(string givenDate){
        if (!DateTime.TryParseExact(givenDate, "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime date)) return false;
        if (date < DateTime.Now.AddDays(1)) return false;
        return true;
    }

    public static List<Play> FilterFullPlays(List<Play> plays)
    {
        List<Play> filteredPlays = plays;
        foreach (Play play in plays) {
            if (play.BookedSeats == App.Halls[play.Hall].Seats) {
                filteredPlays.Remove(play);
            }
        }
        return filteredPlays;
    }
    public static void AddBooking(Ticket newTicket)
    {
        foreach (Play play in App.Plays[newTicket.PerformanceId]) {
            if (play.Date == newTicket.Date && play.Time == newTicket.Time && play.Hall == newTicket.Hall) {
                play.BookedSeats += 1;
                DataAccess.UpdateList<Play>();
                break;
            }
        }
    }
}
