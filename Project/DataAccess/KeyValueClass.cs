using System.Text.Json.Serialization;

public class KeyValueClass{
    [JsonPropertyName("User")]
    public string User {get; set;}
    [JsonPropertyName("Ticket")]
    public Ticket Ticket {get; set;}
    public KeyValueClass(string user, Ticket ticket){
        this.User = user;
        this.Ticket = ticket;
    }
}