public class Ticket{
    public string MovieName {get; set;}
    public string Date {get; set;}
    public string Time {get; set;}

    public Ticket(string movieName, string date, string time){
        this.MovieName = movieName;
        this.Date = date;
        this.Time = time;
        UpdateJsonFile();
    }

    protected void UpdateJsonFile(){
        StreamReader reader = new("../DataSources/tickets.json");
    }
}