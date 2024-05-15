public static class NotificationPresentation {
    public static void AccountSettings(){
        while (true){
            (string location, string genres) = NotificationLogic.UpdatePrintInfo();
            Console.Clear();
            Console.WriteLine($"What would you like to change?");
            Console.WriteLine($"1: Change location".PadRight(30) + location);
            Console.WriteLine($"2: Change interested genres".PadRight(30) + genres);
            Console.Write("3: Exit\n> ");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice == 0){
                Console.WriteLine("Not a valid choice");
                continue;
            }
            else if (choice == 1){
                NotificationLogic.ChangeLocation();
            }
            else if (choice == 2){
                NotificationLogic.ChangeGenres();
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
        int originalNotificationCount = App.Notifications[App.LoggedInUsername].Count;
        while (true){
            Console.Clear();
            Console.WriteLine("Your preferences:");
            Console.WriteLine($"{location} | {genres}\n");
            Console.WriteLine("Notifications:");
            if (App.Notifications[App.LoggedInUsername].Count == 0) Console.WriteLine("No notifications");
            int index = 1;
            foreach (Notification notification in App.Notifications[App.LoggedInUsername]){
                Console.WriteLine($"{index++}: {notification.ToString()}");
            }
            Console.WriteLine($"\n{index}: Exit\n");
            Console.Write("Choose a notification to remove (read)\n> ");
            Int32.TryParse(Console.ReadLine(), out int choice);
            if (choice <= 0 || choice > index){
                Console.WriteLine("Invalid choice");
                Thread.Sleep(1000);
                continue;
            } else if (choice == index){
                NotificationLogic.UpdateNotificationOption(true, originalNotificationCount);
                Console.WriteLine("\nExiting...");
                Thread.Sleep(1500);
                return;
            } else {
                App.Notifications[App.LoggedInUsername].RemoveAt(choice - 1);
            }
        }
    }
}
