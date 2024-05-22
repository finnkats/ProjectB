public class GenreLogic : LogicBase<Genre>{
    // Function gets called to add A genre
    // It checks if the name isn't empty or the genre with the same name already exists
    // Returns if it successfully added the genre
    public static Logger logger = new GenreLogger();

    public bool AddGenre(string name, int age){
        if (!ValidAge(age)) return false;
        bool success = base.AddObject(new Genre(name, age));
        if (success)
        {
            logger.LogAction("Genre added", new { Name = name, Age = age });
        }
        return success;
    }

    public bool ValidAge(int age){
        return Genre.Ages.Contains(age);
    }

    public bool ChangeAge(string id, int age){
        if (!App.Genres.ContainsKey(id)) return false;
        if (!Genre.Ages.Contains(age)) return false;
        int oldAge = App.Genres[id].Age;
        App.Genres[id].Age = age;
        DataAccess.UpdateItem<Genre>();
        logger.LogAction("Genre age changed", new { GenreId = id, OldAge = oldAge, NewAge = age });
        return true;
    }
}