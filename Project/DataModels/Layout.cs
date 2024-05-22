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
    public static int[][] array = new int[12][];

    public static readonly Layout LayoutSmall = new Layout(new int[][]
    {
        new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0},
        new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0},
        new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30}
    }, 30);

    public static readonly Layout LayoutMedium = new Layout(new int[][]
    {
        new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0},
        new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0},
        new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30},
        new int[]{31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42},
        new int[]{0, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 0},
        new int[]{0, 0, 53, 54, 55, 56, 57, 58, 59, 60, 0, 0}
    }, 60);

    public static readonly Layout LayoutBig = new Layout(new int[][]
    {
        new int[]{0, 0, 1, 2, 3, 4, 5, 0, 0},
        new int[]{0, 0, 6, 7, 8, 9, 10, 0, 0},
        new int[]{0, 0, 11, 12, 13, 14, 15, 0, 0},
        new int[]{16, 17, 18, 19, 20, 21, 22, 23, 24},
        new int[]{25, 26, 27, 28, 29, 30, 31, 32, 33},
        new int[]{34, 35, 36, 37, 38, 39, 40, 41, 42},
        new int[]{43, 44, 45, 46, 47, 48, 49, 50, 51},
        new int[]{52, 53, 54, 55, 56, 57, 58, 59, 60},
        new int[]{61, 62, 63, 64, 65, 66, 67, 68, 69},
        new int[]{70, 71, 72, 73, 74, 75, 76, 77, 78},
        new int[]{79, 80, 81, 82, 83, 84, 85, 86, 87},
        new int[]{0, 0, 0, 88, 89, 90, 0, 0,}
    }, 90);
}
