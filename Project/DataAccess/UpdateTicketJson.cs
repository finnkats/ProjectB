using System.Text.Json;
using System.Text.Json.Serialization;

public static class UpdateTicketJson{
    public static int UniqueTicketID = 0;
    public static string? JsonTestPath = null;
    // Checks if the unittest has set a path, if not (null) it will use the program's json file
    private static string JsonPath {
        get => string.IsNullOrEmpty(JsonTestPath) ? "./DataSources/tickets.json" : JsonTestPath;
    }
    public static void UpdateJsonFile(Ticket newTicket){
        List<KeyValueClass> ticketsCollection;
        // Check if file exist and has data inside it
        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            ticketsCollection = JsonSerializer.Deserialize<List<KeyValueClass>>(ticketsJson)!;
            reader.Close();
        }
        else{
            ticketsCollection = new List<KeyValueClass>();
        }

        int ticketID = NewTicketID();
        KeyValueClass customDict = new KeyValueClass(ticketID, newTicket);
        ticketsCollection.Add(customDict);

        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(ticketsCollection);
        writer.Write(listJson);
        writer.Close();
    }

// Creates new ID's for new Movies
    private static int NewTicketID(){
        UniqueTicketID++;
        return UniqueTicketID;
    }
}