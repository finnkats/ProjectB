public class GenreLogic : LogicBase<Genre>{
    // Function gets called to add A genre
    // It checks if the name isn't empty or the genre with the same name already exists
    // Returns if it successfully added the genre
    public bool AddGenre(string name, int age){
        if (!ValidAge(age)) return false;
        return base.AddObject(new Genre(name, age));
    }

    public bool ValidAge(int age){
        return Genre.Ages.Contains(age);
    }

    public bool ChangeAge(string id, int age){
        if (!App.Genres.ContainsKey(id)) return false;
        if (!Genre.Ages.Contains(age)) return false;
        App.Genres[id].Age = age;
        DataAccess.UpdateItem<Genre>();
        return true;
    }
}
