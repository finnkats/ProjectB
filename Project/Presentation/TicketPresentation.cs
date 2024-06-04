using System.Security.Authentication.ExtendedProtection;
using System.Threading;

public static class TicketPresentation{
    public static void TicketMenu()
    {
        while (true){
            Console.Clear();
            var TicketsList = MainTicketSystem.SortActiveTicket(); // Method sorts tickets owned by user by if they are active or not
            Console.WriteLine("Front Page -> Home Page -> View Orders\n\n");
            string[] Headers = new string[]{"Performance", "Order Info", "Date", "Time", "Location"};
            int[] HeaderSize = new int[]{19, "Number of Seat(s): 99 ".Length, 11, 16, 35};
            string barrier = new String('-', HeaderSize.Sum() + HeaderSize.Length + 1);
            string format = $"|{{0,-{HeaderSize[0]}}}|{{1,-{HeaderSize[1]}}}|{{2,-{HeaderSize[2]}}}|{{3,-{HeaderSize[3]}}}|{{4,-{HeaderSize[4]}}}|";

            for (int ticketListIndex = 1; ticketListIndex >= 0; ticketListIndex--){
                string title = ticketListIndex == 1 ? "Previous Orders" : "Current Orders";
                Console.ForegroundColor = ticketListIndex == 1 ? ConsoleColor.Red : ConsoleColor.Green;
                Console.WriteLine(title);
                Console.ResetColor();

                Console.WriteLine(barrier);
                Console.WriteLine(String.Format(format, Headers[0], Headers[1], Headers[2], Headers[3], Headers[4]));

                Console.WriteLine(barrier);
                foreach (Ticket ticket in TicketsList[ticketListIndex]){
                    string name = App.Performances[ticket.PerformanceId].Name;
                    if (name.Length > HeaderSize[0]) name = $"{name.Substring(0, HeaderSize[0] - 3)}...";
                    string order = $"Order Number: {ticket.OrderNumber}";
                    string nSeats = $"Number of Seat(s): {ticket.SeatNumbers.Length}";
                    string date = ticket.Date;
                    string time = $"{ticket.Time} ({App.Performances[ticket.PerformanceId].RuntimeInMin} min)";
                    string location = $"{App.Locations[App.Halls[ticket.Hall].LocationId].Name} ({App.Halls[ticket.Hall].Name})";
                    if (location.Length > HeaderSize[4]) location = $"{location.Substring(0, HeaderSize[4] - 4)}...)";
                    Console.WriteLine(String.Format(format, name, order, date, time, location));
                    Console.WriteLine(String.Format(format, "", nSeats, "", "", ""));
                    Console.WriteLine(barrier);
                }

                Console.WriteLine("\n\n");
            }

            Console.WriteLine("Is there an order you want to cancel?");
            int IndexNumber = 1;
            foreach (Ticket ticket in TicketsList[0]) // this part writes all the the current active tickets underneath eachother as options for the user
            {
                string sep = ", ";
                Console.WriteLine($"{IndexNumber++}: Order: {ticket.OrderNumber} | Seat(s): {String.Join(sep, ticket.SeatNumbers)} | ({App.Performances[ticket.PerformanceId].Name})");
            }
            Console.Write("E. Exit\n\n> ");
            string? userInput = Console.ReadLine();
            if (userInput == null) return;
            if (userInput.ToLower() == "e") return;
            // this upcoming part checks the input from the user. if they choose one of the active tickets it gets canceled (IsActive = false), except if the play is tomorrow 
            try {
                if (!Int32.TryParse(userInput, out int IndexInt)) {
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(1000);
                    continue;
                };
                if (TicketsList[0].Count == 0) {
                    Console.WriteLine("No tickets available to cancel.");
                    Console.WriteLine("Leaving menu...");
                    Thread.Sleep(3000);
                    return;
                }
                int orderNumber = -1;
                int ReturnIndex = IndexInt - 1;
                foreach (Ticket ticket in App.Tickets[App.LoggedInUsername]){
                    if (ticket != TicketsList[0][ReturnIndex]) continue;
                    if (!MainTicketSystem.CancellationIsNotOneDayBefore(ticket)) break;
                    orderNumber = ticket.OrderNumber;
                    MainTicketSystem.CancelTicketLogic(ticket);
                    break;
                }
                if (orderNumber != -1) Console.WriteLine($"Refunded Order: {orderNumber} | {App.Performances[TicketsList[0][ReturnIndex].PerformanceId].Name}");
                else Console.WriteLine("Can't refund order because the performance is tomorrow");
                Thread.Sleep(2500);
            } catch (ArgumentOutOfRangeException){
                Console.WriteLine("Invalid input");
                Thread.Sleep(1000);
                continue;
            }
        }
    }

    public static void PrintTicket(Ticket ticket, string performanceId){
        Console.Clear();
        Console.WriteLine($"Front Page -> Home Page -> View Performances -> {App.Performances[performanceId].Name} -> Booking Message\n");
        Console.WriteLine("Just booked:");
        Console.WriteLine(ticket.TicketInfo());
        Thread.Sleep(6000 + 500 * ticket.SeatNumbers.Length);
    }
}
