using System.Text.Json.Serialization;

public class UserTicket{
    [JsonPropertyName("User")]
    public string User {get; set;}
    [JsonPropertyName("Ticket")]
    public Ticket Ticket {get; set;}
    public UserTicket(string user, Ticket ticket){
        this.User = user;
        this.Ticket = ticket;
    }
}