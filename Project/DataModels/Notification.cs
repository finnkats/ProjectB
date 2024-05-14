using System.Text.Json.Serialization;

public class Notification : IDataAccessList
{
    [JsonPropertyName("Text")]
    public string Text { get; set; }
    [JsonPropertyName("Time")]
    public string Time { get; set;}
    public Notification(string text){
        Text = text;
        Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    }
}
