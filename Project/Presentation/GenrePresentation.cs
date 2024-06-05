public class GenrePresentation : PresentationBase<Genre>
{
    public GenrePresentation(LogicBase<Genre> logic) : base(logic) { }

    public void AddGenre()
    {
        Console.Clear();
        string? genreName = GetNameInput();
        if (genreName is null) return;
        Console.WriteLine();

        int genreAge;
        while (true)
        {
            genreAge = -1;
            Console.WriteLine($"Enter age-rating for genre {genreName}");
            Console.WriteLine(Genre.AgeString() + "'E' to exit");
            string? ExitCheck = Console.ReadLine();
            if (ExitCheck == null) break;
            if (ExitCheck.ToLower() == "e")
            {
                Console.WriteLine("Exiting");
                Thread.Sleep(1000);
                return;
            }
            //Int32.TryParse(ExitCheck, out genreAge);
            if (!Int32.TryParse(ExitCheck, out genreAge) || !App.genreLogic.ValidAge(genreAge))
            {
                Console.WriteLine($"\n{ExitCheck} is not a valid age-rating");
                Thread.Sleep(1000);
                Console.Clear();
            }
            else break;
        }
        Console.WriteLine();

        if (!App.genreLogic.AddGenre(genreName, genreAge))
        {
            Console.WriteLine("An error occurred while adding genre.");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();

            return;
        }

        Console.WriteLine($"Genre {genreName} with age-rating {genreAge} has been added.");
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
        return;
    }

    public void EditGenreStart()
    {
        string genreId = GetItem("Which genre do you want to edit?", "Exit", InEditMenu: true);
        Console.WriteLine(genreId);

        if (genreId == "add")
        {
            App.genrePresentation.AddGenre();
            return;
        }

        if (genreId == "null") genreId = "";
        while (true)
        {
            int choice = EditObject(genreId);
            if (choice == 0) return;
            if (choice == 2)
            {
                Console.Clear();
                Console.Write($"Enter new age-rating for '{Logic.Dict[genreId].Name}', currently: {Logic.Dict[genreId].Age}\n" +
                                   Genre.AgeString() + "\n" + "> ");
                int oldAge = Logic.Dict[genreId].Age;
                if (!Int32.TryParse(Console.ReadLine(), out int newAge) || !Genre.Ages.Contains(newAge))
                {
                    Console.WriteLine("Invalid input");
                    Thread.Sleep(2000);
                    continue;
                }
                if (!App.genreLogic.ChangeAge(genreId, newAge))
                {
                    Console.WriteLine($"An error occurred while changing age-rating. Try again");
                }
                else
                {
                    Console.WriteLine($"Successfully changed '{oldAge}' to '{newAge}'");
                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }
    }
}