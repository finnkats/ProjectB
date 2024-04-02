using System.IO;
using System.Text.Json;
public static class PlayReader {
    // Function to read JSON data
    public static List<Play> ReadMovieOptionsFromJson(string jsonFilePath, string playID)
    {   
        if (string.IsNullOrEmpty(jsonFilePath)) jsonFilePath = "DataSources/plays.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        var PlayOptions = JsonSerializer.Deserialize<Dictionary<string, List<Play>>>(jsonData) ?? new Dictionary<string, List<Play>>();

        if (PlayOptions.ContainsKey(playID)){
            return PlayOptions[playID];
        } else {
            return new List<Play>();
        }
    }
}
