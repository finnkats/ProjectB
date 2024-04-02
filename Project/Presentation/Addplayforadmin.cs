using System;

public static class AddPlayForAdmin
{
    public static void AdminTool()
    {
        Console.WriteLine("Enter a performance you want to add.");
        Console.WriteLine("Performance name: ");
        string performanceName = Console.ReadLine();
        Console.Clear();
  
  
        List<string> Genres = new List<string>();
        while (true)
        {
            Console.WriteLine("Enter Q if you want to stop adding Genres");
            Console.WriteLine("Genre: ");
            string Genre = Console.ReadLine();
            if(Genre.ToLower() == "q"){
                Console.Clear();
                break;
            }
            else{
                Genres.Add(Genre);
                Console.WriteLine($"{Genre} has been added.")
                Console.Clear();
                break;
            }
        }
              
  
        Console.WriteLine("Will the performance be currently active?");
        Console.WriteLine("\n1. Yes");
        Console.WriteLine("2. No");

        string activeInput = Console.ReadLine();
        Console.Clear();
        bool Active = true;
  
        if (Active_input == "2")
        {
            Active = false;
        }  
  
        Play Film = new Play(performanceName, Genres, Active);
        AddFilm.AddToJson(Film);
        Console.WriteLine("Performance succesfully added");
    }   
}
