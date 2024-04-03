using Logic;

public static class App
{
    public static string LoggedInUsername { get; set; } = "Unknown";
    public static Menu? CurrentMenu;

    public static void Start()
    {
        // Fill in all Menu's
        CreateMenus();
    }

    // Add new menu's here
    public static Menu FrontPage = new("Front Page");
    public static Menu SignInUp = new("Sign in / up");
    public static Menu HomePage = new("Home Page");
    public static Menu AdminFeatures = new("Admin Features");
    public static Menu ModifyPerformances = new("Modify Performances");
    public static Menu ModifyCategories = new("Modify Categories");
    public static Menu ModifyLocations = new("Modify Locations");
    public static Menu EditLocation = new("Edit Location");
    public static Menu ExampleMenu1 = new("Example Menu 1");

    public static void CreateMenus()
    {
        // First menu the program will use
        CurrentMenu = FrontPage;

        // Fill up menu's

        //  Front Page
        FrontPage.AddAllOption("Home Page", HomePage.SetToCurrentMenu);
        FrontPage.AddAllOption("Sign in / up", SignInUp.SetToCurrentMenu);
        FrontPage.AddAllOption("Logout", AccountLogic.Logout);
        FrontPage.AddAllOption("Example Menu", ExampleMenu1.SetToCurrentMenu);
        FrontPage.AddCurrentOption("Home Page");
        FrontPage.AddCurrentOption("Sign in / up");

        //  Sign in / up
        SignInUp.PreviousMenu = FrontPage;
        SignInUp.AddAllOption("Sign in", () => AccountLogic.Login());
        SignInUp.AddAllOption("Sign up", AccountLogic.CreateAccount); // TODO add create account function
        SignInUp.AddCurrentOption("Sign in");
        SignInUp.AddCurrentOption("Sign up");

        //  Home Page
        HomePage.PreviousMenu = FrontPage;
        HomePage.AddAllOption("View Plays", Example.DoNothing); // TODO add view Play function
        HomePage.AddAllOption("View Tickets", () => PlayLogic.Choose("ID1")); // TODO add view ticket function // for now linked to ticket system
        HomePage.AddAllOption("View Notifications", Example.DoNothing); // TODO add view notification function
        HomePage.AddAllOption("Edit Account Settings", Example.DoNothing); // TODO add account settings function
        HomePage.AddAllOption("Admin Features", AdminFeatures.SetToCurrentMenu);
        HomePage.AddCurrentOption("View Plays");

        // Admin Features
        AdminFeatures.PreviousMenu = HomePage;
        AdminFeatures.AddAllOption("Modify Performances", ModifyPerformances.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Modify Categories", ModifyCategories.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Modify Locations", ModifyLocations.SetToCurrentMenu);
        AdminFeatures.AddAllOption("Check Statistics", Example.DoNothing); // TODO add statistic function
        AdminFeatures.AddCurrentOption("Modify Performances");
        AdminFeatures.AddCurrentOption("Modify Categories");
        AdminFeatures.AddCurrentOption("Modify Locations");
        AdminFeatures.AddCurrentOption("Check Statistics");

        //  Modify Performances
        ModifyPerformances.PreviousMenu = AdminFeatures;
        ModifyPerformances.AddAllOption("Add Performance", PerformancePresentation.AddPerformance); // TODO add add Play function
        ModifyPerformances.AddAllOption("Edit Performance", PerformancePresentation.EditPerformanceChoice); // TODO add edit Play function
        ModifyPerformances.AddCurrentOption("Add Performance");
        ModifyPerformances.AddCurrentOption("Edit Performance");

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
    public static void AddAllMenus()
    {
        FrontPage.AddCurrentOption("Logout");
        FrontPage.AddCurrentOption("Example Menu");

        HomePage.AddCurrentOption("View Plays");
        HomePage.AddCurrentOption("View Tickets");
        HomePage.AddCurrentOption("View Notifications");
        HomePage.AddCurrentOption("Edit Account Settings");
        HomePage.AddCurrentOption("Admin Features");
    }
}
