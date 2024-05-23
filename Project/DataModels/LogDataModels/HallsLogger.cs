public class HallsLogger : Logger
{
    protected override string FilePath {get; set;} = "DataSources/LogFiles/Halls.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Hall info";
    public HallsLogger(){ }

    public override void LogAction(string action, object? hallInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {hallInfo}";
        WriteToCsv(logEntry);
    }
}
