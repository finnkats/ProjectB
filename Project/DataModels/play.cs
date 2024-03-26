using System.Text.Json.Serialization;

public class Play
{
    [JsonPropertyName("PlayName")]
	public string PlayName { get; set; }
    [JsonPropertyName("Genres")]
    public list<genres> Genre { get; set; }
    [JsonPropertyName("Active")]
    public string Active { get; set; }

    public Play(string playname, list<genres> genre, bool active)
    {
        PlayName = playname;
        Genre = genre
        Active = active
    }
}

public class PlayDetails
{
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Time")]
    public string Time { get; set; }
    [JsonPropertyName("Room")]
    public string Room { get; set; }
    [JsonPropertyName("Date")]
    public string Date { get; set; }
    [JsonPropertyName("PlayName")]
    public string PlayName { get; set; }

    public PlayDetails(string location, string time, string room, string date, string playname)
    {
        Location = location
        Time = time
        Room = room
        Date = date
        PlayName = playname
    }
}