using Logic;

public static class App
{
    public static string LoggedInUsername { get; set; } = "Unknown";

    public static Menu? CurrentMenu;

    public static Dictionary<string, Performance> Performances = DataAccess.ReadItem<Performance>();
    public static readonly PerformanceLogic performanceLogic = new PerformanceLogic();
    public static readonly PerformancePresentation performancePresentation = new PerformancePresentation(performanceLogic);

    public static Dictionary<string, Location> Locations = DataAccess.ReadItem<Location>();
    public static LocationLogic locationLogic = new LocationLogic();
    public static LocationPresentation locationPresentation = new LocationPresentation(locationLogic);

    public static Dictionary<string, Hall> Halls = DataAccess.ReadItem<Hall>();
    public static HallLogic hallLogic = new HallLogic();
    public static HallPresentation hallPresentation = new HallPresentation(hallLogic);

    public static Dictionary<string, Genre> Genres = DataAccess.ReadItem<Genre>();
    public static readonly GenreLogic genreLogic = new GenreLogic();
    public static readonly GenrePresentation genrePresentation = new GenrePresentation(genreLogic);


    public static Dictionary<string, Account> Accounts = DataAccess.ReadItem<Account>();
    public static Dictionary<string, List<Play>> Plays = DataAccess.ReadList<Play>();
    public static Dictionary<string,List<Ticket>> Tickets = DataAccess.ReadList<Ticket>();

    public static void Start()
    {
        // Fill in all Menu's
        CreateMenus();
        MainTicketSystem.CheckOutdatedTickets();
    }

    // Add new menu's here
    public static Menu FrontPage = new("Front Page");
    public static Menu SignInUp = new("Sign in / up");
    public static Menu HomePage = new("Home Page");
    public static Menu AdminFeatures = new("Admin Features");
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
        HomePage.AddAllOption("View Performances", performanceLogic.PerformanceCatalogue);
        HomePage.AddAllOption("View Tickets", TicketPresentation.TicketMenu);
        HomePage.AddAllOption("View Notifications", Example.DoNothing); // TODO add view notification function
        HomePage.AddAllOption("Edit Account Settings", Example.DoNothing); // TODO add account settings function
        HomePage.AddAllOption("Admin Features", AdminFeatures.SetToCurrentMenu);
        HomePage.AddCurrentOption("View Performances");

        // Admin Features
        AdminFeatures.PreviousMenu = HomePage;
        AdminFeatures.AddAllOption("Modify Performances", performancePresentation.EditPerformanceStart);
        AdminFeatures.AddAllOption("Modify Genres", genrePresentation.EditGenreStart);
        AdminFeatures.AddAllOption("Modify Locations", locationPresentation.EditLocationStart);
        AdminFeatures.AddAllOption("Modify Halls", hallPresentation.EditHallStart);
        AdminFeatures.AddAllOption("Check Statistics", Example.DoNothing); // TODO add statistic function
        AdminFeatures.AddCurrentOption("Modify Performances");
        AdminFeatures.AddCurrentOption("Modify Genres");
        AdminFeatures.AddCurrentOption("Modify Halls");
        AdminFeatures.AddCurrentOption("Modify Locations");
        AdminFeatures.AddCurrentOption("Check Statistics");

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
