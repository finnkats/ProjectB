using System.Text.Json.Serialization;
public class Play {
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Time")]
    public string Time { get; set; }
    [JsonPropertyName("Date")]
    public string Date { get; set; }
    [JsonPropertyName("Hall")]
    public string Hall { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    public Play(string location, string time, string date, string hall, string name){
        this.Location = location;
        this.Time = time;
        this.Date = date;
        this.Hall = hall;
        this.Name = name;
    }
}
