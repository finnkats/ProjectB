public abstract class LogicBase<T> where T : IEditable{
    // A reference to App.Objects for T
    public Dictionary<string, T> Dict;
    public LogicBase(){
        // fields are all Fields of App
        var fields = typeof(App).GetFields();

        Dictionary<string, T>? objDict = null;
        // Get the App.Objects dictionary, for the object of T
        foreach (var field in fields){
            var fieldValue = field.GetValue(null);
            if (fieldValue?.GetType() == typeof(Dictionary<string, T>)){
                objDict = (Dictionary<string, T>)fieldValue;
                break;
            }
        }
        if (objDict is null) throw new Exception($"App.{typeof(T).Name}s does not exist");
        Dict = objDict;
    }

    // Checks if name exists, assigns id and updates database
    // Call this in objects own "AddObject" after validating other properties of object
    public bool AddObject(T obj){
        if (obj is null) return false;
        if (!ValidName(obj.Name)) return false;
        Dict.Add(GetID(), obj);
        DataAccess.UpdateItem<T>();
        return true;
    }

    public string GetID(){
        return $"ID{Dict.Count}";
    }

    public bool ValidName(string inputName, string? id = null){
        if (inputName == "") return false;

        // If ID is given, you don't have to loop through the dictionary
        if (id != null) {
            return !(Dict[id].Name.ToLower() == inputName.ToLower());
        }

        foreach (var dictObj in Dict.Values){
            if (dictObj.Name.ToLower() == inputName.ToLower()){
                //Console.WriteLine($"{typeof(T).Name.ToLower()} with name {inputName} already exists\n");
                return false;
            }
        }
        return true;
    }

    public bool ChangeName(string id, string name){
        if (!Dict.ContainsKey(id)) return false;
        if (!ValidName(name, id)) return false;
        string oldName = Dict[id].Name;
        Dict[id].Name = name;
        DataAccess.UpdateItem<T>();
        if (typeof(T) == typeof(Genre)){
            GenreLogic.logger.LogAction("Genre name changed", new { GenreId = id, OldName = oldName, NewName = name });
        }
        return true;
    }
    // if object is given, it finds the id of it
    public bool ChangeName(T obj, string name) => ChangeName(Dict.First(keyval => keyval.Value.Name == obj.Name).Key, name);
}
