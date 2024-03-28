public static class MainTicketSystem{
    public static void CreateBookTicket(string movieName, string date, string time, string room){
        Ticket createNewTicket = new Ticket(movieName, date, time, room);
        createNewTicket.UpdateData();
    }

    public static void ShowTicketInfo(){
        List<KeyValueClass>? ticketList = ReadTicketJson.ReadTickets();
        if(ticketList != null){
            foreach(KeyValueClass ticketPair in ticketList){
                if (ticketPair.User != App.LoggedInUsername) continue;
                Console.WriteLine(ticketPair.Ticket.TicketInfo());
            }
        }
        else{
            Console.WriteLine("No tickets booked");
        }
    }
}