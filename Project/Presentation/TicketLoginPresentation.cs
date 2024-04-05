using System;
using Logic;

public static class TicketLoginPresentation{

    public static void ChooseLoginOption(bool loop = false){
        Console.Clear();
        if(!loop){
            Console.WriteLine("You're not logged in, please choose to sign in or to sign up");
        }
        Console.WriteLine("1. Sign in");
        Console.WriteLine("2. Sign up");
        Console.Write("3. Exit/Stop\n\n>");
        string? inputOption = Console.ReadLine();
        bool isInt = int.TryParse(inputOption, out int option);
        if (isInt){
            if (option == 1){
                bool isAdmin;
                string loginName, loginPassword;
                (isAdmin, loginName, loginPassword) = MainTicketSystem.LoginCheckAdmin();
                if(isAdmin){
                    Console.Clear();
                    Console.WriteLine("Cannot use Admin account to buy tickets");
                    Thread.Sleep(5000);
                    ChooseLoginOption(true);
                }
                else{
                    AccountLogic.Login(loginName, loginPassword);
                }
            }
            else if(option == 2){
                AccountLogic.CreateAccount();
            }
            else{
                Console.WriteLine("This is not an option");
            }
        }
        else{
            Console.WriteLine("Invalid input");
        }
    }
}