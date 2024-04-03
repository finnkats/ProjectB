using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Account
{
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Password")]
    public string Password {get; set;}
    [JsonPropertyName("IsAdmin")]
    public bool IsAdmin {get; set;}
    
    public Account(string name, string password, bool isAdmin){
        Name = name;
        Password = password;
        IsAdmin = isAdmin;
    }
}
