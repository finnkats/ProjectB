using System.IO;
using System.Text.Json;
public static class PlayReader {
    // Function to read JSON data
    public static List<MovieViewing> ReadMovieOptionsFromJson(string jsonFilePath, string playID)
    {   
        if (string.IsNullOrEmpty(jsonFilePath)) jsonFilePath = "DataSources/plays.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        var PlayOptions = JsonSerializer.Deserialize<Dictionary<string, List<MovieViewing>>>(jsonData) ?? new Dictionary<string, List<MovieViewing>>();

        if (PlayOptions.ContainsKey(playID)){
            return PlayOptions[playID];
        } else {
            return new List<MovieViewing>();
        }
    }
}
