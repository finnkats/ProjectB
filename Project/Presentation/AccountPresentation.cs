public static class AccountPresentation{
    public static bool CheckLoggedIn(){
        if (!string.IsNullOrEmpty(App.LoggedInUsername))
        {
            Console.WriteLine("You are already logged in as: " + App.LoggedInUsername);
            return true; // Prevent further login attempts
        }
        return false;
    }

    public static (string?, string?) GetLoginDetails(){
        Console.WriteLine("Name: ");
        string? loginName = Console.ReadLine();

        Console.WriteLine();

        Console.WriteLine("Password:");
        string? loginPassword = Console.ReadLine();
        return (loginName, loginPassword);
    }

    public static void PrintSuccess(string loginString){
        Console.WriteLine("\nLogin successful!");
        Thread.Sleep(1500);
        Console.WriteLine(loginString);
        Thread.Sleep(2000);
    }

    public static void LoginFailure() => Console.WriteLine("Invalid name or password. Please try again.\n");
}
