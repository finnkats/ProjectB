using System.Text.Json.Serialization;
public class Hall : IDataAccessItem, IEditable
{
    [Editable]
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    public int Seats { get => SeatLayout.SeatAmount; }

    [JsonPropertyName("LocationId")]
    public string LocationId { get; set; }
    [JsonPropertyName("SeatLayout")]
    public Layout SeatLayout { get; set;}
    
    public Hall(string name, Layout seatlayout, string locationId){
        Name = name;
        LocationId = locationId;
        SeatLayout = seatlayout;
    }
}