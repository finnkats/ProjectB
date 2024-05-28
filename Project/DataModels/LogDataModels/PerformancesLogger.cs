public class PerformanceLogger : Logger
{
    protected override string FilePath { get; set; } = "DataSources/Logfiles/Performances.csv";
    protected override string Headers { get; set; } = "Time, User, Action, Peformance Info";

    public PerformanceLogger() { }

    public override void LogAction(string action, object? PeformanceInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {PeformanceInfo}";
        WriteToCsv(logEntry);
    }

}
