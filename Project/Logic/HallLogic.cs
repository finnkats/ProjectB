public class HallLogic : LogicBase<Hall> {
    // Function gets called to add A hall
    // It checks if the name isn't empty or seats is <= 0 or a hall with the same name already exists
    // Returns if it successfully added the hall

    // A hall can be made without instantly linking it to a location, the location will be the string "null"
    // in that case
    public bool AddHall(string Name, int Seats, string locationId = "null"){
        if (Seats <= 0) return false;
        return base.AddObject(new Hall(Name, Seats, locationId));
    }

    public bool ChangeSeats(string id, int seats){
        if (!App.Halls.ContainsKey(id)) return false;
        if (seats <= 0) return false;
        App.Halls[id].Seats = seats;
        HallDataAccess.UpdateHalls();
        return true;
    }
}
