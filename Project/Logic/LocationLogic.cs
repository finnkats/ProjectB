public class LocationLogic : LogicBase<Location>{
    // App.Halls is a list of Hall id's linked to this location (can be empty)
    public bool AddLocation(string Name, List<string> Halls){
        return base.AddObject(new Location(Name, Halls));
    }

    public bool ChangeHalls(string id, List<string>? halls = null){
        if (halls == null) halls = App.hallPresentation.GetItemList(id);
        if (!App.Locations.ContainsKey(id)) return false;
        App.Locations[id].Halls = halls;
        DataAccess.UpdateItem<Location>();
        return true;
    }
}
