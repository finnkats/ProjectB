public class Ticket{
    public string MovieName {get; set;}
    public string Date {get; set;}
    public string Time {get; set;}
    public string Room {get; set;}

    public Ticket(string movieName, string date, string time, string roomChar){
        this.MovieName = movieName;
        this.Date = date;
        this.Time = time;
        this.Room = roomChar;
        UpdateTicketJson.UpdateJsonFile(this);
    }
}