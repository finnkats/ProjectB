public static class NotificationLogic {
    public static (string, string) UpdatePrintInfo(){
        string locationId = App.Accounts[App.LoggedInUsername].Location;
        string location = locationId == "null" ? "No location" : App.Locations[locationId].Name;
        List<string> genres = new();
        foreach (string id in App.Accounts[App.LoggedInUsername].Genres){
            genres.Add(App.Genres[id].Name);
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
        if (App.LoggedInUsername == "Unknown") return "View Notifications (0)";
        return $"View Notifications ({App.Notifications[App.LoggedInUsername].Count})";
    }

    public static void UpdateNotificationOption(bool add){
        string before = GetString();
        App.HomePage.RemoveAllOption(before);
        App.HomePage.RemoveCurrentOption(before);

        DataAccess.UpdateList<Notification>();

        if (add){
            string after = GetString();
            App.HomePage.AddAllOption(after, NotificationPresentation.NotificationMenu);
            App.HomePage.AddCurrentOption(after);
        }
    }
}
