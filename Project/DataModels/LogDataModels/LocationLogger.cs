public class LocationLogger : Logger
{
    protected override string FilePath {get; set;} = "DataSources/LogFiles/Halls.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Location info";
    public LocationLogger(){ }

    public override void LogAction(string action, object locationInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {locationInfo}";
        WriteToCsv(logEntry);
    }
}
