using System.Text.Json;

public static class HallDataAccess{
    // Dictionary where the key is a string of ID ("ID0") and its value is a Hall object
    // and puts it into App.Halls
    public static Dictionary<string, Hall> ReadHalls(){
        string jsonData = File.ReadAllText(@"DataSources/halls.json");
        var Halls = JsonSerializer.Deserialize<Dictionary<string, Hall>>(jsonData) ?? new Dictionary<string, Hall>();
        return Halls;
    }

    // Updates halls.json with App.Halls
    public static void UpdateHalls(){
        var HallsJson = JsonSerializer.Serialize(App.Halls);
        File.WriteAllText(@"DataSources/halls.json", HallsJson);
    }
}
