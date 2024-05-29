class TicketLogger : Logger
{
    protected override string FilePath { get; set; } = "DataSources/LogFiles/Tickets.csv"; 
    protected override string Headers { get; set; } = "Time, User, Action, Ticket";

    public TicketLogger() { }

    public override void LogAction(string action, object? Ticket)
    {
        string logEntry = $"{DateTime.Now}, {App.LoggedInUsername}, {action}, {Ticket}";
        WriteToCsv(logEntry);
    }
}