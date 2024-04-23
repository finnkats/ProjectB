using System.Text.Json;

public static class LocationDataAccess{
    public static Dictionary<string, Location> ReadLocations(){
        string jsonData = File.ReadAllText(@"DataSources/locations.json");
        var Locations = JsonSerializer.Deserialize<Dictionary<string, Location>>(jsonData) ?? new Dictionary<string, Location>();
        return Locations;
    }

    public static void UpdateLocations(){
        var LocationsJson = JsonSerializer.Serialize(App.Locations);
        File.WriteAllText(@"DataSources/locations.json", LocationsJson);
    }
}
