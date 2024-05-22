using System.Globalization;

public class HallPresentation : PresentationBase<Hall>
{
    public HallPresentation(LogicBase<Hall> logic) : base(logic) { }

    public void AddHall()
    {
        Console.Clear();
        Layout selectedLayout = null;
        string? hallName = GetNameInput();
        if (hallName is null) return;
        Console.WriteLine();

        int hallSeats;
        while (true)
        {
            hallSeats = -1;


            // Pre-made Layout #1 (30 seats)
            int[][] layout1Seats = new int[][]
            {
                new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0},
                new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0},
                new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30}
            };

            // Pre-made Layout #2 (60 seats)
            int[][] layout2Seats = new int[][]
            {
                new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0},
                new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0},
                new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30},
                new int[]{31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42},
                new int[]{0, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 0},
                new int[]{0, 0, 53, 54, 55, 56, 57, 58, 59, 60, 0, 0}
            };

            // Pre-made Layout #3 (90 seats)
            int[][] layout3Seats = new int[][]
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
            };

            Layout layout1 = new Layout(layout1Seats, 30);
            Layout layout2 = new Layout(layout2Seats, 60);
            Layout layout3 = new Layout(layout3Seats, 90);

            Console.WriteLine("1. Layout #1 (30 seats)");
            LayoutPresentation.PrintLayout(layout1, new HashSet<int>() { }, new HashSet<int>() { });

            Console.WriteLine(); // Create space between the layouts

            Console.WriteLine("2. Layout #2 (60 seats)");
            LayoutPresentation.PrintLayout(layout2, new HashSet<int>() { }, new HashSet<int>() { });

            Console.WriteLine(); // Create space between the layouts

            Console.WriteLine("3. Layout #3 (90 seats)");
            LayoutPresentation.PrintLayout(layout3, new HashSet<int>() { }, new HashSet<int>() { });

            Console.WriteLine("Choose a Layout:\n");
            Console.Write("> ");
            string layoutHallChoice = Console.ReadLine();

            if (layoutHallChoice == "1")
            {
                selectedLayout = layout1;
                hallSeats = layout1.SeatAmount;
                break;
            }

            else if (layoutHallChoice == "2")
            {
                selectedLayout = layout2;
                hallSeats = layout2.SeatAmount;
                break;
            }

            else if (layoutHallChoice == "3")
            {
                selectedLayout = layout3;
                hallSeats = layout3.SeatAmount;
                break;
            }

            else
            {
                Console.WriteLine("Invalid choice input");
            }


        }
        Console.WriteLine();

        string locationId = App.locationPresentation.GetItem("In which location is this hall?", "No location yet");

        // TO DO add layout chooser and put the correct layout in here
        // and remove seatchoice
        if (!App.hallLogic.AddHall(hallName, selectedLayout, locationId))
        {
            Console.WriteLine("An error occurred while adding hall.");
            Thread.Sleep(3000);
            return;
        }

        string locationAdded = (locationId == "null") ? "null" : $" to location {App.Locations[locationId].Name}";
        Console.WriteLine($"Hall '{hallName}' with {hallSeats} seats has been added" + locationAdded);
        Thread.Sleep(5000);
    }

    public void EditHallStart()
    {
        string hallId = GetItem("Which hall do you want to edit?", "Exit", InEditMenu: true);

        if (hallId == "add")
        {
            App.hallPresentation.AddHall();
            return;
        }

        if (hallId == "null") hallId = "";
        while (true)
        {
            int choice = EditObject(hallId);
            if (choice == 0) return;
        }
    }
}
