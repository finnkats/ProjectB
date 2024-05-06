public class LogicBase<T> where T : IHasName{
    public virtual bool AddObject(T obj){
        var fields = typeof(App).GetFields();
        Dictionary<string, T> objDict;
        foreach (var field in fields){
            var fieldValue = field.GetValue(null);
            if (fieldValue?.GetType() == typeof(Dictionary<string, T>)) objDict = fieldValue;
        }
        return false;
        //if (ObjDict is null) return false;
        //foreach (var dictObj in ObjDict.Values){
        //    if (obj.Name.ToLower() == dictObj.Name.ToLower()) return false;
        //}
        //return true;
    }
}
