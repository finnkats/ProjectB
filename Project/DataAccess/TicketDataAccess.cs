using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// This should be changed like all the other classes
// Make it a dictionary where the key is a username and the value is a list of tickets
// (like how Plays is stored, but instead of ID a Username as key)
// ! This will break alot of other code

public static class TicketDataAccess{
    // List of UserTicket objects (has string: User and Ticket: Ticket fields)
    // and puts it into App.Tickets
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

    // Gets a new UserTicket from a Ticket,
    // adds it to App.Tickets and then updates tickets.json with App.Tickets
    public static void UpdateTickets(Ticket newTicket){
        UserTicket customDict = new UserTicket(App.LoggedInUsername, newTicket);
        App.Tickets.Add(customDict);

        string JsonPath = @"DataSources/tickets.json";
        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(App.Tickets);
        writer.Write(listJson);
        writer.Close();
    }

    // Does the same as above, but only updates the json file
    public static void UpdateTickets(){
        string JsonPath = @"DataSources/tickets.json";
        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(App.Tickets);
        writer.Write(listJson);
        writer.Close();
    }
}
