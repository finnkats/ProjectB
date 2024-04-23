using System.Security.Authentication.ExtendedProtection;
using System.Threading;

public static class TicketPresentation{
    public static void TicketMenu()
    {
        Console.Clear();
        var TicketsList = MainTicketSystem.SortActiveTicket(); // Method sorts tickets owned by user by if they are active or not
        if (TicketsList != null)
        {
            // This part of the method prints the Inactive tickets
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inactive Tickets");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 82));
            Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "Performance Name", "Date", "Time",
                "Hall"));
            Console.WriteLine(new string('-', 82));

            foreach (Ticket ticket in TicketsList[1])
            {
                string performanceName = App.Performances[ticket.PerformanceId].Name;
                if (performanceName.Length > 19) performanceName = $"{performanceName.Substring(0, 16)}..."; // This line replaces letters from a string with "..." if the string is too long for the tabel
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|",
                    performanceName, ticket.Date, ticket.Time, ticket.Hall));
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "", "", "", ""));
                Console.WriteLine(new string('-', 82));

            }
            // this part of the code prints the Active Tickets
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\nActive Tickets");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-', 82));
            Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "Performance Name", "Date", "Time",
                "Hall"));
            Console.WriteLine(new string('-', 82));

            foreach (Ticket ticket in TicketsList[0])
            {
                string performanceName = App.Performances[ticket.PerformanceId].Name;
                if (performanceName.Length > 19) performanceName = $"{performanceName.Substring(0, 16)}...";
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|",
                    performanceName, ticket.Date, ticket.Time, ticket.Hall));
                Console.WriteLine(String.Format("|{0,-19}|{1,-19}|{2,-19}|{3,-19}|", "", "", "", ""));
                Console.WriteLine(new string('-', 82));
            }
            int IndexNumber = 1;
            Console.WriteLine("\n\nIs there a ticket you want to cancel?");
            foreach (Ticket ticket in TicketsList[0]) // this part writes all the the current active tickets underneath eachother as options for the user
            {
                Console.WriteLine($"{IndexNumber++}. {App.Performances[ticket.PerformanceId].Name} ");
            }
            Console.WriteLine("Q. I don't want to cancel any tickets");
            string? userInput = Console.ReadLine();
            if (userInput == null) return;
            if (userInput.ToLower() == "q") return;
            // this upcoming part checks the input from the user. if they choose one of the active tickets it gets canceled (IsActive = false), except if the play is tomorrow 
            try {
                if (!Int32.TryParse(userInput, out int IndexInt)) return; 
                int ReturnIndex = IndexInt - 1;
                foreach (UserTicket userTicket in App.Tickets){
                    if (userTicket.Ticket == TicketsList[0][ReturnIndex]){
                        if (!MainTicketSystem.CancellationIsNotOneDayBefore(userTicket)){
                            Console.WriteLine("Can't refund ticket because the performance is tomorrow");
                            return;
                        }
                        MainTicketSystem.CancelTicketLogic(userTicket);
                    }
                }
                Console.WriteLine($"Refunded {App.Performances[TicketsList[0][ReturnIndex].PerformanceId].Name}");
                Thread.Sleep(2500);
            } catch (ArgumentOutOfRangeException){
                return;
            }

        }
    }

    public static void PrintTicket(Ticket ticket){
        Console.Clear();
        Console.WriteLine("Just booked:");
        Console.WriteLine(ticket.TicketInfo());
        Thread.Sleep(6000);
    }
}
