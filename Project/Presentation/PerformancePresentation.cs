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

        bool active = (Console.ReadLine() == "1");

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

    public static void EditPerformanceChoice(){
        var Performances = PerformanceDataAccess.ReadPerformances();
        while (true){
            Console.Clear();
            int index = 1;
            List<string> PerformanceIds = new();

            foreach (KeyValuePair<string, Performance> performance in Performances){
                PerformanceIds.Add(performance.Key);
                Console.WriteLine($"{index++}: {performance.Value.Name}");
            }
            Console.WriteLine($"{index}: Exit\n");

            Console.WriteLine("Choose the performance you want to edit:");
            Int32.TryParse(Console.ReadLine(), out int choice);
            try {
                EditPerformance(PerformanceIds[choice - 1]);
            } catch (ArgumentOutOfRangeException) {
                if (choice == PerformanceIds.Count() + 1) return;
            }
        }
    }

    public static void EditPerformance(string performanceId){
        var Performances = PerformanceDataAccess.ReadPerformances();
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{Performances[performanceId].Name}\"");
            Console.WriteLine($"2: Change genres \"{String.Join('|', Performances[performanceId].Genres)}\"");
            Console.WriteLine($"3: Change active status \"{Performances[performanceId].Active}\"");
            Console.WriteLine("4: Exit\n");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1){
                Console.Clear();
                Console.WriteLine("Enter new name:\n");
                if (PerformanceLogic.ChangeName(Console.ReadLine() ?? "", performanceId, Performances)) Console.WriteLine("Successfully changed name");
                else Console.WriteLine("Couldn't change name");
                Thread.Sleep(2500);
            }
            else if (choice == 2){
                Console.Clear();
                List<string> genres = GetGenres();
                PerformanceLogic.ChangeGenres(genres, performanceId, Performances);
                Console.WriteLine("Successfully changed genres");
                Thread.Sleep(2500);
            }
            else if (choice == 3){
                PerformanceLogic.ChangeActive(performanceId, Performances);
                Console.WriteLine("Successfully changed active status");
                Thread.Sleep(2500);
            }
            else if (choice == 4){
                return;
            }
        }
    }
}
