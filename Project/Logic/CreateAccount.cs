using Logic;

public static class CreateAccount{
    public static void Create(){
        (string name, string password) = AccountPresentation.GetLoginDetails();
        if (!AccountPresentation.DoubleCheckPassword(password) || name == "null"){
            return;
        }

        Dictionary<string, AccountDataModel> Accounts = AccountDataAccess.LoadAll();
        if (Accounts.ContainsKey(name) || name == "Unknown"){
            AccountPresentation.PrintMessage("Account with that name already exists");
            return;
        }
        Accounts.Add(name, new AccountDataModel(name, password, false));
        AccountDataAccess.WriteAll(Accounts);
        AccountPresentation.PrintMessage("\nAccount has been created.");
        Thread.Sleep(1000);
        AccountLogic.Login(null, name, password);
    }
}
