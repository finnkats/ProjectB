using System.Collections.Generic;
using System.Text.Json.Serialization;

public class AccountDataModel
{
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Password")]
    public string Password {get; set;}
    [JsonPropertyName("IsAdmin")]
    public bool IsAdmin {get; set;}
    
    public AccountDataModel(string name, string password, bool isAdmin){
        Name = name;
        Password = password;
        IsAdmin = isAdmin;
    }
}
