using System.Text.Json.Serialization;

public class Genre {
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Genre")]
    public int Age {get; set;}
    public Genre(string name, int age){
        Name = name;
        Age = age;
    }
}
