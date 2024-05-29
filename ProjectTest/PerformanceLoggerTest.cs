using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectTest.PerformanceLoggerTest;

[TestClass]
public class PerformanceLoggerTest{
    private string FilePath = "../../../Sources/LogFiles/Performances.csv";
    private PerformanceLogger? logger;

    [TestInitialize]
    public void Reset(){
        this.logger = new PerformanceLogger();
        this.logger.FilePath = this.FilePath;
        // Empty the file for new test methods
        if (File.Exists(FilePath)){
            File.WriteAllText(FilePath, String.Empty);
        }
    }

    [TestMethod]
    public void TestLogCorrectHeaderFormat(){
        // Expected result that are written inside the csv file.
        string expectedHeaders = "Time, User, Action, Performance Info";
        object performanceInfo = new{PerforamnceID = "TestID", RuntimeInMin = 120, Genres = new List<string>{"TestGenre1", "TestGenre2"}, Active = false};
        string expectedPerformanceInfo = "{ PerforamnceID = TestID, RuntimeInMin = 120, Genres = System.Collections.Generic.List`1[System.String], Active = False }";

        this.logger!.LogAction("TestAction", performanceInfo);

        string[] lines = File.ReadAllLines(FilePath);
        Assert.AreEqual(expectedHeaders, lines[0]);
        Assert.IsTrue(lines[1].Contains("TestAction"));
        Assert.IsTrue(lines[1].Contains(expectedPerformanceInfo));
    }

    [TestMethod]
    public void TestLogWriteToFile(){
        // Test if the logger writes the action correct into the csv file.
        // Expected result that are written inside the csv file.
        object performanceInfo = new{PerforamnceID = "TestID", RuntimeInMin = 120, Genres = new List<string>{"TestGenre1", "TestGenre2"}, Active = false};

        this.logger!.LogAction("TestAction", performanceInfo);

        string[] lines = File.ReadAllLines(FilePath);
        Assert.AreEqual(2, lines.Length); // One header line and one log entry line
    }

    [TestMethod]
    public void TestLogCorrectFormat(){
        // Test if the logger writes the actions contents correctly.
        // Expected result that are written inside the csv file.
        object performanceInfo = new{PerforamnceID = "TestID", RuntimeInMin = 120, Genres = new List<string>{"TestGenre1", "TestGenre2"}, Active = false};
        string expectedPerformanceInfo = "{ PerforamnceID = TestID, RuntimeInMin = 120, Genres = System.Collections.Generic.List`1[System.String], Active = False }";

        this.logger!.LogAction("TestAction", performanceInfo);

        string[] lines = File.ReadAllLines(FilePath);
        Assert.AreEqual(2, lines.Length);

        var logEntry = lines[1];
        var parts = logEntry.Split(", ");
        Assert.AreEqual(7, parts.Length); // 4 from the headers, 3 from the fields of the object (in this case, Performance class)
        Assert.IsTrue(DateTime.TryParse(parts[0], out _));
        Assert.AreEqual("Unknown", parts[1]);
        Assert.AreEqual("TestAction", parts[2]);
        Assert.IsTrue(lines[1].Contains(expectedPerformanceInfo));
    }

    [TestMethod]
    public void TestLogNullPerformanceInfo(){
        // Test logger for when Performance info is null.
        this.logger!.LogAction("TestAction", null);

        string[] lines = File.ReadAllLines(FilePath);
        Console.WriteLine(lines.Count());
        Assert.IsTrue(lines[1].Contains("TestAction"));
        Assert.IsTrue(lines[1].Contains(""));
    }

}