using System;
using Logic;

public static class TicketLoginPresentation{
    public static Menu SignInUp = new("Sign in / up");

    public static void ChooseLoginOption(){
        SignInUp.AddAllOption("Sign in", () => AccountLogic.Login());
        SignInUp.AddAllOption("Sign out", AccountLogic.CreateAccount);
        SignInUp.AddCurrentOption("Sign in");
        SignInUp.AddCurrentOption("Sign out");
        Console.WriteLine(SignInUp.MenuString());
        // SignInUp.AddAllOption("Go back", Func);
    }
}