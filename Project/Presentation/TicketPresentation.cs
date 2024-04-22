public static class TicketPresentation
{
    public static void PrintTickets()
    {
        while (true)
        {
            Console.Clear();
            if (App.Tickets != null)
            {
                foreach (UserTicket ticketPair in App.Tickets)
                {
                    if (ticketPair.User != App.LoggedInUsername) continue;
                    Console.WriteLine(ticketPair.Ticket.TicketInfo());
                }
            }
            else
            {
                Console.WriteLine("No tickets booked");
            }

            Console.WriteLine("\n1 to Exit");
            if (Console.ReadLine() == "1") return;
        }
    }

    public static void PrintTicket(Ticket ticket)
    {
        Console.Clear();
        Console.WriteLine("Just booked:");
        Console.WriteLine(ticket.TicketInfo());
        Thread.Sleep(6000);
    }

    public static Ticket TicketMenu()
    {
        Console.Clear();
        var TicketsList = MainTicketSystem.SortActiveTicket();
        if (TicketsList != null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inactive Tickets");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 82));
            Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "Performance Name", "Date", "Time",
                "Hall"));
            Console.WriteLine(new string('-', 82));

            foreach (Ticket ticket in TicketsList[1])
            {
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|",
                    App.Performances[ticket.PerformanceId].Name, ticket.Date, ticket.Time, ticket.Hall));
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "", "", "", ""));
                Console.WriteLine(new string('-', 82));

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\nActive Tickets");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 82));
            Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "Performance Name", "Date", "Time",
                "Hall"));
            Console.WriteLine(new string('-', 82));

            foreach (Ticket ticket in TicketsList[0])
            {
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|",
                    App.Performances[ticket.PerformanceId].Name, ticket.Date, ticket.Time, ticket.Hall));
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "", "", "", ""));
                Console.WriteLine(new string('-', 82));
            }
            int IndexNumber = 0;
            Console.WriteLine("\n\nIs there a ticket you want to cancel?");
            foreach (Ticket ticket in TicketsList[1])
            {
                Console.WriteLine($"{IndexNumber}. {App.Performances[ticket.PerformanceId].Name} ");
            }
            Console.WriteLine("Q. I don't want to cancel any tickets\n");
            string? userInput = Console.ReadLine();
            if (userInput != null)
            {
                if (userInput.ToLower() == "q")
                {
                    return null;
                }

                else
                {
                    int IndexInt = Convert.ToInt32(userInput);
                    int ReturnIndex = IndexInt - 1;
                    return TicketsList[0][ReturnIndex];
                }
            }

        }
    }
}    
