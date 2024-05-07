using System.Text.Json.Serialization;
public class Hall : IEditable
{
    [Editable]
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [Editable]
    [JsonPropertyName("Seats")]
    public int Seats { get; set; }

    [Editable]
    [JsonPropertyName("LocationId")]
    public string LocationId { get; set; }
    
    public Hall(string name, int seats, string locationId){
        Name = name;
        Seats = seats;
        LocationId = locationId;
    }
}
