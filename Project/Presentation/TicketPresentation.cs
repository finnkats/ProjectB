public static class TicketPresentation{
    public static void PrintTickets(){
        while (true){
            Console.Clear();
            if(App.Tickets != null){
                foreach(UserTicket ticketPair in App.Tickets){
                    if (ticketPair.User != App.LoggedInUsername) continue;
                    Console.WriteLine(ticketPair.Ticket.TicketInfo());
                }
            }
            else{
                Console.WriteLine("No tickets booked");
            }
            Console.WriteLine("\n1 to Exit");
            if (Console.ReadLine() == "1") return;
        }
    }
    
    public static void PrintTicket(Ticket ticket){
        Console.Clear();
        Console.WriteLine("Just booked:");
        Console.WriteLine(ticket.TicketInfo());
        Thread.Sleep(6000);
    }

    public static void TicketMenu()
    {
        Console.Clear();
        var TicketsList = MainTicketSystem.SortActiveTicket();
        if (TicketsList != null)
        {

            Console.WriteLine(new string('-', 82));
            Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "Performance Name", "Date", "Time",
                "Hall"));
            foreach (Ticket ticket in TicketsList[1])
            {
                Console.WriteLine(
                    String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", ticket.PerformanceId, "test2", "test3", "test4"));
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "", "", "", ""));
                Console.WriteLine(new string('-', 82));

            }
        }
    }
}
