using System.Text.Json.Serialization;

public class Play
{
	[JsonPropertyName("PlayName")]
    public string ID {get; set;}

    [JsonPropertyName("PlayName")]
	public string PlayName {get; set;}
    [JsonPropertyName("Genres")]
    public list<string> Genre {get; set;}
    [JsonPropertyName("Active")]
    public bool Active {get; set;}

    public Play(string playname, list<string> genre, bool active)
    {
        PlayName = playname;
        Genre = genre;
        Active = active;
    }
}
