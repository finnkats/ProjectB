App.Start();
Console.Clear();
// Turn on "Demo" mode if any program arguments are given
if (args.Length != 0) App.AddAllMenus();
while (App.CurrentMenu != null){
    Console.Write(App.CurrentMenu.MenuString());
    // input will be 0 if the the input is not an integer
    Int32.TryParse(Console.ReadLine(), out int input);
    Action? function = App.CurrentMenu.GetFunction(input);
    if (function != null) function();
    Console.Clear();
}

Console.WriteLine("Exiting program...");
