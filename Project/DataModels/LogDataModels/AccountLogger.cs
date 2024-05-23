public class AccountLogger : Logger
{
    protected override string FilePath {get; set;} = "DataSources/LogFiles/Account.csv";
    protected override string Headers {get; set;} = "Time, User, Action";
    public AccountLogger(){ }

    public override void LogAction(string action, object? obj = null)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}";
        WriteToCsv(logEntry);
    }
}