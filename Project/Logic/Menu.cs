public class Menu{
    public string Name {get;}
    public Menu(string name) => Name = name;

    // Dictionary with all options the menu will ever have (so also currently hidden options) as key,
    // and the function (without parameters) they will call as value.
    private Dictionary<string, Action> AllOptions = new();
    // List of options which should "currently" be able to be accessed.
    private List<string> CurrentOptions = new();
    // The menu this menu was accessed by

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

    public string MenuString(){
        string menuString = $"{Name}\n\n";
        int index = 1;
        CurrentOptions.ForEach(option => menuString += $"{index++}: {option}\n");
        return menuString;
    }
}
