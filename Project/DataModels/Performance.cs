using System.Text.Json.Serialization;

public class Performance
{
    [JsonPropertyName("PlayName")]
	public string PlayName {get; set;}
    [JsonPropertyName("Genres")]
    public list<string> Genre {get; set;}
    [JsonPropertyName("Active")]
    public bool Active {get; set;}

    public Performance(string playname, list<string> genre, bool active)
    {
        PlayName = playname;
        Genre = genre;
        Active = active;
    }
}
