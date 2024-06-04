public class Menu
{
    public string Name { get; }
    public Menu(string name) => Name = name;

    // Dictionary with all options the menu will ever have (so also currently hidden options) as key,
    // and the function (without parameters) they will call as value.
    private Dictionary<string, Action> AllOptions = new();
    // List of options which should "currently" be able to be accessed.
    private List<string> CurrentOptions = new();
    // The menu this menu was accessed by
    public Menu? PreviousMenu = null;

    public void AddAllOption(string optionName, Action optionFunction)
    {
        try
        {
            AllOptions.Add(optionName, optionFunction);
        }
        catch (ArgumentException) { }
    }

    public void RemoveAllOption(string optionName) => AllOptions.Remove(optionName);

    public void AddCurrentOption(string optionName)
    {
        // Dont allow duplicate options or options which arent in AllOptions
        if (CurrentOptions.Contains(optionName)) return;
        if (AllOptions.ContainsKey(optionName)) CurrentOptions.Add(optionName);
    }

    public void RemoveCurrentOption(string optionName) => CurrentOptions.Remove(optionName);

    public string MenuString()
    {
        string menuString = "";

        if (Name == "Front Page"){
            menuString +=
            @"   _____ _                  _                   _  ___     _     " + "\n" +
            @"  / ____(_)                (_)                 | |/ (_)   | |    " + "\n" +
            @" | |     _ _ __   _____   ___  _____      __   | ' / _  __| |___ " + "\n" +
            @" | |    | | '_ \ / _ \ \ / / |/ _ \ \ /\ / /   |  < | |/ _` / __|" + "\n" +
            @" | |____| | | | |  __/\ V /| |  __/\ V  V /    | . \| | (_| \__ \" + "\n" +
            @"  \_____|_|_| |_|\___| \_/ |_|\___| \_/\_/     |_|\_\_|\__,_|___/" + "\n" +
            @"                                                                 " + "\n"
            ;
        }

        // Include logged-in username if available
        if (App.LoggedInUsername != "Unknown")
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
        menuString += $"E: Exit\n";
        return menuString;
    }


    public Action? GetFunction(string input)
    {
        // If input is the last item + 1 (Exit), which isn't in CurrentOptions
        int ParsedInput = 0;
        if (input.ToLower() == "e") ParsedInput = -1;
        else ParsedInput = int.Parse(input);

        if (ParsedInput == -1) return this.SetToPreviousMenu;
        // Menu input starts at 1, so if input is 1, then index should be 0
        try
        {
            Action function = AllOptions[CurrentOptions[ParsedInput - 1]];
            return function;
        }
        catch (ArgumentOutOfRangeException) { return null; }
    }

    public void SetToPreviousMenu() => App.CurrentMenu = PreviousMenu;
    public void SetToCurrentMenu() => App.CurrentMenu = this;
}
