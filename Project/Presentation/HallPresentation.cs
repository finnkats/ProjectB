using System.Globalization;
using System.Threading;

public class HallPresentation : PresentationBase<Hall>
{
    public HallPresentation(LogicBase<Hall> logic) : base(logic) { }

    public void AddHall()
    {
        Console.Clear();
        Layout? selectedLayout = null;
        string? hallName = GetNameInput();
        if (hallName is null) return;
        Console.WriteLine();

        int hallSeats = -1;
        int layoutIndex = 0;
        Layout[] layouts = { Layout.LayoutSmall, Layout.LayoutMedium, Layout.LayoutBig };
        string[] layoutnames = { "Small", "Medium", "Large" };
        while (true)
        {
            Console.Clear();
            Layout currentLayout = layouts[layoutIndex];
            hallSeats = currentLayout.SeatAmount;
            Console.WriteLine($"Front Page -> Home Page -> Admin Features -> Adding Hall '{hallName}'\n");
            Console.WriteLine($"Layout {layoutnames[layoutIndex]} ({hallSeats} seats):");
            LayoutPresentation.PrintLayout(currentLayout, new HashSet<int>(), new HashSet<int>());

            Console.WriteLine("\nChoose an option");
            Console.WriteLine("1: Next layout");
            Console.WriteLine("2: Previous layout");
            Console.WriteLine("3: Select this layout");
            Console.WriteLine("E: Exit without selection");

            Console.Write("\n> ");
            string input = Console.ReadLine()?.ToUpper() ?? "";

            if (input == "1")
            {
                layoutIndex = (layoutIndex + 1) % layouts.Length;
            }
            else if (input == "2")
            {
                layoutIndex = (layoutIndex - 1 + layouts.Length) % layouts.Length;
            }
            else if (input == "3")
            {
                selectedLayout = currentLayout;
                break;
            }
            else if (input == "Q")
            {
                Console.WriteLine("\nExiting...");
                Thread.Sleep(2000);
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice");
                Thread.Sleep(1000);
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

        string locationAdded = (locationId == "null") ? " no location" : $" to location {App.Locations[locationId].Name}";
        Console.WriteLine($"Hall '{hallName}' with {hallSeats} seats has been added" + locationAdded + $" with {selectedLayout.SeatAmount} seats");
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
