using Newtonsoft.Json;

public class Ticket{
    public string MovieName {get; set;}
    public string Date {get; set;}
    public string Time {get; set;}
    private static int UniqueBookID = 1;

    public Ticket(string movieName, string date, string time){
        this.MovieName = movieName;
        this.Date = date;
        this.Time = time;
        UpdateJsonFile();
    }

    protected void UpdateJsonFile(){
        try{
            StreamReader reader = new("../DataSources/tickets.json");
            string ticketsJson = reader.ReadToEnd();
        }
        catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }

    private static void NewBookID() => UniqueBookID++;
}