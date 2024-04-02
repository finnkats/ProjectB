using System.Text.Json;
using System.Text.Json.Serialization;

public static class ReadTicketJson{
    
    public static string? JsonTestPath = null;
    // Checks if the unittest has set a path, if not (null) it will use the program's json file
    private static string JsonPath {
        get => string.IsNullOrEmpty(JsonTestPath) ? "./DataSources/tickets.json" : JsonTestPath;
    }

    // Returns all tickets from JsonPath into a list
    public static List<UserTicket>? ReadTickets(){
        List<UserTicket> ticketCollection;

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
