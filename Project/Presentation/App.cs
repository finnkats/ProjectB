using Logic;

public static class App
{
    public static string LoggedInUsername { get; set; } = "Unknown";

    public static Menu? CurrentMenu;

    public static readonly Dictionary<string, Performance> Performances = PerformanceDataAccess.ReadPerformances();
    public static readonly Dictionary<string, Account> Accounts = AccountDataAccess.ReadAccounts();
    public static readonly Dictionary<string, List<Play>> Plays = PlayDataAccess.ReadPlays();
    public static readonly List<UserTicket> Tickets = TicketDataAccess.ReadTickets();
    public static readonly Dictionary<string, Location> Locations = LocationDataAccess.ReadLocations();
    public static readonly Dictionary<string, Hall> Halls = HallDataAccess.ReadHalls();

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
        SignInUp.AddAllOption("Sign up", AccountLogic.CreateAccount);
        SignInUp.AddCurrentOption("Sign in");
        SignInUp.AddCurrentOption("Sign up");

        //  Home Page
        HomePage.PreviousMenu = FrontPage;
        HomePage.AddAllOption("View Performances", PerformanceLogic.PerformanceCatalogue); // TODO add view Performance function
        HomePage.AddAllOption("View Tickets", TicketPresentation.PrintTickets); // TODO add view ticket function // for now linked to ticket system
        HomePage.AddAllOption("View Notifications", Example.DoNothing); // TODO add view notification function
        HomePage.AddAllOption("Edit Account Settings", Example.DoNothing); // TODO add account settings function
        HomePage.AddAllOption("Admin Features", AdminFeatures.SetToCurrentMenu);
        HomePage.AddCurrentOption("View Performances");

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
        ModifyPerformances.AddAllOption("Add Performance", PerformancePresentation.AddPerformance);
        ModifyPerformances.AddAllOption("Edit Performance", PerformancePresentation.EditPerformanceStart);
        ModifyPerformances.AddAllOption("Add Play", PlayPresentation.AddPlayDetails);
        ModifyPerformances.AddCurrentOption("Add Performance");
        ModifyPerformances.AddCurrentOption("Edit Performance");
        ModifyPerformances.AddCurrentOption("Add Play");

        // Modify Categories
        ModifyCategories.PreviousMenu = AdminFeatures;
        ModifyCategories.AddAllOption("Add Category", Example.DoNothing); // TODO add add category function
        ModifyCategories.AddAllOption("Edit Category", Example.DoNothing); // TODO add edit category function
        ModifyCategories.AddCurrentOption("Add Category");
        ModifyCategories.AddCurrentOption("Edit Category");

        // Modify Locations
        ModifyLocations.PreviousMenu = AdminFeatures;
        ModifyLocations.AddAllOption("Add Location", LocationPresentation.AddLocation); // TODO add add category function
        ModifyLocations.AddAllOption("Edit Location", LocationPresentation.EditLocationStart); // TODO add edit category function
        ModifyLocations.AddAllOption("Add Hall", HallPresentation.AddHall); // TODO add add hall function
        ModifyLocations.AddAllOption("Edit Hall", Example.DoNothing); // TODO add edit hall function
        ModifyLocations.AddCurrentOption("Add Location");
        ModifyLocations.AddCurrentOption("Edit Location");
        ModifyLocations.AddCurrentOption("Add Hall");
        ModifyLocations.AddCurrentOption("Edit Hall");

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
