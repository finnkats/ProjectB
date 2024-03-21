// Initialize objects that will be used in the app
public static class App{
    public static Menu? CurrentMenu;
    public static void Start(){
        // Fill in all Menu's
        CreateMenus();
    }


    // Add new menu's here
    public static Menu FrontPage = new("Front Page");
    public static Menu SignInUp = new("Sign in / up");
    public static Menu HomePage = new("Home Page");
    public static Menu AdminFeatures = new("Admin Features");
    public static Menu ModifyPlays = new("Modify Plays");
    public static Menu ModifyCategories = new("Modify Categories");
    public static Menu ModifyLocations = new("Modify Locations");
    public static Menu EditLocation = new("Edit Location");
    public static Menu ExampleMenu1 = new("Example Menu 1");

    public static void CreateMenus(){
        // First menu the program will use
        CurrentMenu = FrontPage;

        // Fill up menu's

        //  Front Page
        FrontPage.AddAllOption("Home Page", HomePage.SetToCurrentMenu);
        FrontPage.AddAllOption("Sign in / up", SignInUp.SetToCurrentMenu);
        FrontPage.AddAllOption("Logout", Example.DoNothing); // TODO add logout function
        FrontPage.AddAllOption("Example Menu", ExampleMenu1.SetToCurrentMenu);
        FrontPage.AddCurrentOption("Home Page");
        FrontPage.AddCurrentOption("Sign in / up");
        
        //  Sign in / up
        SignInUp.PreviousMenu = FrontPage;
        SignInUp.AddAllOption("Sign in", Example.DoNothing); // TODO add login function
        SignInUp.AddAllOption("Sign up", Example.DoNothing); // TODO add create account function
        SignInUp.AddCurrentOption("Sign in");
        SignInUp.AddCurrentOption("Sign up");

        //  Home Page
        HomePage.PreviousMenu = FrontPage;
        HomePage.AddAllOption("View Plays", Example.DoNothing); // TODO add view movie function
        HomePage.AddAllOption("View Tickets", Example.DoNothing); // TODO add view ticket function
        HomePage.AddAllOption("View Notification", Example.DoNothing); // TODO add view notification function
        HomePage.AddAllOption("Edit Account Settings", Example.DoNothing); // TODO add account settings function
        HomePage.AddAllOption("Admin Features", AdminFeatures.SetToCurrentMenu);
        HomePage.AddCurrentOption("View Plays");

        // Admin Features
        AdminFeatures.PreviousMenu = HomePage;
        AdminFeatures.AddAllOption("Modify Plays", ModifyPlays.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Modify Categories", ModifyCategories.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Modify Locations", ModifyLocations.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Check Statistics", Example.DoNothing); // TODO add statistic function
        AdminFeatures.AddCurrentOption("Modify Plays");
        AdminFeatures.AddCurrentOption("Modify Categories");
        AdminFeatures.AddCurrentOption("Modify Locations");
        AdminFeatures.AddCurrentOption("Check Statistics");

        //  Modify Plays
        ModifyPlays.PreviousMenu = AdminFeatures;
        ModifyPlays.AddAllOption("Add Play", Example.DoNothing); // TODO add add movie function
        ModifyPlays.AddAllOption("Edit Play", Example.DoNothing); // TODO add edit movie function
        ModifyPlays.AddCurrentOption("Add Play");
        ModifyPlays.AddCurrentOption("Edit Play");

        // Modify Categories
        ModifyCategories.PreviousMenu = AdminFeatures;
        ModifyCategories.AddAllOption("Add Category", Example.DoNothing); // TODO add add category function
        ModifyCategories.AddAllOption("Edit Category", Example.DoNothing); // TODO add edit category function
        ModifyCategories.AddCurrentOption("Add Category");
        ModifyCategories.AddCurrentOption("Edit Category");

        // Modify Locations
        ModifyLocations.PreviousMenu = AdminFeatures;
        ModifyLocations.AddAllOption("Add Location", Example.DoNothing); // TODO add add category function
        ModifyLocations.AddAllOption("Edit Location", EditLocation.SetToCurrentMenu); // TODO add edit category function
        ModifyLocations.AddCurrentOption("Add Location");
        ModifyLocations.AddCurrentOption("Edit Location");

        // Edit Location
        EditLocation.PreviousMenu = ModifyLocations;
        EditLocation.AddAllOption("Add Hall", Example.DoNothing); // TODO add add hall function
        EditLocation.AddAllOption("Edit Hall", Example.DoNothing); // TODO add edit hall function
        EditLocation.AddCurrentOption("Add Hall");
        EditLocation.AddCurrentOption("Edit Hall");

        //  Example Menu 1
        ExampleMenu1.PreviousMenu = FrontPage;
        ExampleMenu1.AddAllOption("Boilerplate Option 1", Example.Menu1AddOption3);
        ExampleMenu1.AddAllOption("Boilerplate Option 2", Example.Menu1RemoveOption3);
        ExampleMenu1.AddAllOption("Boilerplate Option 3", Example.DoNothing);
        ExampleMenu1.AddCurrentOption("Boilerplate Option 1");
        ExampleMenu1.AddCurrentOption("Boilerplate Option 2");
    }

    // Adds all "hidden" menu's, for demo
    public static void AddAllMenus(){
        FrontPage.AddCurrentOption("Logout");
        FrontPage.AddCurrentOption("Example Menu");

        HomePage.AddCurrentOption("View Movies");
        HomePage.AddCurrentOption("View Tickets");
        HomePage.AddCurrentOption("View Notifications");
        HomePage.AddCurrentOption("Edit Account Settings");
        HomePage.AddCurrentOption("Admin Features");
    }
}
