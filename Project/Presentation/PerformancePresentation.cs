using System;

public static class PerformancePresentation
{
    public static void AddPerformance(){
        Console.Clear();
        Console.WriteLine("Enter a performance you want to add.");
        Console.WriteLine("Performance name: ");
        string? performanceName = Console.ReadLine();
        Console.Clear();
  
        var genres = GenrePresenation.GetGenres();

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

    public static string? PerformanceChoice(string question, bool onlyActive=false){
        Console.Clear();

        var PerformanceOptions = PerformanceLogic.GetPerformanceOptions(onlyActive);
        int page = 1;
        int pages = (PerformanceOptions.Count + 4) / 5;
        int offset = 0;

        while (true){
            Console.Clear();
            var PerformanceOptionsScope = PerformanceOptions.Skip(0 + (5 * (page - 1))).Take(5).ToList();
            foreach (var performanceOption in PerformanceOptionsScope){
                Console.WriteLine(performanceOption.Item2);
            }
            Console.WriteLine();

            if (offset == 2 || PerformanceOptions.Count > 5){
                Console.WriteLine($"Page {page}/{pages}");
                Console.WriteLine($"{PerformanceOptionsScope.Count + 1}: Next page");
                Console.WriteLine($"{PerformanceOptionsScope.Count + 2}: Previous page");
                offset = 2;
            }
            Console.WriteLine($"{PerformanceOptionsScope.Count + 1 + offset}: Exit\n");

            Console.WriteLine(question);

            Int32.TryParse(Console.ReadLine(), out int choice);
            try {
                string performanceId = PerformanceOptionsScope[choice - 1].Item1;
                return performanceId;
            } catch (ArgumentOutOfRangeException) {
                if (choice == PerformanceOptionsScope.Count() + 1 + offset) return null;
                else if (offset == 2){
                    if (choice == PerformanceOptionsScope.Count() + 1){
                        page = (page + 1 > pages) ? 1 : page + 1;
                    } else if (choice == PerformanceOptionsScope.Count() + 2){
                        page = (page - 1 < 1) ? pages : page - 1;
                    }
                }
            }
        }
    }

    public static void EditPerformanceStart(){
        string? performanceId = PerformanceChoice("Choose the performance you want to edit:");
        if (performanceId == null) return;
        EditPerformance(performanceId);
    }

    public static void EditPerformance(string performanceId){
        while (true){
            Console.Clear();
            Console.WriteLine($"1: Change name \"{App.Performances[performanceId].Name}\"");
            List<string> currentGenres = new();
            foreach (var genreId in App.Performances[performanceId].Genres){
                currentGenres.Add(App.Genres[genreId].Name);
            }
            string seperator = ", ";
            Console.WriteLine($"2: Change genres: [{String.Join(seperator, currentGenres)}]");
            Console.WriteLine($"3: Change active status \"{App.Performances[performanceId].Active}\"");
            Console.WriteLine("4: Exit\n");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 1){
                Console.Clear();
                Console.WriteLine("Enter new name:\n");
                if (PerformanceLogic.ChangeName(Console.ReadLine() ?? "", performanceId, App.Performances)) Console.WriteLine("Successfully changed name");
                else Console.WriteLine("Couldn't change name");
                Thread.Sleep(2500);
            }
            else if (choice == 2){
                Console.Clear();
                List<string> genres = GenrePresenation.GetGenres(performanceId);
                PerformanceLogic.ChangeGenres(genres, performanceId, App.Performances);
                Console.WriteLine("Successfully changed genres");
                Thread.Sleep(2500);
            }
            else if (choice == 3){
                PerformanceLogic.ChangeActive(performanceId, App.Performances);
                Console.WriteLine("Successfully changed active status");
                Thread.Sleep(2500);
            }
            else if (choice == 4){
                return;
            }
        }
    }
}
