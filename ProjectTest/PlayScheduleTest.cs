using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectTest.PlayScheduleTests;

[TestClass]
public class MovieScheduleTest
{
    [TestMethod]
    public void GetDatesTest(){
        string Location1 = "Kruispunt";
        string Location2 = "Zuidplein";
        List<MovieViewing> Viewings = new(){
            new MovieViewing(Location1, "00:00", "01/01/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location1, "00:00", "02/01/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location2, "00:00", "01/02/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location2, "00:00", "02/02/2024", "Hall 1", "Play 1"),
        };

        string? datesString;
        Dictionary<int, string>? datesDict;

        // Empty
        (datesString, datesDict) = PlaySchedule.GetDates("Non-existing Location", new List<MovieViewing>());
        Assert.AreEqual(datesString, null);
        Assert.AreEqual(datesDict, null);
        
        // No options
        (datesString, datesDict) = PlaySchedule.GetDates("Non-existing Location", Viewings);
        Assert.AreEqual(datesDict?.Count(), 0);

        // Location1
        (datesString, datesDict) = PlaySchedule.GetDates(Location1, Viewings);
        Assert.IsTrue(datesString?.Contains("01/01/2024"));
        Assert.IsTrue(datesString?.Contains("02/01/2024"));
        Assert.IsFalse(datesString?.Contains("01/02/2024"));

        // Location2
        (datesString, datesDict) = PlaySchedule.GetDates(Location2, Viewings);
        Assert.IsTrue(datesString?.Contains("01/02/2024"));
        Assert.IsTrue(datesString?.Contains("02/02/2024"));
        Assert.IsFalse(datesString?.Contains("01/01/2024"));
    }
    
    [TestMethod]
    public void GetTimesTest(){
        string Location1 = "Kruispunt";
        string Location2 = "Zuidplein";
        List<MovieViewing> Viewings = new(){
            new MovieViewing(Location1, "19:21:13", "01/01/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location1, "20:30:00", "01/01/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location2, "17:40:00", "01/01/2024", "Hall 1", "Play 1"),
            new MovieViewing(Location2, "09:15:00", "01/01/2024", "Hall 1", "Play 1"),
        };

        string? TimeString;
        Dictionary<int, string>? TimeDict;
        
        // Empty
        (TimeString, TimeDict) = PlaySchedule.GetTimes("Non-existing Location", "01/01/2024", new List<MovieViewing>());
        Assert.AreEqual(TimeString, null);
        Assert.AreEqual(TimeDict, null);
        
        // No options
        (TimeString, TimeDict) = PlaySchedule.GetTimes("Non-existing Location", "01/01/2024", Viewings);
        Assert.AreEqual(TimeDict?.Count, 0);

        // Location1
        (TimeString, TimeDict) = PlaySchedule.GetTimes(Location1, "01/01/2024", Viewings);
        Assert.IsTrue(TimeString?.Contains("19:21:13"));
        Assert.IsTrue(TimeString?.Contains("20:30:00"));
        Assert.IsFalse(TimeString?.Contains("09:15:00"));

        // Location2
        (TimeString, TimeDict) = PlaySchedule.GetTimes(Location2, "01/01/2024", Viewings);
        Assert.IsTrue(TimeString?.Contains("17:40:00"));
        Assert.IsTrue(TimeString?.Contains("09:15:00"));
        Assert.IsFalse(TimeString?.Contains("19:21:13"));
    }
}
