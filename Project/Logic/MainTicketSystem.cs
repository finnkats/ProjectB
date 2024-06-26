using Logic;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

public static class MainTicketSystem{

    private static TicketLogger logger = new TicketLogger();
    public static (bool,string,string)? IsTesting {get; set;}
    // Creates a new Ticket (UserTicket)
    public static void CreateBookTicket(string performanceId, string date, string time, string room, HashSet<int> seats, bool test=false){
        if(!App.Tickets.ContainsKey(App.LoggedInUsername)){
            App.Tickets[App.LoggedInUsername] = new List<Ticket>();
        }
        
        Ticket createNewTicket = new Ticket(performanceId, date, time, room, seats.ToArray(), true);
        var orderedSeats = string.Join(", ", seats.OrderBy(seat => seat));
        logger.LogAction("Bought a new ticket", new { PerformanceID = performanceId, Date = date, Time = time, Hall = room, OrderedSeats = orderedSeats, Active = true });
        PlayLogic.AddBooking(createNewTicket);
        App.Tickets[App.LoggedInUsername].Add(createNewTicket);

        DataAccess.UpdateList<Ticket>();
        if (!test) TicketPresentation.PrintTicket(createNewTicket, performanceId);
    }

    // Gets login info and checks if logged in person is an admin
    // this method probably already exists in AccountLogic
    public static (bool, string, string) LoginCheckAdmin(){
        string loginName, loginPassword;
        (loginName, loginPassword) = (IsTesting != null && IsTesting.Value.Item1) ? (IsTesting.Value.Item2, IsTesting.Value.Item3) : AccountPresentation.GetLoginDetails();
        // Check if admin here instead of checking it the method from AccountLogic
        foreach(var account in App.Accounts.Values){
            if(!AccountLogic.CheckLogin(loginName, loginPassword, account)){
                continue;
            }
            if(account.IsAdmin){
                return (true, loginName, loginPassword);
            }
        }
        return (false, loginName, loginPassword);
    }

    public static void CancelTicketLogic(Ticket ticketToCancel){
        // This ticketToCancel is a reference to the App.Tickets ticket (classes are reference types)
        ticketToCancel.IsActive = false;
        logger.LogAction("Cancelled a ticket", new { PerformanceID = ticketToCancel.PerformanceId, date = ticketToCancel.Date, hall = ticketToCancel.Hall, Activity = ticketToCancel.IsActive });
        PlayLogic.RemoveBooking(ticketToCancel);
        DataAccess.UpdateList<Ticket>();
    }

    public static void CheckOutdatedTickets(){
        foreach(var userTicket in App.Tickets){
            // Check time (string) with current DateTime
            DateTime currentTime = DateTime.Now;
            foreach(Ticket ticket in userTicket.Value){
                string currentTicketDateTime = $"{ticket.Date} {ticket.Time}";
                if(DateTime.TryParse(currentTicketDateTime, System.Globalization.CultureInfo.GetCultureInfo("nl-NL"), out DateTime ticketDate)){
                    if(ticketDate < currentTime){
                        ticket.IsActive = false;
                        DataAccess.UpdateList<Ticket>();
                    }
                }
            }
        }
    }

    public static bool CancellationIsNotOneDayBefore(Ticket userTicket){
        DateTime currentTime = DateTime.Now;
        string currentTicketDateTime = $"{userTicket.Date} {userTicket.Time}";
        if(DateTime.TryParse(currentTicketDateTime, System.Globalization.CultureInfo.GetCultureInfo("nl-NL"), out DateTime ticketDate)){
            DateTime oneDayBefore = ticketDate.AddDays(-1);
            return currentTime < oneDayBefore;
        }
        return false;
    }

    // 2 lists, active and inactive tickets
    // returns these lists in a list [0] is inactive and [1] active (should become a tuple or something)
    public static List<List<Ticket>> SortActiveTicket()
    {
        List<Ticket> ActiveTickets = new();
        List<Ticket> InactiveTickets = new();
        List <List< Ticket>> ReturnLists = new();


        if (App.Tickets != null)
        {
            if(App.Tickets.ContainsKey(App.LoggedInUsername)){
                foreach(Ticket ticket in App.Tickets[App.LoggedInUsername]){
                    if (ticket.IsActive == true)
                    {
                        ActiveTickets.Add(ticket);
                    }
                    else
                    {
                        InactiveTickets.Add(ticket);
                    }
                }
            }
        }
        ReturnLists.Add(ActiveTickets);
        ReturnLists.Add(InactiveTickets);
        return ReturnLists;
    }
}