public class GenreLogger : Logger
{
    //DataSources/LogFiles/Genres.csv
    public override string FilePath {get; set;} = "DataSources/LogFiles/Genres.csv";
    protected override string Headers {get; set;} = "Time, User, Action, Genre info";
    public GenreLogger(){ }

    public override void LogAction(string action, object? genreInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {genreInfo}";
        WriteToCsv(logEntry);
    }
}
