using System.IO;
using System.Text.Json;
public static class PlayDataAccess {
    public static Dictionary<string, List<Play>> ReadPlays(){
        string jsonData = File.ReadAllText(@"DataSources/plays.json");
        var Plays = JsonSerializer.Deserialize<Dictionary<string, List<Play>>>(jsonData) ?? new Dictionary<string, List<Play>>();
        return Plays;
    }

    public static void WritePlays(Dictionary<string, List<Play>> plays){
        var JsonString = JsonSerializer.Serialize(plays);
        File.WriteAllText(@"DataSources/plays.json", JsonString);
    }

    public static List<Play> GetPlaysFromPresentation(string playID)
    {   
        string jsonData = File.ReadAllText(@"DataSources/plays.json");
        var PlayOptions = JsonSerializer.Deserialize<Dictionary<string, List<Play>>>(jsonData) ?? new Dictionary<string, List<Play>>();

        if (PlayOptions.ContainsKey(playID)){
            return PlayOptions[playID];
        } else {
            return new List<Play>();
        }
    }
}
