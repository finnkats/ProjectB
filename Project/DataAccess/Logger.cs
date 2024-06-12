public abstract class Logger
{
    public abstract string FilePath { get; set; }  // Path to the log file
    protected abstract string Headers { get; set; }   // Headers for the CSV file

    // Abstract method to log an action, to be implemented by derived classes
    public abstract void LogAction(string action, object? obj = null);

    // Method to write a log entry to the CSV file
    protected void WriteToCsv(string logEntry){
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