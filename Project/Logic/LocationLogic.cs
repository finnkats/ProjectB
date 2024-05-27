public class LocationLogic : LogicBase<Location>{
    // App.Halls is a list of Hall id's linked to this location (can be empty)
    public static Logger logger = new LocationLogger();
    public bool AddLocation(string Name, List<string> Halls){
        string tempId = GetID();
        if (!AddObject(new Location(Name, Halls))) return false;
        foreach (string hall in Halls){
            App.Halls[hall].LocationId = tempId;
        }
        DataAccess.UpdateItem<Hall>();

        string hallsString = string.Join(", ", Halls);
        logger.LogAction("Location added", $"{Name}, Halls: {hallsString}");

        return true;
    }
}
