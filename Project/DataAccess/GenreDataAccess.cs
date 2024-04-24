using System.Text.Json;

public static class GenreDataAccess {
    // Dictionary where the key is a string of ID ("ID0") and its value is a Genre object
    // and puts it into App.Genres
    public static Dictionary<string, Genre> ReadGenres()
    {   
        string json = File.ReadAllText(@"DataSources/genres.json");
        var genreDataList = JsonSerializer.Deserialize<Dictionary<string, Genre>?>(json); // Convert to dictionary, with AccountDataModel object as value
        return genreDataList == null ? new Dictionary<string, Genre>() : genreDataList; 
    }

    // Updates genres.json with App.Genres
    public static void UpdateGenres(){
        string jsonPath = @"DataSources/genres.json";

        string json = JsonSerializer.Serialize(App.Genres);
        File.WriteAllText(jsonPath, json);
    }
}
