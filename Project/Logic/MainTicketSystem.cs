using Logic;

public static class MainTicketSystem{
    // public static Tuple<bool,string,string>? IsTesting {get; set;}
    public static (bool,string,string)? IsTesting {get; set;}
    public static void CreateBookTicket(string performanceId, string date, string time, string room, bool activity){
        Ticket createNewTicket = new Ticket(performanceId, date, time, room, activity);
        TicketPresentation.PrintTicket(createNewTicket);
        createNewTicket.UpdateData();
    }

    public static void ShowTicketInfo(){
        if(App.Tickets.Count != 0){
            foreach(UserTicket ticketPair in App.Tickets){
                if (ticketPair.User != App.LoggedInUsername) continue;
                Console.WriteLine(ticketPair.Ticket.TicketInfo());
            }
        }
        else{
            Console.WriteLine("No tickets booked");
        }
    }

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

    // Needs to change when UserTicket is changed
    public static void CancelTicketLogic(UserTicket ticketToCancel){
        // Note: the ticketInApp here is a reference type not a copy of the class
        foreach(var ticketInApp in App.Tickets){
            if(ticketInApp == ticketToCancel){
                ticketInApp.Ticket.IsActive = false;
                TicketDataAccess.UpdateTickets();
                break;
            }
        }
    }

    public static void CheckOutdatedTickets(){
        // Change the for loop after the UserTicket has been changed
        foreach(var userTicket in App.Tickets){
            // Check time (string) with current DateTime
            DateTime currentTime = DateTime.Now;
            string currentTicketDateTime = $"{userTicket.Ticket.Date} {userTicket.Ticket.Time}";
            if(DateTime.TryParse(currentTicketDateTime, out DateTime ticketDate)){
                if(ticketDate < currentTime){
                    userTicket.Ticket.IsActive = false;
                    TicketDataAccess.UpdateTickets();
                }
            }
        }
    }

    public static bool CancellationIsNotOneDayBefore(UserTicket userTicket){
        DateTime currentTime = DateTime.Now;
        string currentTicketDateTime = $"{userTicket.Ticket.Date} {userTicket.Ticket.Time}";
        if(DateTime.TryParse(currentTicketDateTime, out DateTime ticketDate)){
            DateTime oneDayBefore = ticketDate.AddDays(-1);
            return currentTime < oneDayBefore;
        }
        return false;
    }

    public static List<List<Ticket>> SortActiveTicket()
    {
        List<Ticket> ActiveTickets = new();
        List<Ticket> InactiveTickets = new();
        List <List< Ticket>> ReturnLists = new();


        if (App.Tickets != null)
        {
            foreach (UserTicket ticketPair in App.Tickets)
            {
                if (App.LoggedInUsername == ticketPair.User)
                {
                    if (ticketPair.Ticket.IsActive == true)
                    {
                        ActiveTickets.Add(ticketPair.Ticket);
                    }
                    else
                    {
                        InactiveTickets.Add(ticketPair.Ticket);
                    }
                }
            }
        }
        ReturnLists.Add(ActiveTickets);
        ReturnLists.Add(InactiveTickets);
        return ReturnLists;
    }
}