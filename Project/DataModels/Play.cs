using System.Text.Json.Serialization;
public class Play : IDataAccessList {
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Time")]
    public string Time { get; set; }
    [JsonPropertyName("Date")]
    public string Date { get; set; }
    [JsonPropertyName("Hall")]
    public string Hall { get; set; }
    [JsonPropertyName("BookedSeats")]
    public int BookedSeats { get; set; }
    [JsonIgnore]
    public string PerformanceId { get; set; }

    public Play(string location, string time, string date, string hall, string performanceId){
        this.Location = location;
        this.Time = time;
        this.Date = date;
        this.Hall = hall;
        this.BookedSeats = 0;
        this.PerformanceId = performanceId;
    }
}
