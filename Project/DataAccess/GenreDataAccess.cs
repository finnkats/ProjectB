using System.Text.Json;

public static class GenreDataAccess {
    public static Dictionary<string, Genre> ReadGenres()
    {   
        string json = File.ReadAllText(@"DataSources/accounts.json");
        var genreDataList = JsonSerializer.Deserialize<Dictionary<string, Genre>?>(json); // Convert to dictionary, with AccountDataModel object as value
        return genreDataList == null ? new Dictionary<string, Genre>() : genreDataList; 
    }

    public static void UpdateGenres(){
        string jsonPath = @"DataSources/accounts.json";

        string json = JsonSerializer.Serialize(App.Genres);
        File.WriteAllText(jsonPath, json);
    }
}
