App.Start();

// for now
int i = 0;
bool run = true;
if (App.CurrentMenu == null) return;
while (run){
    Console.Write(App.CurrentMenu.MenuString());
    // input will be 0 if the the input is not an integer
    Int32.TryParse(Console.ReadLine(), out int input);
    Action? function = App.CurrentMenu.GetFunction(input);
    if (function != null) function();
    Console.WriteLine();
    if (i++ == 10) run = false;
}
