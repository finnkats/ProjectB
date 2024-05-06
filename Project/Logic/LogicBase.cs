public class LogicBase<T> where T : IHasName{
    public virtual bool AddObject(T obj){
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
        if (objDict is null) return false;

        // Check if object with the same name already exists
        foreach (var dictObj in objDict.Values){
            if (dictObj.Name.ToLower() == obj.Name.ToLower()){
                return false;
            }
        }

        // Add object to dictionary | This also adds it to App.Objects because its a reference
        objDict.Add(GetID(objDict), obj);
        // DataAccess.UpdateData
        return true;
    }

    private string GetID(Dictionary<string, T> objDict){
        return $"ID{objDict.Count}";
    }
}
