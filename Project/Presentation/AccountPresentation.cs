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
        // Initialize a StringBuilder to store the password
        StringBuilder password = new StringBuilder();

        // Declare a variable to store the key pressed by the user
        ConsoleKeyInfo key;

        // Loop until the user presses the Enter key
        do
        {
            // Read a single key from the console without displaying it
            key = Console.ReadKey(true);

            // Check if the entered key is a valid character (letter or number)
            if (char.IsLetterOrDigit(key.KeyChar))
            {
                // Append the character to the password StringBuilder
                password.Append(key.KeyChar);

                // Display an asterisk (*) on the console to mask the input
                Console.Write("*");
            }
            // Check if the Backspace key is pressed and the password is not empty
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                // Remove the last character from the password
                password.Remove(password.Length - 1, 1);

                // Move the cursor back one position to overwrite the character with a space
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter); // Repeat until the Enter key is pressed

        // Move the cursor to the next line
        Console.WriteLine();

        // Return the password as a string
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
        Console.WriteLine();
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
        Console.WriteLine("Confirm Password:");
        string confirmedPassword = MaskPasswordInput(); // Get the confirmed password


        if (confirmedPassword != password)
        {
            Console.WriteLine("Password is not correct.");
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
