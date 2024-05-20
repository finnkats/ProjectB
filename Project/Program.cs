using Logic;

App.Start();

Layout.array[0] = new int[]{1, 2, 3, 4, 5};
Layout.array[1] = new int[]{6, 7, 8, 9 ,10};
Layout.array[2] = new int[]{11, 12, 0, 13, 14};
Layout.array[3] = new int[]{15, 16, 0, 17, 18};
Layout.array[4] = new int[]{19, 0, 0, 0, 20};
//LayoutPresentation.PrintLayout(new Layout(Layout.array, 20), new HashSet<int>(){1, 3, 7, 10, 11, 12}, new HashSet<int>(){8, 9});
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
