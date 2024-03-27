using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class AddFilm
{
    public void AddToJson(AddFilm Film)
    {
        string ID = this.ID_Asigner();
        var FilmDict = new Dictionary<int, Play>()
        {
            {ID, Film};
        };
    var JsonFormat = JsonSerializer.Serialize(FilmDict, new JsonSerializerOptions
    {
        WriteIndented = true
    });

        File.WriteAllText(@"../../DataSources/Films.json", JsonFormat);
    }
    public string ID_Asigner() 
    {
        var FilmsJson = File.ReadAllText(@"../../DataSources/Films.json");
        var films = JsonSerializer.Deserialize<Dictionary<string, Play>>(FilmsJson);

        if (films == null)
        {
            return "ID1";
        }

        else
        {
            int CurrentID = 0;
            foreach (KeyValuePair<string, Play> film in films)
            {
                string NumberString = Regex.Match(film.Key, @"\d+").Value; //haalt het nummer van uit de ID als string
                int ID = Int32.Parse(NumberString);

                if (CurrentID < ID) {
                        CurrentID = ID;
                    }
            }

            CurrentID = CurrentID + 1;
            string ReturnID = @$"ID{CurrentID}";

            return ReturnID;
        }
    }
}
