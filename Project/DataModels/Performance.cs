using System.Text.Json.Serialization;

public class Performance : IEditable
{
    [Editable]
    [JsonPropertyName("Name")]
	public string Name {get; set;}

    [Editable]
    [JsonPropertyName("Genres")]
    public List<string> Genres {get; set;}

    [Editable]
    [JsonPropertyName("Active")]
    public bool Active {get; set;}

    public Performance(string Name, List<string> Genres, bool Active)
    {
        this.Name = Name;
        this.Genres = Genres;
        this.Active = Active;
    }
}
