using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectTest.PlayScheduleTests;

[TestClass]
public class PlayScheduleTest
{
    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void GetDatesTest(){
        string Location1 = "ID0";
        string Location2 = "ID1";
        List<Play> Viewings = new(){
            new Play(Location1, "00:00", "01/01/2024", "ID0", "ID0"),
            new Play(Location1, "00:00", "02/01/2024", "ID1", "ID0"),
            new Play(Location2, "00:00", "01/02/2024", "ID2", "ID1"),
            new Play(Location2, "00:00", "02/02/2024", "ID3", "ID2"),
        };

        string? datesString;
        Dictionary<int, string>? datesDict;

        // Empty
        (datesString, datesDict) = PlayLogic.GetDates("Non-existing Location", new List<Play>());
        Assert.AreEqual(datesString, null);
        Assert.AreEqual(datesDict, null);
        
        // No options
        (datesString, datesDict) = PlayLogic.GetDates("Non-existing Location", Viewings);
        Assert.AreEqual(datesDict?.Count(), 0);

        // Location1
        (datesString, datesDict) = PlayLogic.GetDates(Location1, Viewings);
        Assert.IsTrue(datesString?.Contains("01/01/2024"));
        Assert.IsTrue(datesString?.Contains("02/01/2024"));
        Assert.IsFalse(datesString?.Contains("01/02/2024"));

        // Location2
        (datesString, datesDict) = PlayLogic.GetDates(Location2, Viewings);
        Assert.IsTrue(datesString?.Contains("01/02/2024"));
        Assert.IsTrue(datesString?.Contains("02/02/2024"));
        Assert.IsFalse(datesString?.Contains("01/01/2024"));
    }
    
    [TestMethod]
    public void GetTimesTest(){
        string Location1 = "ID0";
        string Location2 = "ID1";
        List<Play> Viewings = new(){
            new Play(Location1, "19:21:13", "01/01/2024", "ID0", "ID0"),
            new Play(Location1, "20:30:00", "01/01/2024", "ID1", "ID0"),
            new Play(Location2, "17:40:00", "01/01/2024", "ID2", "ID1"),
            new Play(Location2, "09:15:00", "01/01/2024", "ID3", "ID2")
        };

        string? TimeString;
        Dictionary<int, string>? TimeDict;
        
        // Empty
        (TimeString, TimeDict) = PlayLogic.GetTimes("Non-existing Location", "01/01/2024", new List<Play>());
        Assert.AreEqual(TimeString, null);
        Assert.AreEqual(TimeDict, null);
        
        // No options
        (TimeString, TimeDict) = PlayLogic.GetTimes("Non-existing Location", "01/01/2024", Viewings);
        Assert.AreEqual(TimeDict?.Count, 0);

        // Location1
        (TimeString, TimeDict) = PlayLogic.GetTimes(Location1, "01/01/2024", Viewings);
        Assert.IsTrue(TimeString?.Contains("19:21:13"));
        Assert.IsTrue(TimeString?.Contains("20:30:00"));
        Assert.IsFalse(TimeString?.Contains("09:15:00"));

        // Location2
        (TimeString, TimeDict) = PlayLogic.GetTimes(Location2, "01/01/2024", Viewings);
        Assert.IsTrue(TimeString?.Contains("17:40:00"));
        Assert.IsTrue(TimeString?.Contains("09:15:00"));
        Assert.IsFalse(TimeString?.Contains("19:21:13"));
    }
}
