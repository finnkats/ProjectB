using System.Text.Json;

public static class DataAccess {
    public static string FilePrefix = "DataSources/";

    public static Dictionary<string, T> ReadItem<T>() where T : IDataAccessItem{
        string filePath = $"{FilePrefix}{typeof(T).Name.ToLower()}s.json";
        if(!File.Exists(filePath) || string.IsNullOrEmpty(File.ReadAllText(filePath))){
            return new Dictionary<string, T>();
        }
        string json = File.ReadAllText(filePath);
        try{
            return JsonSerializer.Deserialize<Dictionary<string, T>>(json) ?? new Dictionary<string, T>();
        }
        catch (JsonException){
            return new Dictionary<string, T>();
        }
    }

    public static Dictionary<string, List<T>> ReadList<T>() where T : IDataAccessList{
        string filePath = $"{FilePrefix}{typeof(T).Name.ToLower()}s.json";
        if(!File.Exists(filePath) || string.IsNullOrEmpty(File.ReadAllText(filePath))){
            return new Dictionary<string, List<T>>();
        }
        string json = File.ReadAllText(filePath);
        try{
            return JsonSerializer.Deserialize<Dictionary<string, List<T>>>(json) ?? new Dictionary<string, List<T>>();
        }
        catch(JsonException){
            return new Dictionary<string, List<T>>();
        }
    }

    public static void UpdateItem<T>() where T : IDataAccessItem {
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
        // Couldn't find dictionary
        if (objDict is null) return;


        string json = JsonSerializer.Serialize(objDict);
        File.WriteAllText($"{FilePrefix}{typeof(T).Name.ToLower()}s.json", json);
    }

    public static void UpdateList<T>() where T : IDataAccessList {
        // fields are all Fields of App
        var fields = typeof(App).GetFields();

        Dictionary<string, List<T>>? objDict = null;
        // Get the App.Objects dictionary, for the object of T
        foreach (var field in fields){
            var fieldValue = field.GetValue(null);
            if (fieldValue?.GetType() == typeof(Dictionary<string, List<T>>)){
                objDict = (Dictionary<string, List<T>>)fieldValue;
                break;
            }
        }
        // Couldn't find dictionary
        if (objDict is null) return;


        string json = JsonSerializer.Serialize(objDict);
        File.WriteAllText($"{FilePrefix}{typeof(T).Name.ToLower()}s.json", json);
    }
}
