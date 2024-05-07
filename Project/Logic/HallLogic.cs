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
        DataAccess.UpdateItem<Hall>();
        return true;
    }

    public static string AssignId(){
        return $"ID{App.Halls.Count}";
    }

    public static bool ChangeName(string id, string name){
        if (!App.Halls.ContainsKey(id)) return false;
        if (name == "") return false;
        foreach (var hall in App.Halls){
            if (hall.Key == id) continue;
            if (hall.Value.Name.ToLower() == name.ToLower()) return false;
        }
        App.Halls[id].Name = name;
        DataAccess.UpdateItem<Hall>();
        return true;
    }

    public static bool ChangeSeats(string id, int seats){
        if (!App.Halls.ContainsKey(id)) return false;
        if (seats <= 0) return false;
        App.Halls[id].Seats = seats;
        DataAccess.UpdateItem<Hall>();
        return true;
    }
}
