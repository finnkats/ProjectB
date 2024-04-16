using System.Text.Json;

public static class HallDataAccess{
    public static Dictionary<string, Hall> ReadHalls(){
        string jsonData = File.ReadAllText(@"DataSources/halls.json");
        var Halls = JsonSerializer.Deserialize<Dictionary<string, Hall>>(jsonData) ?? new Dictionary<string, Hall>();
        return Halls;
    }

    public static void UpdateHalls(){
        var HallsJson = JsonSerializer.Serialize(App.Halls);
        File.WriteAllText(@"DataSources/halls.json", HallsJson);
    }
}
