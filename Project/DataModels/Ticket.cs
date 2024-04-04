using System.Text.Json.Serialization;

public class Ticket{
    [JsonPropertyName("PerformanceId")]
    public string PerformanceId {get; set;}
    [JsonPropertyName("Date")]
    public string Date {get; set;}
    [JsonPropertyName("Time")]
    public string Time {get; set;}
    [JsonPropertyName("Hall")]
    public string Hall {get; set;}

    public Ticket(string PerformanceId, string Date, string Time, string Hall){
        this.PerformanceId = PerformanceId;
        this.Date = Date;
        this.Time = Time;
        this.Hall = Hall;
    }

    public void UpdateData(){
        TicketDataAccess.UpdateTickets(this);
    }

    public string TicketInfo() {
        return $"The play you booked: {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | {this.Hall}";
    }  
}