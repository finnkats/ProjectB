using System.Text;

public static class LayoutPresentation
{
    public static void ChooseTickets(Play play){

    }

    public static void PrintLayout(Layout layout){
        // Because seats will be '[00] ', every seat is 5 long, however, ignore the last whitespace
        // and make it so whitespace is before and after the screen (" -------- "), but make sure a screen atleast always exists
        string screen = new string('-', Math.Max(layout.Seats.GetLength(0) * 5 - 1 - 2, 2));
        Console.WriteLine($" {screen} ");
        for (int row = 0; row < layout.Seats.GetLength(0); row++){
            for (int col = 0; col < layout.Seats.GetLength(1); col++){
                if (layout.Seats[row,col] == 0) Console.Write("     ");
                else Console.Write($"[{layout.Seats[row,col]:00}] ");
            }
            Console.WriteLine();
        }
    }
}

// for testing
public class Layout {
    public int SeatAmount { get; set;}
    public int[,] Seats { get; set;}
    public Layout(){
        SeatAmount = 25;
        Seats = new int[5,5]{{1, 2, 3, 4, 5},
                             {6, 7, 8, 9 ,10}, 
                             {11, 12, 0, 13, 14}, 
                             {15, 16, 0, 17, 18}, 
                             {19, 0, 0, 0, 20}
                            };
    }
}