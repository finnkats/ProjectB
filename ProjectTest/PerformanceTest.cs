using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectTest.PerformanceTest;

[TestClass]
public class PerformanceTest{

    [TestInitialize]
    public void EmptyPerformances(){
        File.WriteAllText(@"DataSources/performances.json", "{}");
        App.Performances.Clear();
        File.WriteAllText(@"DataSources/Plays.json", "{}");
        App.Plays.Clear();
    }

    [TestMethod]
    public void AddNewPerformanceTest(){
        string name = "Test Performance 1";
        List<string> genres = new(){"Genre 1", "Genre 2"};
        bool active = false;

        PerformanceLogic.AddPerformance(name, genres, active);

        Performance? Performance = null;
        foreach (var performance in App.Performances.Values){
            if (performance.Name == "Test Performance 1"){
                Performance = performance;
                break;
            }
        }

        Assert.IsNotNull(Performance);
        Assert.AreEqual("Test Performance 1", Performance.Name);
        Assert.AreEqual(genres[0], Performance.Genres[0]);
        Assert.AreEqual(active, Performance.Active);
    }

    [TestMethod]
    public void DuplicateTest(){
        int Amount = App.Performances.Count();
        PerformanceLogic.AddPerformance("Test Performance 2", new List<string>(){"Genre 1", "Genre 2"}, false);
        PerformanceLogic.AddPerformance("Test Performance 2", new List<string>(){"Genre 1", "Genre 2"}, false);

        Assert.AreEqual(Amount + 1, App.Performances.Count());

    }

    [TestMethod]
    public void AssignIdTest(){
        int baseAmount = App.Performances.Count();
        PerformanceLogic.AddPerformance("Test Performance 3", new List<string>(){"Genre 1", "Genre 2"}, false);
        
        Assert.AreEqual(1, App.Performances.Count() - baseAmount);
        Assert.IsTrue(App.Performances.ContainsKey($"ID{baseAmount}"));
        Assert.AreEqual("Test Performance 3", App.Performances[$"ID{baseAmount}"].Name);
    }
}
