using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;

namespace ProjectTest.PlayScheduleTests;

[TestClass]
public class PlayScheduleTest
{
    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void ValidTimeTest(){
        string invalidTime1 = "50:00";
        string invalidTime2 = "not a time";
        string validTime = "12:00";

        Assert.IsFalse(PlayLogic.ValidTime(invalidTime1));
        Assert.IsFalse(PlayLogic.ValidTime(invalidTime2));
        Assert.IsTrue(PlayLogic.ValidTime(validTime));
    }

    [TestMethod]
    public void ValidDateTest(){
        string invalidDate1 = DateTime.Now.ToString(@"dd\/MM\/yyyy");
        string invalidDate2 = "not a date";
        string validDate = DateTime.Now.AddDays(10).ToString(@"dd\/MM\/yyyy");

        Assert.IsFalse(PlayLogic.ValidDate(invalidDate1));
        Assert.IsFalse(PlayLogic.ValidDate(invalidDate2));
        Assert.IsTrue(PlayLogic.ValidDate(validDate));
    }


    [TestMethod]
    public void CorrectPlayDetailsTest(){
        string validTime = "12:00";
        string invalidTime = "50:00";

        string validDate = DateTime.Now.AddDays(10).ToString(@"dd\/MM\/yyyy");
        string invalidDate = "01/01/2020";

        string validPerformance = "ID0";
        string invalidPerformance = "ID5";

        Assert.IsFalse(PlayLogic.AddPlay("ID0", validTime, invalidDate, "ID0", validPerformance));
        Assert.IsFalse(PlayLogic.AddPlay("ID0", invalidTime, validDate, "ID0", validPerformance));
        Assert.IsFalse(PlayLogic.AddPlay("ID0", validTime, validDate, "ID0", invalidPerformance));
        Assert.IsTrue(PlayLogic.AddPlay("ID0", validTime, validDate, "ID0", validPerformance));
    }

    /* Because of the new menu, these unit tests aren't relevant anymore,
     * however they are still in the test-sheet of sprint 2, so I don't know
     * if we need to remove this or not */

    // [TestMethod]
    // public void GetDatesTest(){
    //     string Location1 = "ID0";
    //     string Location2 = "ID1";
    //     List<Play> Viewings = new(){
    //         new Play(Location1, "00:00", "01/01/2024", "ID0", "ID0"),
    //         new Play(Location1, "00:00", "02/01/2024", "ID1", "ID0"),
    //         new Play(Location2, "00:00", "01/02/2024", "ID2", "ID1"),
    //         new Play(Location2, "00:00", "02/02/2024", "ID3", "ID2"),
    //     };

    //     string? datesString;
    //     Dictionary<int, string>? datesDict;

    //     // Empty
    //     (datesString, datesDict) = PlayLogic.GetDates("Non-existing Location", new List<Play>());
    //     Assert.AreEqual(datesString, null);
    //     Assert.AreEqual(datesDict, null);
        
    //     // No options
    //     (datesString, datesDict) = PlayLogic.GetDates("Non-existing Location", Viewings);
    //     Assert.AreEqual(datesDict?.Count(), 0);

    //     // Location1
    //     (datesString, datesDict) = PlayLogic.GetDates(Location1, Viewings);
    //     Assert.IsTrue(datesString?.Contains("01/01/2024"));
    //     Assert.IsTrue(datesString?.Contains("02/01/2024"));
    //     Assert.IsFalse(datesString?.Contains("01/02/2024"));

    //     // Location2
    //     (datesString, datesDict) = PlayLogic.GetDates(Location2, Viewings);
    //     Assert.IsTrue(datesString?.Contains("01/02/2024"));
    //     Assert.IsTrue(datesString?.Contains("02/02/2024"));
    //     Assert.IsFalse(datesString?.Contains("01/01/2024"));
    // }
    
    // [TestMethod]
    // public void GetTimesTest(){
    //     string Location1 = "ID0";
    //     string Location2 = "ID1";
    //     List<Play> Viewings = new(){
    //         new Play(Location1, "19:21:13", "01/01/2024", "ID0", "ID0"),
    //         new Play(Location1, "20:30:00", "01/01/2024", "ID1", "ID0"),
    //         new Play(Location2, "17:40:00", "01/01/2024", "ID2", "ID1"),
    //         new Play(Location2, "09:15:00", "01/01/2024", "ID3", "ID2")
    //     };

    //     string? TimeString;
    //     Dictionary<int, string>? TimeDict;
        
    //     // Empty
    //     (TimeString, TimeDict) = PlayLogic.GetTimes("Non-existing Location", "01/01/2024", new List<Play>());
    //     Assert.AreEqual(TimeString, null);
    //     Assert.AreEqual(TimeDict, null);
        
    //     // No options
    //     (TimeString, TimeDict) = PlayLogic.GetTimes("Non-existing Location", "01/01/2024", Viewings);
    //     Assert.AreEqual(TimeDict?.Count, 0);

    //     // Location1
    //     (TimeString, TimeDict) = PlayLogic.GetTimes(Location1, "01/01/2024", Viewings);
    //     Assert.IsTrue(TimeString?.Contains("19:21:13"));
    //     Assert.IsTrue(TimeString?.Contains("20:30:00"));
    //     Assert.IsFalse(TimeString?.Contains("09:15:00"));

    //     // Location2
    //     (TimeString, TimeDict) = PlayLogic.GetTimes(Location2, "01/01/2024", Viewings);
    //     Assert.IsTrue(TimeString?.Contains("17:40:00"));
    //     Assert.IsTrue(TimeString?.Contains("09:15:00"));
    //     Assert.IsFalse(TimeString?.Contains("19:21:13"));
    // }

    [TestMethod]
    public void PlayGetsAddedToArchiveTest(){
        string validDate = DateTime.Now.AddDays(10).ToString(@"dd\/MM\/yyyy");
        PlayLogic.AddPlay("ID0", "12:00", validDate, "ID0", "ID0");

        Play archivedPlay = App.ArchivedPlays["ID0"][0];
        Assert.IsTrue(archivedPlay.Location == "ID0" && archivedPlay.StartTime == "12:00" && 
                      archivedPlay.Date == validDate && archivedPlay.Hall == "ID0");
    }

    [TestMethod]
    public void RemoveOutdatedPlaysTest(){
        string tenDays = DateTime.Now.AddDays(10).ToString(@"dd\/MM\/yyyy");
        string today = DateTime.Now.ToString(@"dd\/MM\/yyyy");
        string invalidTime = DateTime.Now.AddMinutes(30).ToString("HH:mm:ss");

        Play ValidPlay = new("ID0", "12:00", tenDays, "ID0", "ID0");
        Play outdatedPlay1 = new("ID0", "12:00", "01/01/2020", "ID0", "ID0");
        Play outdatedPlay2 = new("ID0", invalidTime, today, "ID0", "ID0");
        App.Plays["ID0"].Add(ValidPlay);
        App.Plays["ID0"].Add(outdatedPlay1);
        App.Plays["ID0"].Add(outdatedPlay2);

        PlayLogic.RemoveOutdatedPlays();

        Assert.IsTrue(App.Plays["ID0"].Contains(ValidPlay));
        Assert.IsFalse(App.Plays["ID0"].Contains(outdatedPlay1));
        Assert.IsFalse(App.Plays["ID0"].Contains(outdatedPlay2));
    }

    [TestMethod]
    public void IsHallAvailableTest_HallIsFree(){
        // With no plays added, assumes that json file is empty List with no plays.
        bool IsAvailable1 = PlayLogic.IsHallAvailable("ID0", DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "19:21", "ID0");
        bool IsAvailable2 = PlayLogic.IsHallAvailable("ID1", DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "17:50", "ID2");

        Assert.IsTrue(IsAvailable1);
        Assert.IsTrue(IsAvailable2);
    }

    [TestMethod]
    public void IsHallAvailableTest_HallIsBooked(){
        string Location1 = "ID0";
        string Location2 = "ID1";
        List<Play> Viewings = new(){
            new Play(Location1, "19:21", "01/01/2025", "ID0", "ID0"),
            new Play(Location1, "20:30", "01/01/2025", "ID1", "ID0"),
            new Play(Location2, "17:50", "01/01/2025", "ID2", "ID1"),
            new Play(Location2, "09:15", "01/01/2025", "ID3", "ID2"),
            new Play(Location2, "09:15", "01/01/2025", "null", "ID2")
        };
        App.Plays.Add($"{App.Plays.Count}", Viewings);

        bool IsNotAvailable1 = PlayLogic.IsHallAvailable(Location1, DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "19:21", "ID0");
        bool IsNotAvailable2 = PlayLogic.IsHallAvailable(Location1, DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "20:30", "ID1");
        bool IsNotAvailable3 = PlayLogic.IsHallAvailable(Location2, DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "09:15", "ID3");
        bool IsNotAvailable4 = PlayLogic.IsHallAvailable(Location2, DateTime.Parse("01/01/2025", new CultureInfo("nl-NL")), "09:15", "null");

        Assert.IsFalse(IsNotAvailable1);
        Assert.IsFalse(IsNotAvailable2);
        Assert.IsFalse(IsNotAvailable3);
        Assert.IsTrue(IsNotAvailable4);
    }

    [TestMethod]
    public void TestEndTimeIsSet(){
        string Location1 = "ID0";
        string Location2 = "ID1";
        List<Play> Viewings = new(){
            new Play(Location1, "19:21", "01/01/2025", "ID0", "ID0"),
            new Play(Location1, "20:30", "01/01/2025", "ID1", "ID0"),
            new Play(Location2, "17:50", "01/01/2025", "ID2", "ID1"),
            new Play(Location2, "09:15", "01/01/2025", "ID3", "ID2"),
            new Play(Location2, "09:15", "01/01/2025", "null", "ID2")
        };
        App.Plays.Add($"{App.Plays.Count}", Viewings);

        Assert.AreEqual(120, App.Performances["ID0"].RuntimeInMin);
        Assert.AreEqual(120, App.Performances["ID1"].RuntimeInMin);
    }
    [TestMethod]
    public void TestFilterFullPlays()
    {
        string nextMonth = DateTime.Now.AddMonths(1).ToString(@"dd\/MM\/yyyy");
        HashSet<int> seats = new();
        for (int i = 1; i <= 20; i++){
            seats.Add(i);
        }
        Play Play1 = new Play("ID0", "18:00", nextMonth, "ID5", "ID0");

        Console.WriteLine(App.Plays["ID0"].Count);
        App.Plays["ID0"].Add(Play1);
        List<Play> FilterCheck1 = PlayLogic.FilterFullPlays(App.Plays["ID0"]);
        Assert.IsTrue(FilterCheck1.Contains(Play1));

        App.Plays["ID0"][0].Seats = seats;
        List<Play> FilterCheck2 = PlayLogic.FilterFullPlays(App.Plays["ID0"]);
        Assert.IsFalse(FilterCheck2.Contains(Play1), $"{String.Join('|', Play1.Seats)} {Play1.BookedSeats}");
    }
}
