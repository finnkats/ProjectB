using System.Text;

public static class AccountPresentation
{
    public static bool CheckLoggedIn()
    {
        if (App.LoggedInUsername != "Unknown")
        {
            Console.WriteLine("You are already logged in as: " + App.LoggedInUsername);
            return true; // Prevent further login attempts
        }
        return false;
    }

    private static string MaskPasswordInput()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password.Append(key.KeyChar);
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }

    public static (string, string) GetLoginDetails()
    {
        Console.Clear();
        Console.WriteLine("Name: ");
        string? loginName = Console.ReadLine();

        Console.WriteLine();

        Console.WriteLine("Password:");
        string loginPassword = MaskPasswordInput();
        return (loginName ?? "null", loginPassword);
    }

    public static void PrintSuccess(string loginString)
    {
        Console.WriteLine("\nLogin successful!");
        Thread.Sleep(1500);
        Console.WriteLine(loginString);
        Thread.Sleep(2000);
    }

    public static bool LoginFailure()
    {
        Console.Clear();
        Console.WriteLine("Invalid name or password.\nDo you want to try again? (y/n)");
        string input = Console.ReadLine()?.ToLower() ?? "n";
        return input.StartsWith('y');
    }

    public static void PrintLogout()
    {
        Console.Clear();
        Console.WriteLine($"Logging out user: {App.LoggedInUsername}");
        Thread.Sleep(1500);
    }

    public static bool DoubleCheckPassword(string? password)
    {
        Console.WriteLine("\nConfirm Password:");
        string confirmedPassword = MaskPasswordInput(); // Get the confirmed password

        if (confirmedPassword != password)
        {
            Console.WriteLine("Passwords is not correct.");
            Thread.Sleep(2000);
            return false;
        }

        return true;
    }


    public static void PrintMessage(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(2000);
    }
}
