using System.Text.Json.Serialization;

public class Genre : IEditable 
{
    [JsonPropertyName("Name")]
    public string Name {get; set;}
    [JsonPropertyName("Age")]
    public int Age {get; set;}
    [JsonIgnore]
    public static List<int> Ages = new(){0, 6, 9, 13, 17};
    public Genre(string name, int age){
        Name = name;
        Age = age;
    }

    public static string AgeString(){
        string ageString = "'0' for everyone\n";
        foreach (int age in Ages.Skip(1)){
            ageString += $"'{age}' for {age}+\n";
        }
        return ageString;
    }
}
