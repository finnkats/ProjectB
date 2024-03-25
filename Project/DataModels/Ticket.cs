using System.Text.Json.Serialization;

public class Ticket{
    [JsonPropertyName("Movie")]
    public string MovieName {get; set;}
    [JsonPropertyName("Date")]
    public string Date {get; set;}
    [JsonPropertyName("Time")]
    public string Time {get; set;}
    [JsonPropertyName("Room")]
    public string Room {get; set;}

    public Ticket(string MovieName, string Date, string Time, string Room){
        this.MovieName = MovieName;
        this.Date = Date;
        this.Time = Time;
        this.Room = Room;
    }

    public void UpdateData(){
        UpdateTicketJson.UpdateJsonFile(this);
    }

    public string TicketInfo() => $"The play you booked: {this.MovieName}. On {this.Date} at {this.Time} | {this.Room}";
}