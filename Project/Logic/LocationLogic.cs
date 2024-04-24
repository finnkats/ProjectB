public static class LocationLogic {
    // Same story as the other Logic files
    // App.Halls is a list of Hall id's linked to this location (can be empty)
    public static bool AddLocation(string Name, List<string> Halls){
        if (Name == "") return false;
        foreach (var location in App.Locations){
            if (location.Value.Name.ToLower() == Name.ToLower()) return false;
        }
        
        string locationId = AssignId();
        Halls.ForEach(hall => App.Halls[hall].LocationId = locationId);
        Location newLocation = new(Name, Halls);
        App.Locations.Add(locationId, newLocation);
        LocationDataAccess.UpdateLocations();
        return true;
    }

    public static string AssignId(){
        return $"ID{App.Locations.Count}";
    }

    // Similar to other logic
    public static bool ChangeName(string id, string name){
        if (!App.Locations.ContainsKey(id)) return false;
        if (name == "") return false;
        foreach (var location in App.Locations){
            if (location.Key == id) continue;
            if (location.Value.Name.ToLower() == name.ToLower()) return false;
        }
        App.Locations[id].Name = name;
        LocationDataAccess.UpdateLocations();
        return true;
    }

    // Similar to other logic
    public static bool ChangeHalls(string id, List<string>? halls = null){
        if (halls == null) halls = HallPresentation.GetUnlinkedHalls(id);
        if (!App.Locations.ContainsKey(id)) return false;
        App.Locations[id].Halls = halls;
        return true;
    }
}
