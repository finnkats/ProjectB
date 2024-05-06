using System.IO;
using System.Text.Json;
public static class PlayDataAccess {
    // Dictionary where the key is a string of ID ("ID0") and the ID is related to a Performance ID
    // its value is an List of Play objects
    // and puts it into App.Plays
    public static Dictionary<string, List<Play>> ReadPlays(){
        string jsonData = File.ReadAllText(@"DataSources/plays.json");
        var Plays = JsonSerializer.Deserialize<Dictionary<string, List<Play>>>(jsonData) ?? new Dictionary<string, List<Play>>();
        return Plays;
    }

    // Updates Plays.json with App.Plays
    public static void UpdatePlays(){
        var JsonString = JsonSerializer.Serialize(App.Plays);
        File.WriteAllText(@"DataSources/plays.json", JsonString);
    }
}
