using Logic;

public static class MainTicketSystem{
    public static (bool,string,string)? IsTesting {get; set;}
    public static void CreateBookTicket(string performanceId, string date, string time, string room, bool activity){
        Ticket createNewTicket = new Ticket(performanceId, date, time, room, activity);
        if(!App.Tickets.ContainsKey(App.LoggedInUsername)){
            App.Tickets[App.LoggedInUsername] = new List<Ticket>();
        }
        App.Tickets[App.LoggedInUsername].Add(createNewTicket);
        TicketDataAccess.UpdateTickets();
        TicketPresentation.PrintTicket(createNewTicket);
        // createNewTicket.UpdateData(); before
    }

    public static void ShowTicketInfo(){
        if(App.Tickets.Count != 0){
            foreach(KeyValuePair<string, List<Ticket>> userTicket in App.Tickets){
                if (userTicket.Key != App.LoggedInUsername) continue;
                foreach(var ticket in userTicket.Value){
                    Console.WriteLine(ticket.TicketInfo());
                }
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

    public static void CancelTicketLogic(Ticket ticketToCancel){
        // Note: the ticketInApp here is a reference type not a copy of the class
        // OLD
        // foreach(var ticketInApp in App.Tickets){
        //     if(ticketInApp == ticketToCancel){
        //         ticketInApp.Ticket.IsActive = false;
        //         TicketDataAccess.UpdateTickets();
        //         break;
        //     }
        // }
        // This ticketToCancel is a reference to the App.Tickets ticket (classes are reference types)
        ticketToCancel.IsActive = false;
        TicketDataAccess.UpdateTickets();
    }

    public static void CheckOutdatedTickets(){
        foreach(var userTicket in App.Tickets){
            // Check time (string) with current DateTime
            DateTime currentTime = DateTime.Now;
            foreach(Ticket ticket in userTicket.Value){
                string currentTicketDateTime = $"{ticket.Date} {ticket.Time}";
                if(DateTime.TryParse(currentTicketDateTime, out DateTime ticketDate)){
                    if(ticketDate < currentTime){
                        ticket.IsActive = false;
                        TicketDataAccess.UpdateTickets();
                    }
                }
            }
        }
    }

    public static bool CancellationIsNotOneDayBefore(Ticket userTicket){
        DateTime currentTime = DateTime.Now;
        string currentTicketDateTime = $"{userTicket.Date} {userTicket.Time}";
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