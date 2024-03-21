public class Menu{
    public string Name {get;}
    public Menu(string name) => Name = name;

    // Dictionary with all options the menu will ever have (so also currently hidden options) as key,
    // and the function (without parameters) they will call as value.
    private Dictionary<string, Action> AllOptions = new();
    // List of options which should "currently" be able to be accessed.
    private List<string> CurrentOptions = new();
    // The menu this menu was accessed by
    public Menu? PreviousMenu = null;

    public void AddAllOption(string optionName, Action optionFunction){
        try {
            AllOptions.Add(optionName, optionFunction);
        } catch (ArgumentException) {}
    }

    public void AddCurrentOption(string optionName){
        // Dont allow duplicate options or options which arent in AllOptions
        if (CurrentOptions.Contains(optionName)) return;
        if (AllOptions.ContainsKey(optionName)) CurrentOptions.Add(optionName);
    }

    public void RemoveCurrentOption(string optionName) => CurrentOptions.Remove(optionName);

    public string MenuString()
{
    string menuString = "";
    // Include logged-in username if available
    if (!string.IsNullOrEmpty(App.LoggedInUsername))
    {
        menuString += $"Logged in as: {App.LoggedInUsername}\n\n";
    }

    string menuPath = $"{Name}\n";
    Menu? pointer = PreviousMenu;
    while (pointer != null)
    {
        menuPath = $"{pointer.Name} -> " + menuPath;
        pointer = pointer.PreviousMenu;
    }
    menuString += menuPath;

    int index = 1;
    CurrentOptions.ForEach(option => menuString += $"{index++}: {option}\n");
    // Add Exit option at the end
    menuString += $"{index}: Exit\n";
    return menuString;
}


    public Action? GetFunction(int input){
        // If input is the last item + 1 (Exit), which isnt in CurrentOptions
        if (input == CurrentOptions.Count() + 1) return this.SetToPreviousMenu;
        // Menu input starts at 1, so if input is 1, then index should be 0
        try {
            Action function = AllOptions[CurrentOptions[input - 1]];
            return function;
        } catch (ArgumentOutOfRangeException) { return null; }
    }

    public void SetToPreviousMenu() => App.CurrentMenu = PreviousMenu;
    public void SetToCurrentMenu() => App.CurrentMenu = this;
}
