public static class GenreLogic {
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

    public static bool ChangeAge(string id, int age){
        List<int> ages = new(){0, 6, 9, 13, 17};
        if (!App.Genres.ContainsKey(id)) return false;
        if (age < 0) return false;
        App.Genres[id].Age = age;
        GenreDataAccess.UpdateGenres();
        return true;
    }
}
