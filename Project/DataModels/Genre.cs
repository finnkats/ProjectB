using System.Text.Json.Serialization;

public class Genre : IHasName 
{
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Age")]
    public int Age {get; set;}
    public Genre(string name, int age){
        Name = name;
        Age = age;
    }
}
