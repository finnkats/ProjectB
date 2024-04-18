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
    [JsonPropertyName("Active")]
    public bool IsActive {get; set;}

    public Ticket(string PerformanceId, string Date, string Time, string Hall, bool isActive){
        this.PerformanceId = PerformanceId;
        this.Date = Date;
        this.Time = Time;
        this.Hall = Hall;
        this.IsActive = isActive;
    }

    public void UpdateData(){
        TicketDataAccess.UpdateTickets(this);
    }

    public string TicketInfo() {
        if(IsActive){
            return $"The play you booked: {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name}.";
        }
        else{
            return $"The play you had booked was (is now cancelled): {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name}.";
        }
    }  
}
