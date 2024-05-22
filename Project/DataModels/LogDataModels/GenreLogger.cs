public class GenreLogger : Logger
{
    protected override string FilePath {get; set;} = "DataSources/LogFiles/Genres.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Object";
    public GenreLogger(){ }

    public override void LogAction(string action, object obj)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {obj}";
        WriteToCsv(logEntry);
    }
}
