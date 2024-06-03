public static class NotificationPresentation {
    public static void AccountSettings(){
        while (true){
            (string location, string genres) = NotificationLogic.UpdatePrintInfo();
            Console.Clear();
            Console.WriteLine("Front Page -> Home Page -> Edit Account Settings\n");
            Console.WriteLine($"What would you like to change?");
            Console.WriteLine($"1: Change location".PadRight(30) + location);
            Console.WriteLine($"2: Change interested genres".PadRight(30) + genres);
            Console.Write("E: Exit\n> ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "e"){
                Console.WriteLine("\nExiting...");
                return;
            }
            Int32.TryParse(choice, out int choiceInt);
            if (choiceInt == 0){
                Console.WriteLine("Not a valid choice");
                continue;
            }
            else if (choiceInt == 1){
                NotificationLogic.ChangeLocation();
            }
            else if (choiceInt == 2){
                NotificationLogic.ChangeGenres();
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
            Console.WriteLine("Front Page -> Home Page -> View Notifications\n");
            Console.WriteLine("Your preferences:");
            Console.WriteLine($"{location} | {genres}\n");
            Console.WriteLine("Notifications:");
            if (App.Notifications[App.LoggedInUsername].Count == 0) Console.WriteLine("No notifications");
            int index = 1;
            foreach (Notification notification in App.Notifications[App.LoggedInUsername]){
                Console.WriteLine($"{index++}: {notification.ToString()}");
            }
            Console.WriteLine($"\nE: Exit\n");
            Console.Write("Choose a notification to remove (read)\n> ");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "e"){
                NotificationLogic.UpdateNotificationOption(true, originalNotificationCount);
                Console.WriteLine("\nExiting...");
                Thread.Sleep(1500);
                return;
            }
            Int32.TryParse(choice, out int choiceInt);
            if (choiceInt <= 0 || choiceInt > index){
                Console.WriteLine("Invalid choice");
                Thread.Sleep(1000);
                continue;
            } else {
                App.Notifications[App.LoggedInUsername].RemoveAt(choiceInt - 1);
            }
        }
    }
}
