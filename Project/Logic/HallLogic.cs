public class HallLogic : LogicBase<Hall> {
    // Function gets called to add A hall
    // It checks if the name isn't empty or seats is <= 0 or a hall with the same name already exists
    // Returns if it successfully added the hall
    public static Logger logger = new HallsLogger();
    // A hall can be made without instantly linking it to a location, the location will be the string "null"
    // in that case
    public bool AddHall(string Name, Layout layout, string locationId = "null"){
        string tempId = GetID();
        if (!AddObject(new Hall(Name, layout, locationId))) return false;
        if (locationId != "null"){
            App.Locations[locationId].Halls.Add(tempId);
        }
        DataAccess.UpdateItem<Location>();

        logger.LogAction("Hall added", new { Name = Name, Seats = layout.SeatAmount, LocationId = locationId });
        return true;
    }
}
