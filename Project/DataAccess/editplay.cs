using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

class EditPlay
{
    public string FilmFinder(string FilmName)
    {
        var FilmsJson = File.ReadAllText(@"../../DataSources/Films.json");
        var films = JsonSerializer.Deserialize<Dictionary<string, Play>>(FilmsJson);

        if (films == null)
        {
            Console.WriteLine("Nothing to edit")
            return
        }

        else
        {
            foreach (KeyValuePair<string, Play> film in films)
            {
                if (film.Value.ContainsValue(FilmName))
            }
        }
    }

    public void AdminTool()
    {
        Console.WriteLine("What movie/play do you want to edit?")
        string FilmName = Console.ReadLine();
    }
}