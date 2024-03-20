using System.Text.Json;
using System.Text.Json.Serialization;

public static class UpdateTicketJson{
    private static int UniqueTicketID = 0;
    public static string? JsonTestPath = null;
    private static string JsonPath {
        get => string.IsNullOrEmpty(JsonTestPath) ? "../../DataSources/tickets.json" : JsonTestPath;
    }
    public static void UpdateJsonFile(Ticket newTicket){
        // Currently the json file needs to have some data inside it
        List<KeyValueClass> ticketsCollection;
        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            // List<Ticket> ticketsCollection = JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson)!;
            ticketsCollection = JsonSerializer.Deserialize<List<KeyValueClass>>(ticketsJson)!;
            reader.Close();
        }
        else{
            ticketsCollection = new List<KeyValueClass>();
        }
        // catch(Exception ex){
        //     Console.WriteLine(ex.Message);
        // }

        int ticketID = NewTicketID();
        KeyValueClass customDict = new KeyValueClass(ticketID, newTicket);
        ticketsCollection.Add(customDict);

        StreamWriter writer = new(JsonPath);
        string listJson = JsonSerializer.Serialize(ticketsCollection);
        writer.Write(listJson);
        writer.Close();
    }

    private static int NewTicketID(){
        UniqueTicketID++;
        return UniqueTicketID;
    }
}