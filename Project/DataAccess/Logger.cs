public abstract class Logger
{
    protected string logFilePath {get; set;}
    protected string header {get; set;}

    public Logger(string logFilePath, string header)
    {
        this.logFilePath = logFilePath;
        this.header = header;
        InitializeLogFile();
    }

    private void InitializeLogFile()
    {
        if (!File.Exists(logFilePath))
        {
            using (var writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine(header);
            }
        }
    }

    public abstract void Log(object logEntry);
}