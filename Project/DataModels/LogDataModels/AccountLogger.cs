public class AccountLogger : Logger
{
    public override string FilePath {get; set;} = "DataSources/LogFiles/Accounts.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Account info";
    public AccountLogger(){ }

    public override void LogAction(string action, object? userInfo = null)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}";
        if (userInfo != null)
        {
            logEntry += $", {userInfo}";
        }
        WriteToCsv(logEntry);
    }
}