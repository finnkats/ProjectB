public class GenrePresentation : PresentationBase<Genre>{
    public GenrePresentation(LogicBase<Genre> logic) : base(logic){}

    public void AddGenre(){
        Console.Clear();
        string? genreName = GetNameInput();
        if (genreName is null) return;
        Console.WriteLine();

        int genreAge;
        while (true){
            genreAge = -1;
            Console.WriteLine($"Enter age-rating for genre {genreName}");
            Console.WriteLine(Genre.AgeString() + "'-1' to exit");
            Int32.TryParse(Console.ReadLine(), out genreAge);
            if (genreAge == -1){
                Console.WriteLine("Exitting");
                return;
            }
            if (!App.genreLogic.ValidAge(genreAge)){
                Console.WriteLine($"{genreAge} is not a valid age-rating");
            } else break;
        }
        Console.WriteLine();

        if (!App.genreLogic.AddGenre(genreName, genreAge)){
            Console.WriteLine("An error occurred while adding genre.");
            Thread.Sleep(3000);
            return;
        }

        Console.WriteLine($"Genre {genreName} with age-rating {genreAge} has been added.");
        Thread.Sleep(5000);
        return;
    }

    // Returns an id of a genre
    // This is one of many menu's that look similar to eachother, so this should be refactored
    public string GetGenre(){
        List<(string, string)> GenresOrdered = new();
        foreach (var genre in App.Genres){
            GenresOrdered.Add((genre.Key, $"{genre.Value.Name} ({genre.Value.Age})"));
        }
        GenresOrdered = GenresOrdered.OrderBy(genre => genre.Item2).ToList();

        int index = 1;
        string genres = "";
        while (true){
            int choice = -1;
            Console.Clear();
            Console.WriteLine("Choose a genre:");
            foreach (var genre in GenresOrdered){
                genres += $"{index++}: {genre.Item2}\n";
            }
            genres += $"\n{index}: Exit\n";
            Console.WriteLine(genres);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input");
                    continue;
                }
                return GenresOrdered[choice - 1].Item1;
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == GenresOrdered.Count) return "null";
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
            }
        }
    }

    // Get genre's returns a list of Genres
    // it can take both a performanceid and or question,
    // if a performance id is given, then PerformanceGenres will already be populated with genres
    // if question is gives, it changes the question which is asked in the menu
    // this is also one of many menu's i think that are similar to others, so we should see how we are going
    // to refactor this, as there are a few differences between the other similar functions
    public List<string> GetGenres(string PerformanceId = "", string question = "Which genre belongs to this performance?"){
        if (PerformanceId != "" && !App.Performances.ContainsKey(PerformanceId)) PerformanceId = "";
        List<string> PerformanceGenres = (PerformanceId == "") ? new() : App.Performances[PerformanceId].Genres;

        List<(string, string)> GenresOrdered = new();
        foreach (var genre in App.Genres){
            if (PerformanceGenres.Contains(genre.Key)) continue;
            GenresOrdered.Add((genre.Key, genre.Value.Name));
        }
        GenresOrdered = GenresOrdered.OrderBy(genre => genre.Item2).ToList();
        string seperator = ", ";

        while(true){
            PerformanceGenres.Sort();
            int choice = -1;
            Console.Clear();
            List<string> currentGenres = new();
            PerformanceGenres.ForEach(genreId => currentGenres.Add(App.Genres[genreId].Name));
            Console.WriteLine($"Current genres: [{String.Join(seperator, currentGenres)}]\n");
            Console.WriteLine(question);
            string genres = "";
            int index = 1;
            foreach (var genre in GenresOrdered){
                if (PerformanceGenres.Contains(genre.Item1)) continue;
                genres += $"{index++}: {genre.Item2}\n";
            }
            genres += $"\n{index}: Confirm";
            Console.WriteLine(genres);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    Thread.Sleep(2500);
                } else {
                    PerformanceGenres.Add(GenresOrdered[choice - 1].Item1);
                    GenresOrdered.RemoveAt(choice - 1);
                }
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == GenresOrdered.Count){
                    return PerformanceGenres;
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }

    public void EditGenreStart(){
        string genreId = GetGenre();
        if (genreId == "null") genreId = "";
        while (true){
            int choice = EditObject(genreId);
            if (choice == 0) return;
            if (choice == 2){
                Console.Clear();
                Console.Write($"Enter new age-rating for '{Logic.Dict[genreId].Name}', currently: {Logic.Dict[genreId].Age}\n" +
                                   Genre.AgeString());
                int oldAge = Logic.Dict[genreId].Age;
                if (!Int32.TryParse(Console.ReadLine(), out int newAge) || !Genre.Ages.Contains(newAge)){
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(2000);
                    continue;
                }
                if (!App.genreLogic.ChangeAge(genreId, newAge)){
                    Console.WriteLine($"An error occured while changing age-rating. Try again");
                } else {
                    Console.WriteLine($"Successfully changed '{oldAge}' to '{newAge}'");
                }
                Thread.Sleep(4000);
            }
        }
    }
}
