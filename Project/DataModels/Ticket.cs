using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

public class KeyValueClass{
    public int ID {get; set;}
    public Ticket Ticket {get; set;}
    public KeyValueClass(int id, Ticket ticket){
        this.ID = id;
        this.Ticket = ticket;
    }
}

public class Ticket{
    public string MovieName {get; set;}
    public string Date {get; set;}
    public string Time {get; set;}
    public string Room {get; set;}
    private static int UniqueTicketID = 1;
    public static string? JsonTestPath = null;
    private static string JsonPath {
        get => string.IsNullOrEmpty(JsonTestPath) ? "../../DataSources/tickets.json" : JsonTestPath;
    }

    public Ticket(string movieName, string date, string time, string roomChar){
        this.MovieName = movieName;
        this.Date = date;
        this.Time = time;
        this.Room = roomChar;
        UpdateJsonFile();
    }

    protected void UpdateJsonFile(){
        // Currently the json file needs to have some data inside it
        List<KeyValueClass> ticketsCollection;
        if(File.Exists(JsonPath) && !string.IsNullOrEmpty(File.ReadAllText(JsonPath))){
            StreamReader reader = new(JsonPath);
            string ticketsJson = reader.ReadToEnd();
            // List<Ticket> ticketsCollection = JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson)!;
            ticketsCollection = JsonConvert.DeserializeObject<List<KeyValueClass>>(ticketsJson)!;
            reader.Close();
        }
        else{
            ticketsCollection = new List<KeyValueClass>();
        }
        // catch(Exception ex){
        //     Console.WriteLine(ex.Message);
        // }

        int ticketID = NewTicketID();
        KeyValueClass customDict = new KeyValueClass(ticketID, this);
        ticketsCollection.Add(customDict);

        StreamWriter writer = new(JsonPath);
        string listJson = JsonConvert.SerializeObject(ticketsCollection);
        writer.Write(listJson);
        writer.Close();
    }

    private static int NewTicketID(){
        UniqueTicketID++;
        return UniqueTicketID;
    }
}