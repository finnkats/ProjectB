public static class AccountPresentation{
    public static bool CheckLoggedIn(){
        if (App.LoggedInUsername != "Unknown")
        {
            Console.WriteLine("You are already logged in as: " + App.LoggedInUsername);
            return true; // Prevent further login attempts
        }
        return false;
    }

    public static (string, string) GetLoginDetails(){
        Console.Clear();
        Console.WriteLine("Name: ");
        string? loginName = Console.ReadLine();

        Console.WriteLine();

        Console.WriteLine("Password:");
        string? loginPassword = Console.ReadLine();
        return (loginName ?? "null", loginPassword ?? "null");
    }

    public static void PrintSuccess(string loginString){
        Console.WriteLine("\nLogin successful!");
        Thread.Sleep(1500);
        Console.WriteLine(loginString);
        Thread.Sleep(2000);
    }

    public static bool LoginFailure(){
        Console.Clear();
        Console.WriteLine("Invalid name or password.\nDo you want to try again? (y/n)");
        string input = Console.ReadLine()?.ToLower() ?? "n";
        return input.StartsWith('y');
    }

    public static void PrintLogout(){
        Console.Clear();
        Console.WriteLine($"Logging out user: {App.LoggedInUsername}");
        Thread.Sleep(1500);
    }

    public static bool DoubleCheckPassword(string? password){
        Console.WriteLine("\nConfirm Password:");
        if (Console.ReadLine() != password){
            Console.WriteLine("Password is incorrect.");
            Thread.Sleep(2000);
            return false;
        }
        return true;
    }

    public static void PrintMessage(string message){
        Console.WriteLine(message);
        Thread.Sleep(2000);
    }
}
