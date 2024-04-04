using Logic;

public static class MainTicketSystem{
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
}