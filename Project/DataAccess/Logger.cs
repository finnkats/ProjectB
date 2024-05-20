public abstract class ActionLogger
{
    protected DateTime LogTime { get; }
    protected string User { get; }
    protected string Action { get; }
    protected ActionLogger(DateTime logTime, string user, string action)
    {
        LogTime = logTime;
        User = user;
        Action = action;
    }
    // Abstracte methode voor het vastleggen van specifieke actiegegevens naar een CSV-string
    protected abstract string GetActionDetailsAsCsv();

    // Methode om de volledige CSV-string voor de logboekinvoer te krijgen
    public string GetLogEntryAsCsv()
    {
        return $"{LogTime:HH:mm dd-MM-yyyy},{User},{Action},{GetActionDetailsAsCsv()}";
    }
    
    // Methode om de logboekinvoer naar een CSV-bestand te schrijven
    public void Log(string logFilePath)
    {
        File.AppendAllText(logFilePath, GetLogEntryAsCsv() + "\n");
    }
}