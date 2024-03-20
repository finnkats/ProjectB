using System.Text.Json;

class AddFilm
{
    public string ID
    public string FilmName
    public string Genre

    public AddFilm(string filmname, string id, string genre )
    {
        ID = id
        FilmName = filmname
        Genre = genre
    }

    public void AddToJson(AddFilm Film)
    {
        var JsonFormat = JsonSerializer.Serialize(Film, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        File.WriteAllText(@"Films.json", JsonFormat);
    }
}