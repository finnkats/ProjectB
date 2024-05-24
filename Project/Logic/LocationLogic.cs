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
        logger.LogAction("Location added", Name);
        return true;
    }

    public bool ChangeHalls(string id, List<string>? halls = null){
        if (halls == null) halls = App.hallPresentation.GetItemList(id);
        if (!App.Locations.ContainsKey(id)) return false;
        string locationName = App.Locations[id].Name;
        logger.LogAction("Location halls changed", $"LocationId: {id}, LocationName: {locationName}, Halls: {string.Join(", ", halls)}");

        App.Locations[id].Halls = halls;
        DataAccess.UpdateItem<Location>();
        return true;
    }
}
