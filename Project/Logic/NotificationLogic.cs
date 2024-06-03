public static class NotificationLogic
{
    public static Logger logger = new AccountLogger();
    public static (string, string) UpdatePrintInfo()
    {
        string locationId = App.Accounts[App.LoggedInUsername].Location;
        string location = locationId == "null" ? "No location" : App.Locations[locationId].Name;
        List<string> genres = new();
        foreach (string id in App.Accounts[App.LoggedInUsername].Genres)
        {
            genres.Add(App.Genres[id].Name);
        }
        genres.Sort();
        string seperator = ", ";
        return (location, $"[{String.Join(seperator, genres)}]");
    }

    public static void ChangeLocation()
    {
        var account = App.Accounts[App.LoggedInUsername];
        string oldLocationId = account.Location;
        string oldLocation = oldLocationId == "null" ? "No location" : App.Locations[oldLocationId].Name;
        string newLocationId = App.locationPresentation.GetItem("What location are you interested in", "No location");
        string newLocation = newLocationId == "null" ? "No location" : App.Locations[newLocationId].Name;

        account.Location = newLocationId;
        DataAccess.UpdateItem<Account>();

        logger.LogAction("Updated location preference", new { OldLocation = oldLocation, NewLocation = newLocation });
    }

    public static void ChangeGenres()
    {
        var account = App.Accounts[App.LoggedInUsername];
        var oldGenres = account.Genres.Select(id => App.Genres[id].Name).ToList();
        var newGenreIds = App.genrePresentation.GetItemList();
        var newGenres = newGenreIds.Select(id => App.Genres[id].Name).ToList();

        account.Genres = newGenreIds;
        DataAccess.UpdateItem<Account>();

        logger.LogAction("Updated genre preference", new { OldGenres = string.Join(", ", oldGenres), NewGenres = string.Join(", ", newGenres) });
    }

    public static string GetString(int? num = null)
    {
        int count = 0;
        if (App.LoggedInUsername == "Unknown") count = 0;
        else if (num != null) count = (int)num;
        else count = App.Notifications[App.LoggedInUsername].Count;
        return $"View Notifications ({count})";
    }

    public static void UpdateNotificationOption(bool add, int? original = null)
    {
        string option = GetString(original);
        App.HomePage.RemoveAllOption(option);
        App.HomePage.RemoveCurrentOption(option);

        option = GetString();

        if (add)
        {
            App.HomePage.AddAllOption(option, NotificationPresentation.NotificationMenu);
            App.HomePage.AddCurrentOption(option);
        }

        DataAccess.UpdateList<Notification>();
    }

    public static void SendOutNotifications(Play play)
    {
        Notification notification = new(play.PerformanceId, play.Location);
        foreach (var account in App.Accounts)
        {
            if (account.Value.Location != play.Location) continue;
            foreach (string genre in account.Value.Genres)
            {
                if (App.Performances[play.PerformanceId].Genres.Contains(genre))
                {
                    App.Notifications[account.Key].Add(notification);
                    break;
                }
            }
        }
        DataAccess.UpdateList<Notification>();
    }
}
