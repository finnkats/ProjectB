using System.Text.Json;
using System.Text.Json.Serialization;

public static class UpdateTicketJson{
    public static void UpdateJsonFile(Ticket newTicket){
        List<UserTicket> ticketsCollection;
        string JsonPath = @"DataSources/tickets.json";

        // Check if file exist and has data inside it
        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            ticketsCollection = JsonSerializer.Deserialize<List<UserTicket>>(ticketsJson)!;
            reader.Close();
        }
        else{
            ticketsCollection = new List<UserTicket>();
        }

        // int ticketID = NewTicketID();
        UserTicket customDict = new UserTicket(App.LoggedInUsername, newTicket);
        ticketsCollection.Add(customDict);

        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(ticketsCollection);
        writer.Write(listJson);
        writer.Close();
    }

// Creates new ID's for new Movies
    // private static int NewTicketID(){
    //     UniqueTicketID++;
    //     return UniqueTicketID;
    // }
}