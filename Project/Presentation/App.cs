using Logic;
using System.Runtime.InteropServices;
using System.IO;

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
    public static Dictionary<string, List<ArchivedPlay>> ArchivedPlays = DataAccess.ReadList<ArchivedPlay>();
    public static Dictionary<string,List<Ticket>> Tickets = DataAccess.ReadList<Ticket>();
    public static Dictionary<string, List<Notification>> Notifications = DataAccess.ReadList<Notification>();

    public static void Start()
    {
        // Fill in all Menu's
        CreateMenus();
        MainTicketSystem.CheckOutdatedTickets();
        PlayLogic.RemoveOutdatedPlays();
        IEnumerable<int> ticketCount = App.Tickets.Select(user => user.Value.Count);
        Ticket.CurrentOrderNumber = ticketCount.Count() == 0 ? 1 : ticketCount.Sum() + 1;
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
        FrontPage.AddAllOption("Admin Features", AdminFeatures.SetToCurrentMenu);
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
        HomePage.AddAllOption("View Orders", TicketPresentation.TicketMenu);
        HomePage.AddAllOption("Edit Account Settings", NotificationPresentation.AccountSettings); // TODO add account settings function
        HomePage.AddCurrentOption("View Performances");

        // Admin Features
        AdminFeatures.PreviousMenu = FrontPage;
        AdminFeatures.AddAllOption("Edit/Add Performances", performancePresentation.EditPerformanceStart);
        AdminFeatures.AddAllOption("Edit/Add Genres", genrePresentation.EditGenreStart);
        AdminFeatures.AddAllOption("Edit/Add Locations", locationPresentation.EditLocationStart);
        AdminFeatures.AddAllOption("Edit/Add Halls", hallPresentation.EditHallStart);
        AdminFeatures.AddAllOption("Open Log Folders", OpenLogFolder); // TODO add statistic function
        AdminFeatures.AddCurrentOption("Edit/Add Performances");
        AdminFeatures.AddCurrentOption("Edit/Add Genres");
        AdminFeatures.AddCurrentOption("Edit/Add Halls");
        AdminFeatures.AddCurrentOption("Edit/Add Locations");
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
        HomePage.AddCurrentOption("View Orders");
        HomePage.AddCurrentOption("View Notifications");
        HomePage.AddCurrentOption("Edit Account Settings");
        HomePage.AddCurrentOption("Admin Features");
    }

        public static void OpenLogFolder(){
        string path = Directory.GetCurrentDirectory();
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && !RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
            Console.WriteLine("OS is not supported");
            Thread.Sleep(2500);
            return;
        }
        path += RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? @"\DataSources\LogFiles" : @"/DataSources/LogFiles";

        if (!Directory.Exists(path)){
            Console.WriteLine($"Folder {path} doesn't exist");
            Thread.Sleep(2000);
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) System.Diagnostics.Process.Start("explorer.exe", path);
        else System.Diagnostics.Process.Start("open", path);
    }
}
