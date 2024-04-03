using System.Text.Json;
using System.Text.Json.Serialization;

public static class ReadTicketJson{
    // Returns all tickets from JsonPath into a list
    public static List<UserTicket>? ReadTickets(){
        List<UserTicket> ticketCollection;
        string JsonPath = @"DataSources/tickets.json";

        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            ticketCollection = JsonSerializer.Deserialize<List<UserTicket>>(ticketsJson)!;
            reader.Close();
            return ticketCollection;
        }
        else{
            return null;
        }
    }
}