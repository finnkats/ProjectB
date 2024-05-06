public class GenreLogic : LogicBase<Genre>{
    // Function gets called to add A genre
    // It checks if the name isn't empty or the genre with the same name already exists
    // Returns if it successfully added the genre
    public bool AddGenre(string name, int age){
        if (name == "") return false;
        foreach (var genre in App.Genres.Values){
            if (name.ToLower() == genre.Name.ToLower()) return false;
        }
        Genre newGenre = new Genre(name, age);
        string genreId = GetID();
        App.Genres.Add(genreId, newGenre);
        GenreDataAccess.UpdateGenres();
        return true;
    }

    // Same as ChangeName
    // List of ages should not be more global or something, because other functions use it too
    public bool ChangeAge(string id, int age){
        List<int> ages = new(){0, 6, 9, 13, 17};
        if (!App.Genres.ContainsKey(id)) return false;
        if (age < 0) return false;
        App.Genres[id].Age = age;
        GenreDataAccess.UpdateGenres();
        return true;
    }
}
