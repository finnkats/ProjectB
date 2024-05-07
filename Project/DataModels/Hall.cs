using System.Text.Json.Serialization;
public class Hall {
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Seats")]
    public int Seats { get; set; }
    [JsonPropertyName("LocationId")]
    public string LocationId { get; set; }
    
    public Hall(string name, int seats, string locationId){
        Name = name;
        Seats = seats;
        LocationId = locationId;
    }
}
