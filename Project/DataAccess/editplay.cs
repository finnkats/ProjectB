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
            Console.WriteLine("No film to edit");
            return null;
        }

        else
        {
            foreach (KeyValuePair<string, Play> film in films)
            {
                foreach (KeyValuePair<string, string> FilmDetail in film.Value)
             {
                    if (FilmDetail.Value == FilmName)
                    {
                        return film.Key;
                    }
                }
            }
        }
    }

    public void ChosenFilmData(string ID, string FilmDetail)
    {
        var FilmsDetailsJson = File.ReadAllText(@"../../DataSources/Films.json");
        var films = JsonSerializer.Deserialize<Dictionary<string, PlayDetails>(FilmsDetailsJson);

        if (films == null)
        {
            Console.WriteLine("No films found to edit.");
            return null;
        }

        foreach (KeyValuePair<string, PlayDetails> film in films)
        {
            if (film.Key == ID)
            {
                 
            }
        }
    }

    public void Menu(int MenuUse)
    {
        if (MenuUse == 1) {
            Console.WriteLine("\n1. Location")
            Console.WriteLine("2. Time")
            Console.WriteLine("3. Room")
            Console.WriteLine("4. PlayName")
        }

        if (MenuUse == 2)
        {
            Console.WriteLine("\n1. Delete value")
            Console.WriteLine("2. Add value")
            Console.WriteLine("3. change value")
        }
    }

    public void AdminTool()
    {
        Console.WriteLine("What movie/play do you want to edit?");
        string FilmName = Console.ReadLine();
        string ID = EditPlay.FilmFinder(FilmName);

        Console.WriteLine("What aspect do you want to edit?");
        EditPlay.Menu(1);

        int? UserChoice = null
        do
        {
            int UserInput = Console.ReadLine();

            switch (UserInput)
            {
                case 1:
                    Console.Clear();
                    EditPlay.Menu(2);
                    UserChoice = Console.ReadLine();


            }
        }
        while (UserChoice == null)
    }
}