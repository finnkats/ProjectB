public abstract class Logger
{
    protected string FilePath { get; set; }  // Path to the log file
    protected string Headers { get; set; }   // Headers for the CSV file

    // Constructor for the abstract class
    protected Logger(string filePath, string headers)
    {
        FilePath = filePath;
        Headers = headers;
    }

    // Abstract method to log an action, to be implemented by derived classes
    public abstract void LogAction(string action, object obj);

    // Method to write a log entry to the CSV file
    protected abstract void WriteToCsv(string logEntry);
}