using System.Text.Json.Serialization;

public class KeyValueClass{
    [JsonPropertyName("ID")]
    public int ID {get; set;}
    [JsonPropertyName("Ticket")]
    public Ticket Ticket {get; set;}
    public KeyValueClass(int id, Ticket ticket){
        this.ID = id;
        this.Ticket = ticket;
    }
}