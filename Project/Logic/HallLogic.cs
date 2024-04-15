public static class HallLogic {
    public static bool AddHall(string Name, int Seats, string locationId = "null"){
        if (Name == "") return false;
        if (Seats <= 0) return false;
        foreach (var hall in App.Halls){
            if (hall.Value.Name.ToLower() == Name.ToLower()) return false;
        }

        Hall newHall = new(Name, Seats, locationId);
        string hallId = AssignId();
        App.Halls.Add(hallId, newHall);
        HallDataAccess.UpdateHalls();
        return true;
    }

    public static string AssignId(){
        return $"ID{App.Halls.Count}";
    }

    public static bool ChangeName(string id, string name){
        if (!App.Halls.ContainsKey(id)) return false;
        App.Halls[id].Name = name;
        return true;
    }

    public static bool ChangeSeats(string id, int seats){
        if (!App.Halls.ContainsKey(id)) return false;
        App.Halls[id].Seats = seats;
        return true;
    }
}
