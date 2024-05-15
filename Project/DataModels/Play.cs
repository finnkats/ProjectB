using System.Text.Json.Serialization;
public class Play : IDataAccessList {
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Start time")]
    public string StartTime { get; set; }
    [JsonIgnore()]
    public string EndTime {
        get{
            DateTime startTime = DateTime.Parse(StartTime);
            int runTimeMin = App.Performances[PerformanceId].RuntimeInMin;
            return startTime.AddMinutes(runTimeMin).ToString("HH:mm:ss");
        }
    }
    [JsonPropertyName("Date")]
    public string Date { get; set; }
    [JsonPropertyName("Hall")]
    public string Hall { get; set; }
    [JsonPropertyName("BookedSeats")]
    public int BookedSeats { get; set; }
    [JsonPropertyName("PerformanceId")]
    // PlayId is performanceId
    public string PerformanceId { get; set; }

    public Play(string location, string startTime, string date, string hall, string performanceId){
        this.Location = location;
        this.StartTime = startTime;
        this.Date = date;
        this.Hall = hall;
        this.BookedSeats = 0;
        this.PerformanceId = performanceId;
    }
}
