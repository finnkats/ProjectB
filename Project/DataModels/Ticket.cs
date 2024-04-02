using System.Text.Json.Serialization;

public class Ticket{
    [JsonPropertyName("Performance")]
    public string Performance {get; set;}
    [JsonPropertyName("Date")]
    public string Date {get; set;}
    [JsonPropertyName("Time")]
    public string Time {get; set;}
    [JsonPropertyName("Hall")]
    public string Hall {get; set;}

    public Ticket(string Performance, string Date, string Time, string Hall){
        this.Performance = Performance;
        this.Date = Date;
        this.Time = Time;
        this.Hall = Hall;
    }

    public void UpdateData(){
        UpdateTicketJson.UpdateJsonFile(this);
    }

    public string TicketInfo() => $"The play you booked: {this.Performance}. On {this.Date} at {this.Time} | {this.Hall}";
}