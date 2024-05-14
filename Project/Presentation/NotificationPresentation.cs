public static class NotificationPresentation {
    public static void AccountSettings(){
        (string location, string genres) = NotificationLogic.UpdatePrintInfo();
        while (true){
            Console.Clear();
            Console.WriteLine($"What would you like to change?");
            Console.WriteLine($"1: Change location".PadRight(30) + location);
            Console.WriteLine($"2: Change interested genres".PadRight(30) + genres);
            Console.WriteLine("3: Exit");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 0){
                Console.WriteLine("Not a valid choice");
                continue;
            }
            else if (choice == 1){
                NotificationLogic.ChangeGenres();
            }
            else if (choice == 2){
                NotificationLogic.ChangeLocation();
            }
            else if (choice == 3){
                Console.WriteLine("\nExiting...");
                return;
            }
        }
    }

    public static void NotificationMenu(){
        string printError = "";
        if (App.Accounts[App.LoggedInUsername].Genres.Count == 0) printError += "Please add interested genres in Account Settings\n";
        if (App.Accounts[App.LoggedInUsername].Location == "null") printError += "Please add a location in Account Settings\n";
        // if user still has unread notifications, let them read notifications
        if (App.Notifications[App.LoggedInUsername].Count != 0) printError = "";
        if (printError != ""){
            Console.WriteLine(printError);
            Thread.Sleep(3000);
            return;
        }
        (string location, string genres) = NotificationLogic.UpdatePrintInfo();
        while (true){
            Console.Clear();
            Console.WriteLine($"{location} | {genres}\n");
            int index = 1;
            foreach (Notification notification in App.Notifications[App.LoggedInUsername]){
                Console.WriteLine($"{index++}: {notification.Text} ({notification.Time})");
            }
            Console.WriteLine($"{index}: Exit\n");
            Console.WriteLine("Choose a notification to remove (read)");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice <= 0 || choice > index){
                Console.WriteLine("Invalid choice");
                Thread.Sleep(1000);
                continue;
            } else if (choice == index){
                NotificationLogic.UpdateNotificationOption();
                Console.WriteLine("\nExiting...");
                Thread.Sleep(1500);
                return;
            } else {
                App.Notifications[App.LoggedInUsername].RemoveAt(index);
            }
        }
    }
}
