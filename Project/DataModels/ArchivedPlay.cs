// So the generic DataAccess works with archived plays too

using System.Text.Json.Serialization;

public class ArchivedPlay : Play {
    public ArchivedPlay(string location, string startTime, string date, string hall, string performanceId) 
    : base(location, startTime, date, hall, performanceId) {}
}
