// So the generic DataAccess works with archived plays too

using System.Text.Json.Serialization;

public class ArchivedPlay : Play {
    public ArchivedPlay(string location, string time, string date, string hall, string performanceId) 
    : base(location, time, date, hall, performanceId) {}
}
