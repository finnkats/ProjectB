using System.Text;

public static class LayoutPresentation
{
    public static void ChooseTickets(Play play){

    }

    public static void PrintLayout(Layout layout, HashSet<int>? seats = null, HashSet<int>? selected = null){
        // Because seats will be '[00] ', every seat is 5 long, however, ignore the last whitespace
        // and make it so whitespace is before and after the screen (" -------- "), but make sure a screen atleast always exists
        string screen = new string('-', Math.Max(layout.Seats[0].Length * 5 - 1 - 2, 2));
        Console.ForegroundColor = ConsoleColor.Black;
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
    }
}