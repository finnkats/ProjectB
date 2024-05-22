using System.Text.Json.Serialization;

public class Ticket : IEquatable<Ticket>, IDataAccessList {
    [JsonPropertyName("PerformanceId")]
    public string PerformanceId {get; set;}

    [JsonPropertyName("Date")]
    public string Date {get; set;}

    [JsonPropertyName("Time")]
    public string Time {get; set;}

    [JsonPropertyName("Hall")]
    public string Hall {get; set;}

    [JsonPropertyName("SeatNumber")]
    public int SeatNumber { get; set; }

    [JsonPropertyName("Active")]
    public bool IsActive {get; set;}

    public Ticket(string PerformanceId, string Date, string Time, string Hall, int SeatNumber, bool IsActive){
        this.PerformanceId = PerformanceId;
        this.Date = Date;
        this.Time = Time;
        this.Hall = Hall;
        this.SeatNumber = SeatNumber;
        this.IsActive = IsActive;
    }

    public string TicketInfo() {
        if(IsActive){
            return $"The play you booked: {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name} | Seat {this.SeatNumber}.";
        }
        else{
            return $"The play you had booked was (is now cancelled): {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name} | Seat {this.SeatNumber}.";
        }
    }

    public bool Equals(Ticket? otherTicket){
        if(otherTicket == null){return false;}
        return this.PerformanceId == otherTicket.PerformanceId &&
           this.Date == otherTicket.Date &&
           this.Time == otherTicket.Time &&
           this.Hall == otherTicket.Hall &&
           this.IsActive == otherTicket.IsActive;
    }
    public override bool Equals(object? obj){
        if(obj == null){return false;}
        if (obj is Ticket){
            return Equals(obj as Ticket);
        }
        else{
            return false;
        }
    }

    public override int GetHashCode(){
        return HashCode.Combine(PerformanceId, Date, Time, Hall, IsActive);
    }
}
