public abstract class Logger {
    protected abstract string _filePath {get; set;} // "DataModels/Logs/idk.csv"
    protected abstract string _headers {get; set;}  // "item1,item2,item3,item4"
    public abstract void LogAction();

}
