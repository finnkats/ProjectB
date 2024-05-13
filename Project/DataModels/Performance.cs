using System.Text.Json.Serialization;

public class Performance : IDataAccessItem, IEditable
{
    [Editable]
    [JsonPropertyName("Name")]
	public string Name {get; set;}

    [Editable]
    [JsonPropertyName("RuntimeInMin")]
    public int RuntimeInMin {get; set;}

    [Editable]
    [JsonPropertyName("Genres")]
    public List<string> Genres {get; set;}

    [Editable]
    [JsonPropertyName("Active")]
    public bool Active {get; set;}

    [JsonConstructor]
    public Performance(string Name, int runtimeInMin, List<string> Genres, bool Active)
    {
        this.Name = Name;
        this.RuntimeInMin = runtimeInMin;
        this.Genres = Genres;
        this.Active = Active;
    }
}
