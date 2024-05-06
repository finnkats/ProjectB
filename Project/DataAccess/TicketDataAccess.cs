using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class TicketDataAccess{
    public static Dictionary<string,List<Ticket>> ReadTickets(){
        // List<UserTicket> ticketCollection = new(); before
        Dictionary<string,List<Ticket>> ticketCollection = new();
        string JsonPath = @"DataSources/tickets.json";

        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            ticketCollection = JsonSerializer.Deserialize<Dictionary<string, List<Ticket>>>(ticketsJson)!;
            reader.Close();
        }
        return ticketCollection;
    }

    // public static void UpdateTickets(Ticket newTicket){
    //     UserTicket customDict = new UserTicket(App.LoggedInUsername, newTicket);
    //     App.Tickets.Add(customDict);

    //     string JsonPath = @"DataSources/tickets.json";
    //     StreamWriter writer = new(JsonPath);
    //     string listJson = JsonSerializer.Serialize(App.Tickets);
    //     writer.Write(listJson);
    //     writer.Close();
    // }

    public static void AddItemDictionary<TKey, Tvalue>(Dictionary<TKey, List<Tvalue>> dict, TKey key, Tvalue value) where TKey:notnull{
        if(!dict.ContainsKey(key)){
            dict[key] = new List<Tvalue>();
        }
        dict[key].Add(value);
        TicketDataAccess.UpdateTickets();
    }

    // public static List<Tvalue> ReturnDictValueList<TKey, Tvalue>(Dictionary<TKey, List<Tvalue>> dict) where TKey:notnull{
    //     foreach(KeyValuePair<TKey, List<Tvalue>> dictCollection in dict){
    //         // code
    //     }
    // }

    public static void UpdateTickets(){
        string JsonPath = @"DataSources/tickets.json";
        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(App.Tickets);
        writer.Write(listJson);
        writer.Close();
    }
}
