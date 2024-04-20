using Logic;

App.Start();
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

foreach (var item in PerformanceLogic.GetPerformanceOptions(true))
{
    Console.WriteLine(item);
}
Console.WriteLine("Exiting program...");
