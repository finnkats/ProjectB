public static class MainTicketSystem{
    public static void CreateBookTicket(string movieName, string date, string time, string room){
        Ticket createNewTicket = new Ticket(movieName, date, time, room);
    }

    public static void ShowTicketInfo(){
        List<KeyValueClass>? ticketList = ReadTicketJson.ReadTickets();
        if(ticketList != null){
            foreach(KeyValueClass ticketPair in ticketList){
                Console.WriteLine(ticketPair.Ticket.TicketInfo());
            }
        }
        else{
            Console.WriteLine("No tickets booked");
        }
    }
}