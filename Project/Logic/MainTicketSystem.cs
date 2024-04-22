using Logic;
using System.Collections.Generic;

public static class MainTicketSystem
{
    // public static Tuple<bool,string,string>? IsTesting {get; set;}
    public static (bool, string, string)? IsTesting { get; set; }

    public static void CreateBookTicket(string performanceId, string date, string time, string room)
    {
        Ticket createNewTicket = new Ticket(performanceId, date, time, room, true);
        TicketPresentation.PrintTicket(createNewTicket);
        createNewTicket.UpdateData();
    }

    public static void ShowTicketInfo()
    {
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
    }

    public static (bool, string, string) LoginCheckAdmin()
    {
        string loginName, loginPassword;
        (loginName, loginPassword) = (IsTesting != null && IsTesting.Value.Item1)
            ? (IsTesting.Value.Item2, IsTesting.Value.Item3)
            : AccountPresentation.GetLoginDetails();
        // Check if admin here instead of checking it the method from AccountLogic
        foreach (var account in App.Accounts.Values)
        {
            if (!AccountLogic.CheckLogin(loginName, loginPassword, account))
            {
                continue;
            }

            if (account.IsAdmin)
            {
                return (true, loginName, loginPassword);
            }
        }

        return (false, loginName, loginPassword);
    }

    public static List<List<Ticket>> SortActiveTicket()
    {
        List<Ticket> ActiveTickets = new();
        List<Ticket> InactiveTickets = new();
        List <List< Ticket>> ReturnLists = new();


        if (App.Tickets != null)
        {
            foreach (UserTicket ticketPair in App.Tickets)
            {
                if (ticketPair.Ticket.IsActive == true)
                {
                    ActiveTickets.Add(ticketPair.Ticket);
                }
                else
                {
                    InactiveTickets.Add(ticketPair.Ticket);
                }
            }
        }
        ReturnLists.Add(ActiveTickets);
        ReturnLists.Add(InactiveTickets);
        return ReturnLists;
    }
}