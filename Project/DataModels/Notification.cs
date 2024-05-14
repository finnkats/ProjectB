using System.Text.Json.Serialization;

public class Notification : IDataAccessList
{
    [JsonPropertyName("Performance")]
    public string Performance { get; set; }
    [JsonPropertyName("Location")]
    public string Location { get; set; }
    [JsonPropertyName("Time")]
    public string Time { get; set;}
    public Notification(string performance, string location){
        Performance = performance;
        Location = location;
        Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }

    public override string ToString(){
        if (Performance == null) return "";
        if (Location == null) return "";
        if (Time == null) return "";
        string text = $"{App.Performances[Performance].Name} has been added in {App.Locations[Location]} ({Time})";
        return text;
    }
}
