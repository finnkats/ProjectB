using System;
using Logic;

public static class TicketLoginPresentation{

    public static bool ChooseLoginOption(){
        while (true){
            Console.Clear();
            Console.WriteLine("Front Page -> Home Page -> View Performances\n");
            Console.WriteLine("You're not logged in, please choose to log in or to create an account");
            Console.WriteLine("1. Log In");
            Console.WriteLine("2. Create Account");
            Console.Write("E. Exit\n\n> ");
            string? inputOption = Console.ReadLine() ?? "";
            if (inputOption.ToLower() == "e"){
                Console.Clear();
                Console.WriteLine("Stop buying process...");
                Thread.Sleep(2000);
                return false;
            }
            bool isInt = int.TryParse(inputOption, out int option);
            if (isInt){
                if (option == 1){
                    bool isAdmin;
                    string loginName, loginPassword;
                    (isAdmin, loginName, loginPassword) = MainTicketSystem.LoginCheckAdmin();
                    if(isAdmin){
                        Console.Clear();
                        Console.WriteLine("Cannot use Admin account to buy tickets");
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                    }
                    else{
                        AccountLogic.Login(loginName, loginPassword);
                        if (App.LoggedInUsername != "Unknown") return true;
                    }
                }
                else if(option == 2){
                    AccountLogic.CreateAccount();
                    if (App.LoggedInUsername != "Unknown") return true;
                }
                else{
                    Console.Clear();
                    Console.WriteLine("This is not an option");
                    Thread.Sleep(1500);
                }
            }
            else{
                Console.Clear();
                Console.WriteLine("Invalid input");
                Thread.Sleep(1500);
            }
        }
    }
}
