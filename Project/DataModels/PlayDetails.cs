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
        Location = location;
        Time = time;
        Room = room;
        Date = date;
        PlayName = playname;
    }
}
