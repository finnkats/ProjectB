using System.Text;

public static class LayoutPresentation
{
    public static HashSet<int> ChooseSeats(Play play){
        HashSet<int> selected = new();
        while (true){
            Console.Clear();
            Console.WriteLine($"Front Page -> Home Page -> View Performances -> {App.Performances[play.PerformanceId].Name} -> Choose Seats\n");
            PrintLayout(App.Halls[play.Hall].SeatLayout, play.Seats, selected);
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("Green = Available\n");
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Yellow = Selected\n");
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Red = Unavailable\n"); Console.ResetColor();

            Console.WriteLine($"Select seats for {App.Performances[play.PerformanceId].Name} in {App.Halls[play.Hall].Name}");
            Console.WriteLine("To select a seat, enter the number of the seat");
            Console.Write("To deselect a seat, select it again\nTo continue purchase press enter\nSelect 'Q' To cancel the reservation\n\n> ");

            string input = Console.ReadLine()?.ToLower() ?? "";
            if (input == "") break;
            if (input == "q") return new HashSet<int>();
            Int32.TryParse(input, out int seat);
            if (selected.Contains(seat)) selected.Remove(seat);
            else if (seat <= 0 || seat > App.Halls[play.Hall].Seats) continue;
            else if (play.Seats.Contains(seat)) continue;
            else selected.Add(seat);
        }
        return selected;
    }

    public static void PrintLayout(Layout layout, HashSet<int>? seats = null, HashSet<int>? selected = null){
        // Because seats will be '[00] ', every seat is 5 long, however, ignore the last whitespace
        // and make it so whitespace is before and after the screen (" -------- "), but make sure a screen atleast always exists
        string screen = new string('¯', Math.Max(layout.Seats[0].Length * 5 - 1 - 2, 2));
        Console.ForegroundColor = ConsoleColor.Black;
        if (screen.Length >= 8){
            string podiumBuffer = new string(' ', screen.Length / 2 - 2);
            Console.WriteLine(podiumBuffer + "podium" + podiumBuffer);
        }
        Console.WriteLine($" {screen} ");
        Console.ResetColor();
        for (int row = 0; row < layout.Seats.Length; row++){
            for (int col = 0; col < layout.Seats[row].Length; col++){

                if (layout.Seats[row][col] == 0){
                    Console.Write("     ");
                    continue;
                }
                
                Console.Write("[");
                if (seats != null && selected != null){
                    if (seats.Contains(layout.Seats[row][col])) Console.ForegroundColor = ConsoleColor.Red;
                    else if (selected.Contains(layout.Seats[row][col])) Console.ForegroundColor = ConsoleColor.Yellow;
                    else Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write($"{layout.Seats[row][col]:00}");
                Console.ResetColor();
                Console.Write("] ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}