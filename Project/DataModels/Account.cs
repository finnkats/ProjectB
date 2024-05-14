using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Account : IDataAccessItem
{
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Password")]
    public string Password {get; set;}
    [JsonPropertyName("IsAdmin")]
    public bool IsAdmin {get; set;}
    [JsonPropertyName("Location")]
    public string Location;
    [JsonPropertyName("Genres")]
    public List<string> Genres;
    
    public Account(string name, string password, bool isAdmin){
        Name = name;
        Password = password;
        IsAdmin = isAdmin;
        Location = "null";
        Genres = new List<string>();
    }
}
