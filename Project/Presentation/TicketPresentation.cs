using System.Security.Authentication.ExtendedProtection;
using System.Threading;

public static class TicketPresentation{
    public static void PrintTickets(){
        bool hasTickets = false;
        while (true){
            Console.Clear();
            if(App.Tickets.Count != 0){
                foreach(UserTicket ticketPair in App.Tickets){
                    if (ticketPair.User == App.LoggedInUsername){
                        Console.WriteLine(ticketPair.Ticket.TicketInfo());
                        hasTickets = true;
                    }
                }
                if(!hasTickets){
                    if(NoBooksMenu()) break;
                    else{Console.WriteLine("Not possible option");}
                }
                Console.WriteLine("1: Edit Ticket");
                Console.Write("2: To Exit\n>");
            }
            else{
                if(NoBooksMenu()) break;
                else{Console.WriteLine("Not possible option");}
            }
            string? inputChoice = Console.ReadLine();
            if (inputChoice == "1"){
                Console.Clear();
                Console.WriteLine("1: Cancel Tickets");
                Console.Write("2: Exit\n>");
                string? inputChoice2 = Console.ReadLine();
                if(inputChoice2 == "1"){
                    CancelTicket();
                }
                else if(inputChoice2 == "2") continue;
                else{
                    Console.WriteLine("Not possible choice");
                }
            }
            else if(inputChoice == "2") return;
        }
    }
    
    public static void PrintTicket(Ticket ticket){

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

    public static void CancelTicket(){
        Console.Clear();
        List<UserTicket> filteredTickets = new List<UserTicket>();
        int i = 1;
        int j = 1;
        foreach (UserTicket userTicket in App.Tickets){
            if (userTicket.User == App.LoggedInUsername){
                // This i++ works by using the int i first before incrementing
                if(userTicket.Ticket.IsActive){
                    Console.WriteLine($"{i++}: {userTicket.Ticket.TicketInfo()}");
                    filteredTickets.Add(userTicket);
                }
            }
        }
        if(filteredTickets.Count != 0){Console.WriteLine();}
        foreach (UserTicket userTicket in App.Tickets){
            if (userTicket.User == App.LoggedInUsername){
                // This i++ works by using the int i first before incrementing
                if(!userTicket.Ticket.IsActive){
                    if(j == 1)Console.WriteLine("Ticket History:");
                    Console.WriteLine($"{j++}: {userTicket.Ticket.TicketInfo()}");
                }
            }
        }

        while (true) {
            if(filteredTickets.Count == 0){Console.WriteLine("\nNo tickets available to cancel, please enter 'exit' to go back.");}
            else{Console.Write("\nPlease select the ticket you want to cancel (or type 'exit' to end):\n>");}
            string? input = Console.ReadLine();
            if(input == null) return; //For now
            if(input.ToLower() == "exit"){
                Console.WriteLine("Process stopped");
                Thread.Sleep(1500);
                break;
            }
            if (int.TryParse(input, out int index) && index >= 1 && index <= filteredTickets.Count){
                if(MainTicketSystem.CancellationIsNotOneDayBefore(filteredTickets[index-1])){
                    Console.WriteLine("Are you sure you want to cancel this ticket? (y/n)");
                    string? confirmation = Console.ReadLine();
                    if(confirmation == null)Console.WriteLine("Please enter again");
                    if (confirmation!.ToLower() == "y") {
                        UserTicket ticketToCancel = filteredTickets[index-1];
                        MainTicketSystem.CancelTicketLogic(ticketToCancel);
                        Console.WriteLine("Ticket cancelled successfully :)");
                        Console.WriteLine("Your money will be returned in a few work days");
                        Thread.Sleep(1500);
                        break;
                    } else if (confirmation.ToLower() == "n") {
                        Console.WriteLine("Stopped the process...");
                        Thread.Sleep(1500);
                        break;
                    } else {
                        Console.WriteLine("Invalid option. Please try again");
                    }
                }
                else{
                    Console.WriteLine("Cannot cancel the ticket because the performance is tommorow");
                    Thread.Sleep(2000);
                }
            } else {
                Console.WriteLine("Invalid input. Please try agian.");
            }
        }
    }

    public static bool NoBooksMenu(){
        Console.WriteLine("No tickets booked");
        Console.Write("1: To Exit\n>");
        string? exitChoice = Console.ReadLine();
        return exitChoice == "1";
    }
}
