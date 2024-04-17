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
            Console.WriteLine("1: Edit Ticket");
            Console.WriteLine("2: To Exit");
            string? inputChoice = Console.ReadLine();
            if (inputChoice == "1"){
                Console.Clear();
                Console.WriteLine("1: Cancel Tickets");
                Console.WriteLine("2: Exit");
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
        Console.Clear();
        Console.WriteLine("Just booked:");
        Console.WriteLine(ticket.TicketInfo());
        Thread.Sleep(6000);
    }

    public static void CancelTicket(){
        Console.Clear();
        List<UserTicket> filteredTickets = new List<UserTicket>();
        int i = 1;
        foreach (UserTicket userTicket in App.Tickets){
            if (userTicket.User == App.LoggedInUsername){
                // This i++ works by using the int i first before incrementing
                Console.WriteLine($"{i++}: {userTicket.Ticket.TicketInfo()}");
                filteredTickets.Add(userTicket);
            }
        }

        while (true) {
            Console.Write("Please select the ticket you want to cancel:\n>");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int index) && index >= 1 && index-1 < filteredTickets.Count) {
                Console.WriteLine("Are you sure you want to cancel this ticket? (y/n)");
                string? confirmation = Console.ReadLine();
                if (confirmation.ToLower() == "y") {
                    UserTicket ticketToCancel = filteredTickets[index-1];
                    filteredTickets.RemoveAt(index-1);
                    App.Tickets.Remove(ticketToCancel);
                    Console.WriteLine("Ticket cancelled successfully :)");
                    Thread.Sleep(1500);
                    break;
                } else if (confirmation.ToLower() == "n") {
                    Console.WriteLine("Stopped the process...");
                    Thread.Sleep(1500);
                    break;
                } else {
                    Console.WriteLine("Invalid option. Please try again");
                }
            } else {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }

    // public static List<UserTicket> SortList(){
    //     List<UserTicket> filteredTickets = new List<UserTicket>();
    //     foreach (UserTicket ticket in App.Tickets){
    //         if (ticket.User == App.LoggedInUsername){
    //             filteredTickets.Add(ticket);
    //         }
    //     }
    //     return filteredTickets;
    // }
}
