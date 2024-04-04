using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class TicketDataAccess{
    public static List<UserTicket> ReadTickets(){
        List<UserTicket> ticketCollection = new();
        string JsonPath = @"DataSources/tickets.json";

        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            ticketCollection = JsonSerializer.Deserialize<List<UserTicket>>(ticketsJson)!;
            reader.Close();
        }
        return ticketCollection;
    }

    public static void UpdateTickets(Ticket newTicket){
        UserTicket customDict = new UserTicket(App.LoggedInUsername, newTicket);
        App.Tickets.Add(customDict);

        string JsonPath = @"DataSources/tickets.json";
        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(App.Tickets);
        writer.Write(listJson);
        writer.Close();
    }
}
