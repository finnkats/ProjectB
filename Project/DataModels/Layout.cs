using System;

public class Layout
{
    // Property to hold the 2D array of seats
    public int[][] Seats { get; set; }
    public int SeatAmount { get; set; }

    public Layout(int[][] seats, int seatAmount)
    {
        Seats = seats;
        if (seatAmount > 99)
        {
            throw new ArgumentException($"The seat amount cannot exceed 99, you have: {seatAmount}");
        }
        SeatAmount = seatAmount;
    }


    // for now
    public static int[][] array =  new int[5][];
}
