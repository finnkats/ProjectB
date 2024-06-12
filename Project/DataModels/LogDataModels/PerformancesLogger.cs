public class PerformanceLogger : Logger
{
    public override string FilePath { get; set; } = "DataSources/Logfiles/Performances.csv";
    protected override string Headers { get; set; } = "Time, User, Action, Performance Info";

    public PerformanceLogger() { }

    public override void LogAction(string action, object? PerformanceInfo)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {PerformanceInfo}";
        WriteToCsv(logEntry);
    }

}
