using System;
using System.Collections.Generic;
using System.Globalization;
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
        Play newPlay = new(location, time, date, hall, playId);
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


