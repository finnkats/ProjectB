using System.Text.Json;

public static class LocationDataAccess{
    // Dictionary where the key is a string of ID ("ID0") and its value is a Location object
    // and puts it into App.Locations
    public static Dictionary<string, Location> ReadLocations(){
        string jsonData = File.ReadAllText(@"DataSources/locations.json");
        var Locations = JsonSerializer.Deserialize<Dictionary<string, Location>>(jsonData) ?? new Dictionary<string, Location>();
        return Locations;
    }

    // Updates locations.json with App.Locations
    public static void UpdateLocations(){
        var LocationsJson = JsonSerializer.Serialize(App.Locations);
        File.WriteAllText(@"DataSources/locations.json", LocationsJson);
    }
}
