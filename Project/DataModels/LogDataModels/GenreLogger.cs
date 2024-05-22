public class GenreLogger : Logger
{
    public GenreLogger(string filePath, string headers) : base(filePath, headers) { }

    public override void LogAction(string action, object obj)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {obj}";
        WriteToCsv(logEntry);
    }

    protected override void WriteToCsv(string logEntry)
    {
        try{
            // Check if the file exists
            bool fileExists = File.Exists(FilePath);

            // If the file doesn't exist or is empty, add headers
            if (!fileExists || new FileInfo(FilePath).Length == 0)
            {
                File.WriteAllText(FilePath, Headers + Environment.NewLine);
            }

            // Append the log entry to the file
            File.AppendAllText(FilePath, logEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the log file: {ex.Message}");
        }
    }
}