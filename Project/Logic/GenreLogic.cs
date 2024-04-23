public static class GenreLogic {
    // Function gets called to add A genre
    // It checks if the name isn't empty or the genre with the same name already exists
    // Returns if it successfully added the genre
    public static bool AddGenre(string name, int age){
        if (name == "") return false;
        foreach (var genre in App.Genres.Values){
            if (name.ToLower() == genre.Name.ToLower()) return false;
        }
        Genre newGenre = new Genre(name, age);
        string genreId = AssignId();
        App.Genres.Add(genreId, newGenre);
        GenreDataAccess.UpdateGenres();
        return true;
    }

    public static string AssignId(){
        return $"ID{App.Genres.Count}";
    }

    // Same like AddGenre, where it checks if name isnt empty or already exists
    // and then changes the genre's name with the given id
    public static bool ChangeName(string id, string name){
        if (!App.Genres.ContainsKey(id)) return false;
        if (name == "") return false;
        foreach (var genre in App.Genres){
            if (genre.Key == id) continue;
            if (genre.Value.Name.ToLower() == name.ToLower()) return false;
        }
        App.Genres[id].Name = name;
        GenreDataAccess.UpdateGenres();
        return true;
    }

    // Same as ChangeName
    // List of ages should not be more global or something, because other functions use it too
    public static bool ChangeAge(string id, int age){
        List<int> ages = new(){0, 6, 9, 13, 17};
        if (!App.Genres.ContainsKey(id)) return false;
        if (age < 0) return false;
        App.Genres[id].Age = age;
        GenreDataAccess.UpdateGenres();
        return true;
    }
}
