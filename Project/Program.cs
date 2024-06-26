﻿using Logic;

App.Start();

Console.Clear();
// program arguments
if (args.Length != 0) {
    if (args[0].ToLower() == "demo") App.AddAllMenus();
    else if (args[0].ToLower() == "admin") AccountLogic.Login("Admin123", "Password123");
    else if (args[0].ToLower() == "soufiane") AccountLogic.Login("Soufiane", "password");
}

bool invalidInput = false;
string? menuString;
while (App.CurrentMenu != null){
    menuString = App.CurrentMenu.MenuString();
    menuString += (invalidInput ? "\nInvalid input\n" : "\n") + "> ";
    Console.Write(menuString);
    // input will be 0 if the the input is not an integer
    string input = Console.ReadLine() ?? "";
    Action? function = App.CurrentMenu.GetFunction(input);
    if (function != null) {
        function();
        invalidInput = false;
    }
    else invalidInput = true;
    Console.Clear();
}
Console.WriteLine("Exiting program...");