using System;

public static class PerformancePresentation
{
    public static void AddPerformance(){
        Console.Clear();
        Console.WriteLine("Enter a performance you want to add.");
        Console.WriteLine("Performance name: ");
        string? performanceName = Console.ReadLine();
        Console.Clear();
  
        var genres = GetGenres();
  
        Console.WriteLine("Will the performance be currently active?");
        Console.WriteLine("\n1. Yes");
        Console.WriteLine("2. No");

        bool active = (Console.ReadLine() != "1");

        Console.Clear();
        if (PerformanceLogic.AddPerformance(performanceName ?? "", genres, active)){
            Console.WriteLine($"Performance {performanceName} has been added");
        } else {
            Console.WriteLine($"Performance {performanceName} already exists");
        }

        Thread.Sleep(3500);
        Console.Clear();
    }

    public static List<string> GetGenres(){
        List<string> Genres = new List<string>();
        string? genre = default;
        Console.WriteLine("Enter Q to stop adding Genres\n");
        while (genre?.ToLower() != "q")
        {
            Console.WriteLine("Genre: ");
            genre = Console.ReadLine();
            Console.WriteLine();

            if(genre?.ToLower() != "q"){
                Genres.Add(genre ?? "");
            }
        }
        Console.Clear();
        return Genres;
    } 

    public static void EditPerformance(){

    }
}
