public static class GenrePresenation {
    public static void AddGenre(){
        List<int> ages = new(){0, 6, 9, 13, 17};
        while (true){
            Console.Clear();
            Console.WriteLine("Enter genre name:");
            string Name = Console.ReadLine() ?? "";
            if (Name == ""){
                Console.Write("Invalid name");
                Thread.Sleep(2500);
                continue;
            }
            foreach (var genre in App.Genres){
                if (genre.Value.Name.ToLower() == Name.ToLower()){
                    Console.Write($"Genre with name {Name} already exists");
                    Thread.Sleep(3000);
                    return;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Enter age-rating for {Name}\n" + 
                              "0: For everyone\n6: For 6+\n9: For 9+\n13: For 13+\n17: For 17+");
            if (!Int32.TryParse(Console.ReadLine(), out int Age) || !ages.Contains(Age)){
                Console.WriteLine("Invalid input");
                Thread.Sleep(2500);
                continue;
            }

            if (!GenreLogic.AddGenre(Name, Age)){
                Console.WriteLine("An error occurred while adding genre. Try again.");
                Thread.Sleep(3000);
                continue;
            }

            Console.WriteLine($"Genre {Name} with age-rating {Age} has been added.");
            Thread.Sleep(5000);
            return;
        }
    }

    public static string GetGenre(){
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
                genres += $"{index++}: {genre.Item2}";
            }
            genres += $"\n{index}: Cancel";
            Console.WriteLine(genres);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
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

    public static List<string> GetGenres(string PerformanceId = ""){
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
            Console.WriteLine("Which genres belong to this performance?");
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

    public static void EditGenreStart(){
        string genreId = GetGenre();
        if (genreId == "null") return;
        EditGenre(genreId);
    }

    public static void EditGenre(string genreId){
        List<int> ages = new(){0, 6, 9, 13, 17};
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{App.Genres[genreId].Name}\"");
            Console.WriteLine($"2: Change age-rating \"{App.Genres[genreId].Age}\"");
            Console.WriteLine("3: Exit\n");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1"){
                Console.WriteLine($"Enter new name for {App.Genres[genreId].Name}:");
                string oldName = App.Genres[genreId].Name;
                string newName = Console.ReadLine() ?? "";
                if (!GenreLogic.ChangeName(genreId, newName)){
                    Console.WriteLine($"Couldn't change name, either invalid or '{newName}' already exists");
                } else {
                    Console.WriteLine($"Successfully changed '{oldName}' to '{newName}'");
                }
                Thread.Sleep(4000);
            }

            else if (choice == "2"){
                Console.Clear();
                Console.WriteLine($"Enter new age-rating for '{App.Genres[genreId].Name}', currently: {App.Genres[genreId].Age}\n" +
                                   "0: For everyone\n6: For 6+\n9: For 9+\n13: For 13+\n17: For 17+");
                int oldAge = App.Genres[genreId].Age;
                if (!Int32.TryParse(Console.ReadLine(), out int newAge) || !ages.Contains(newAge)){
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(2000);
                    continue;
                }
                if (!GenreLogic.ChangeAge(genreId, newAge)){
                    Console.WriteLine($"An error occured while changing age-rating. Try again");
                } else {
                    Console.WriteLine($"Successfully changed '{oldAge}' to '{newAge}'");
                }
                Thread.Sleep(4000);
            }

            else if (choice == "3"){
                return;
            } else {
                Console.WriteLine("Invalid input");
                Thread.Sleep(2000);
            }
        }
    }
}