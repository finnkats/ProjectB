public static class LocationLogic {
    public static bool AddLocation(string Name, List<string> Halls){
        if (Name == "") return false;
        foreach (var Location in App.Locations){
            if (Location.Value.Name.ToLower() == Name.ToLower()) return false;
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

    public static bool ChangeName(string id, string name){
        if (!App.Locations.ContainsKey(id)) return false;
        App.Locations[id].Name = name;
        return true;
    }

    public static bool ChangeHalls(string id, List<string>? halls = null){
        if (halls == null) halls = HallPresentation.GetHalls(id);
        if (!App.Locations.ContainsKey(id)) return false;
        App.Locations[id].Halls = halls;
        return true;
    }
}
