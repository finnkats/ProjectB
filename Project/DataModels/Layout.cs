using System;

public class Layout
{
    // Property to hold the 2D array of seats
    public int[,] Seats { get; set; }
    public int SeatAmount { get; set; }

    public Layout(int[,] seats, int seatAmount)
    {
        Seats = seats;
        SeatAmount = Math.Min(seatAmount, 99); // Ensure SeatAmount doesn't exceed 99
    }

}