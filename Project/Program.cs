using Logic;

App.Start();

Layout.array[0] = new int[]{0, 0, 1, 2, 3, 4, 5, 0, 0};
Layout.array[1] = new int[]{0, 0, 6, 7, 8, 9, 10, 0, 0};
Layout.array[2] = new int[]{0, 0, 11, 12, 13, 14, 15, 0, 0};
Layout.array[3] = new int[]{16, 17, 18, 19, 20, 21, 22, 23, 24};
Layout.array[4] = new int[]{25, 26, 27, 28, 29, 30, 31, 32, 33};
Layout.array[5] = new int[]{34, 35, 36, 37, 38, 39, 40, 41, 42};
Layout.array[6] = new int[]{43, 44, 45, 46, 47, 48, 49, 50, 51};
Layout.array[7] = new int[]{52, 53, 54, 55, 56, 57, 58, 59, 60};
Layout.array[8] = new int[]{61, 62, 63, 64, 65, 66, 67, 68, 69};
Layout.array[9] = new int[]{70, 71, 72, 73, 74, 75, 76, 77, 78};
Layout.array[10] = new int[]{79, 80, 81, 82, 83, 84, 85, 86, 87};
Layout.array[11] = new int[]{0, 0, 0, 88, 89, 90, 0, 0,};
//LayoutPresentation.PrintLayout(new Layout(Layout.array, 90), new HashSet<int>(){1, 3, 7, 10, 11, 12}, new HashSet<int>(){8, 9});
App.Halls["ID0"].SeatLayout = new Layout(Layout.array, 90);
LayoutPresentation.ChooseSeats(App.Plays["ID0"][0]);

return;
//Console.Clear();
// program arguments
if (args.Length != 0) {
    if (args[0].ToLower() == "demo") App.AddAllMenus();
    else if (args[0].ToLower() == "admin") AccountLogic.Login("Admin123", "Password123");
}

bool invalidInput = false;
string? menuString;
while (App.CurrentMenu != null){
    menuString = App.CurrentMenu.MenuString();
    menuString += (invalidInput ? "\nInvalid input\n" : "\n") + "> ";
    Console.Write(menuString);
    // input will be 0 if the the input is not an integer
    Int32.TryParse(Console.ReadLine(), out int input);
    Action? function = App.CurrentMenu.GetFunction(input);
    if (function != null) {
        function();
        invalidInput = false;
    }
    else invalidInput = true;
    Console.Clear();
}
Console.WriteLine("Exiting program...");

// Pre-made Layout #1 (30 seats)
// Layout.array[0] = new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0};
// Layout.array[1] = new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0};
// Layout.array[2] = new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30};


// Pre-made Layout #2 (60 seats)
// Layout.array[0] = new int[]{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 0};
// Layout.array[1] = new int[]{0, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 0};
// Layout.array[2] = new int[]{19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30};
// Layout.array[3] = new int[]{31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42};
// Layout.array[4] = new int[]{0, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 0};
// Layout.array[5] = new int[]{0, 0, 53, 54, 55, 56, 57, 58, 59, 60, 0, 0};


// Pre-made Layout #3 (90 seats)
// Layout.array[0] = new int[]{0, 0, 1, 2, 3, 4, 5, 0, 0};
// Layout.array[1] = new int[]{0, 0, 6, 7, 8, 9, 10, 0, 0};
// Layout.array[2] = new int[]{0, 0, 11, 12, 13, 14, 15, 0, 0};
// Layout.array[3] = new int[]{16, 17, 18, 19, 20, 21, 22, 23, 24};
// Layout.array[4] = new int[]{25, 26, 27, 28, 29, 30, 31, 32, 33};
// Layout.array[5] = new int[]{34, 35, 36, 37, 38, 39, 40, 41, 42};
// Layout.array[6] = new int[]{43, 44, 45, 46, 47, 48, 49, 50, 51};
// Layout.array[7] = new int[]{52, 53, 54, 55, 56, 57, 58, 59, 60};
// Layout.array[8] = new int[]{61, 62, 63, 64, 65, 66, 67, 68, 69};
// Layout.array[9] = new int[]{70, 71, 72, 73, 74, 75, 76, 77, 78};
// Layout.array[10] = new int[]{79, 80, 81, 82, 83, 84, 85, 86, 87};
// Layout.array[11] = new int[]{0, 0, 0, 88, 89, 90, 0, 0,};