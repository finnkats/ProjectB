public static class NotificationLogic {
    public static (string, string) UpdatePrintInfo(){
        string locationId = App.Accounts[App.LoggedInUsername].Location;
        string location = locationId == "null" ? "No location" : App.Locations[locationId].Name;
        List<string> genres = App.Accounts[App.LoggedInUsername].Genres;
        for (int i = 0; i < genres.Count; i++){
            genres[i] = App.Genres[genres[i]].Name;
        }
        genres.Sort();
        string seperator = ", ";
        return (location, $"[{String.Join(seperator, genres)}]");
    }

    public static void ChangeLocation(){
        App.Accounts[App.LoggedInUsername].Location = App.locationPresentation.GetItem("What location are you interested in", "No location");
        DataAccess.UpdateItem<Account>();
    }

    public static void ChangeGenres(){
        App.Accounts[App.LoggedInUsername].Genres = App.genrePresentation.GetItemList();
        DataAccess.UpdateItem<Account>();
    }

    public static string GetString(){
        if (App.LoggedInUsername == "Unknown") return "Notifications (0)";
        return $"Notifications ({App.Notifications[App.LoggedInUsername].Count})";
    }

    public static void UpdateNotificationOption(){
        string before = GetString();
        App.FrontPage.RemoveAllOption(before);
        App.FrontPage.RemoveCurrentOption(before);

        DataAccess.UpdateList<Notification>();

        string after = GetString();
        App.FrontPage.AddAllOption(after, NotificationPresentation.NotificationMenu);
        App.FrontPage.AddCurrentOption(after);
    }
}
