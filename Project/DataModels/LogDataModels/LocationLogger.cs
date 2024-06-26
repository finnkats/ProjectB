public class LocationLogger : Logger
{
    public override string FilePath {get; set;} = "DataSources/LogFiles/Locations.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Location info, Halls";
    public LocationLogger(){ }

    public override void LogAction(string action, object? locationInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {locationInfo}";
        WriteToCsv(logEntry);
    }
}
