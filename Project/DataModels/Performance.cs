using System.Text.Json.Serialization;

public class Performance : IHasName
{
    [JsonPropertyName("Name")]
	public string Name {get; set;}
    [JsonPropertyName("Genres")]
    public List<string> Genres {get; set;}
    [JsonPropertyName("Active")]
    public bool Active {get; set;}

    public Performance(string Name, List<string> Genres, bool Active)
    {
        this.Name = Name;
        this.Genres = Genres;
        this.Active = Active;
    }
}
