using System.Text.Json.Serialization;

public class Location : IDataAccessItem, IEditable
{
    [Editable]
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [Editable]
    [JsonPropertyName("Halls")]
    public List<string> Halls {get; set; }

    public Location(string name, List<string> halls){
        Name = name;
        Halls = halls;
    }
}
