public static class App{
    // Initialize objects that will be used in the app
    public static Menu? CurrentMenu;

    // Add new menu's here
        // real menu placeholder
    public static Menu ExampleMenu1 = new("Example Menu 1");

    public static void Start(){
        // First menu the program will use
        CurrentMenu = ExampleMenu1;


        // Fill up objects

        //  real menu placeholder

        //  Example Menu 1
        ExampleMenu1.AddAllOption("Boilerplate Option 1", Example.Menu1AddOption3);
        ExampleMenu1.AddAllOption("Boilerplate Option 2", Example.Menu1RemoveOption3);
        ExampleMenu1.AddAllOption("Boilerplate Option 3", Example.DoNothing);
        ExampleMenu1.AddCurrentOption("Boilerplate Option 1");
        ExampleMenu1.AddCurrentOption("Boilerplate Option 2");
    }
}
