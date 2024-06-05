using System.Text.Json.Serialization;

public class Ticket : IEquatable<Ticket>, IDataAccessList {
    // Real value gets calculated in App.Start, after all json tickets have been read and put in App.Tickets
    public static int CurrentOrderNumber = -1;

    [JsonPropertyName("PerformanceId")]
    public string PerformanceId {get; set;}


    [JsonPropertyName("Date")]
    public string Date {get; set;}


    [JsonPropertyName("Time")]
    public string Time {get; set;}


    [JsonPropertyName("Hall")]
    public string Hall {get; set;}


    [JsonPropertyName("SeatNumbers")]
    public int[] SeatNumbers { get; set; }


    [JsonPropertyName("Active")]
    public bool IsActive {get; set;}

    [JsonPropertyName("OrderNumber")]
    public int OrderNumber {get; set;}


    public Ticket(string PerformanceId, string Date, string Time, string Hall, int[] SeatNumbers, bool IsActive, int OrderNumber = -1){
        this.PerformanceId = PerformanceId;
        this.Date = Date;
        this.Time = Time;
        this.Hall = Hall;
        this.SeatNumbers = SeatNumbers;
        this.IsActive = IsActive;
        this.OrderNumber = (OrderNumber == -1) ? CurrentOrderNumber : OrderNumber;
        CurrentOrderNumber++;
    }

    public string TicketInfo() {
        string sep = ", ";
        if(IsActive){
            return $"The play you booked: {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name} | Seat(s) {String.Join(sep, this.SeatNumbers)}.\nYou can view this order in 'View Orders' in the homepage.";
        }
        else{
            return $"The play you had booked was (is now cancelled): {App.Performances[PerformanceId].Name}. On {this.Date} at {this.Time} | " +
                $"{App.Locations[App.Halls[this.Hall].LocationId].Name} - {App.Halls[this.Hall].Name} | Seat(s) {String.Join(sep, this.SeatNumbers)}.";
        }
    }

    public bool Equals(Ticket? otherTicket){
        if(otherTicket == null){return false;}
        return this.PerformanceId == otherTicket.PerformanceId &&
           this.Date == otherTicket.Date &&
           this.Time == otherTicket.Time &&
           this.Hall == otherTicket.Hall &&
           this.IsActive == otherTicket.IsActive &&
           this.SeatNumbers.SequenceEqual(otherTicket.SeatNumbers);
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
        return HashCode.Combine(PerformanceId, Date, Time, Hall, IsActive, SeatNumbers);
    }
}
