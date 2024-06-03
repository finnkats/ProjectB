public class PlayLogger : Logger
{
    protected override string FilePath { get; set; } = "DataSources/LogFiles/Plays.csv";
    protected override string Headers { get; set; } = "Time, User, Action, Play info";
    public PlayLogger(){ }

    public override void LogAction(string action, object? genreInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {genreInfo}";
        WriteToCsv(logEntry);
    }
}
