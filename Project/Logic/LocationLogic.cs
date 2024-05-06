public class LocationLogic : LogicBase<Location>{
    // Same story as the other Logic files
    // App.Halls is a list of Hall id's linked to this location (can be empty)
    public bool AddLocation(string Name, List<string> Halls){
        if (Name == "") return false;
        foreach (var location in App.Locations){
            if (location.Value.Name.ToLower() == Name.ToLower()) return false;
        }
        
        string locationId = GetID();
        Halls.ForEach(hall => App.Halls[hall].LocationId = locationId);
        Location newLocation = new(Name, Halls);
        App.Locations.Add(locationId, newLocation);
        LocationDataAccess.UpdateLocations();
        return true;
    }

    // Similar to other logic
    public bool ChangeHalls(string id, List<string>? halls = null){
        if (halls == null) halls = App.hallPresentation.GetUnlinkedHalls(id);
        if (!App.Locations.ContainsKey(id)) return false;
        App.Locations[id].Halls = halls;
        return true;
    }
}
