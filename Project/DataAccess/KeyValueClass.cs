public class KeyValueClass{
    public int ID {get; set;}
    public Ticket Ticket {get; set;}
    public KeyValueClass(int id, Ticket ticket){
        this.ID = id;
        this.Ticket = ticket;
    }
}