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

    public static bool AddPlay(string location, string startTime, string date, string hall, string playId){
        if (!App.Plays.ContainsKey(playId)) return false;
        if (!ValidDate(date)) return false;
        if (!ValidTime(startTime)) return false;
        Play newPlay = new(location, startTime, date, hall, playId);
        App.Plays[playId].Add(newPlay);
        NotificationLogic.SendOutNotifications(newPlay);
        DataAccess.UpdateList<Play>();
        App.ArchivedPlays[playId].Add(new ArchivedPlay(location, startTime, date, hall, playId));
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

    public static bool IsHallAvailable(string location, DateTime date, string startTime, string hall){
        if(hall == "null") return true;
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

    public static void RemoveOutdatedPlays(){
        foreach (var playList in App.Plays.Values){
            // Get the time for 1 hour in the future
            DateTime dateLimit = DateTime.Now.Add(DateTime.Now.TimeOfDay).AddHours(1);
            // Loop backwards over list, so removing wont cause errors
            for (int i = playList.Count - 1; i >= 0; i--){
                if (!DateTime.TryParse($"{playList[i].Date} {playList[i].StartTime}", out DateTime playDate)) continue;
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
                if (plays.Count == 0) break;
            }
        }
        return filteredPlays;
    }
    public static void AddBooking(Ticket newTicket)
    {
        foreach (Play play in App.Plays[newTicket.PerformanceId]) {
            if (play.Date == newTicket.Date && play.StartTime == newTicket.Time && play.Hall == newTicket.Hall) {
                Array.ForEach(newTicket.SeatNumbers, number => play.Seats.Add(number));
                DataAccess.UpdateList<Play>();
                break;
            }
        }
    }
    public static void RemoveBooking(Ticket ticket)
    {
        foreach (Play play in App.Plays[ticket.PerformanceId])
        {
            if (play.Date == ticket.Date && play.StartTime == ticket.Time && play.Hall == ticket.Hall)
            {
                Array.ForEach(ticket.SeatNumbers, number => play.Seats.Remove(number));
                DataAccess.UpdateList<Play>();
                break;
            }
        }
    }
}
