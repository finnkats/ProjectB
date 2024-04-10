using Logic;

public static class MainTicketSystem{
    // public static Tuple<bool,string,string>? IsTesting {get; set;}
    public static (bool,string,string)? IsTesting {get; set;}
    public static void CreateBookTicket(string performanceId, string date, string time, string room){
        Ticket createNewTicket = new Ticket(performanceId, date, time, room);
        createNewTicket.UpdateData();
    }

    public static void ShowTicketInfo(){
        if(App.Tickets != null){
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
        (loginName, loginPassword) = IsTesting!.Value.Item1 ? (IsTesting.Value.Item2, IsTesting.Value.Item3) : AccountPresentation.GetLoginDetails();
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
}