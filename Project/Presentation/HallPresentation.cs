using System.Globalization;

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
        while (true)
        {
            Console.Clear();
            Layout currentLayout = layouts[layoutIndex];
            hallSeats = currentLayout.SeatAmount;
            Console.WriteLine($"Layout #{layoutIndex + 1} ({hallSeats} seats):");
            LayoutPresentation.PrintLayout(currentLayout, new HashSet<int>(), new HashSet<int>());

            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("N: Next layout");
            Console.WriteLine("P: Previous layout");
            Console.WriteLine("S: Select this layout");
            Console.WriteLine("E: Exit without selection");

            Console.Write("> ");
            string input = Console.ReadLine()?.ToUpper() ?? "";

            if (input == "N")
            {
                layoutIndex = (layoutIndex + 1) % layouts.Length;
            }
            else if (input == "P")
            {
                layoutIndex = (layoutIndex - 1 + layouts.Length) % layouts.Length;
            }
            else if (input == "S")
            {
                selectedLayout = currentLayout;
                break;
            }
            else if (input == "E")
            {
                Console.WriteLine("Exiting");
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
