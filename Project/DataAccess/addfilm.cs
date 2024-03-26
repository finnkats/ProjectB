using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class AddFilm
{
    public void AddToJson(AddFilm Film)
    {
        int ID = this.ID_Asigner();
        var FilmDict = new Dictionary<int, Play>()
        {
            {ID, Film}
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


    public void AdminTool()
    {
        Console.WriteLine("Enter a movie/play you want to add.");
        Console.WriteLine("\nFilmname: ");
        string MovieName = Console.ReadLine();


        List<string> Genres = new List<string>();
        bool LoopCheck = true;
        while (LoopCheck == true)
        {
            Console.WriteLine("Enter Q if you want to stop adding Genres");
            Console.WriteLine("Genre: ");
            string Genre = Console.ReadLine();
            
            switch(Genre)
            {
                case "Q":
                    LoopCheck = false;
                    break;

                case "q":
                    LoopCheck = false;
                    break;

                default:
                    Genres.Add(Genre);
                    break;

            }
        }
            

        Console.WriteLine("Is the movie/play currently active?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");

        string Active_input = Console.ReadLine();
        bool Active = true;

        if (Active_input == "2")
        {
            Active = false;
        }  

        Play Film = new Play(MovieName, Genres, Active);
        this.AddToJson(Film);
        Console.WriteLine("Movie succesfully added");
    }   
}