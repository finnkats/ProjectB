public static class MainTicketSystem{
    public static void CreateBookTicket(string performanceId, string date, string time, string room){
        Ticket createNewTicket = new Ticket(performanceId, date, time, room);
        createNewTicket.UpdateData();
    }

    public static void ShowTicketInfo(){
        List<UserTicket>? ticketList = ReadTicketJson.ReadTickets();
        if(ticketList != null){
            foreach(UserTicket ticketPair in ticketList){
                if (ticketPair.User != App.LoggedInUsername) continue;
                Console.WriteLine(ticketPair.Ticket.TicketInfo());
            }
        }
        else{
            Console.WriteLine("No tickets booked");
        }
    }
}