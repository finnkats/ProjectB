App.Start();
Console.Clear();
if (args.Length != 0) App.CurrentMenu = App.ExampleMenu1;
while (App.CurrentMenu != null){
    Console.Write(App.CurrentMenu.MenuString());
    // input will be 0 if the the input is not an integer
    Int32.TryParse(Console.ReadLine(), out int input);
    Action? function = App.CurrentMenu.GetFunction(input);
    if (function != null) function();
    Console.Clear();
}

Console.WriteLine("Exiting program...");
