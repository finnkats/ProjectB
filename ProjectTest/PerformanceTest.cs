using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ProjectTest.PerformanceTest;

[TestClass]
public class PerformanceTest{

    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void AddNewPerformanceTest(){
        string name = "Performance 4";
        int runtime = 120;
        List<string> genres = new(){"Genre 1", "Genre 2"};
        bool active = false;

        App.performanceLogic.AddPerformance(name, runtime, genres, active);

        Performance? Performance = null;
        foreach (var performance in App.Performances.Values){
            if (performance.Name == "Performance 4"){
                Performance = performance;
                break;
            }
        }

        Assert.IsNotNull(Performance);
        Assert.AreEqual("Performance 4", Performance.Name);
        Assert.AreEqual(120, Performance.RuntimeInMin);
        Assert.AreEqual(genres[0], Performance.Genres[0]);
        Assert.AreEqual(active, Performance.Active);
    }

    [TestMethod]
    public void DuplicateTest(){
        int Amount = App.Performances.Count();
        App.performanceLogic.AddPerformance("Performance 4", 120, new List<string>(){"Genre 1", "Genre 2"}, false);
        App.performanceLogic.AddPerformance("Performance 4", 120, new List<string>(){"Genre 1", "Genre 2"}, false);

        Assert.AreEqual(Amount + 1, App.Performances.Count());
    }

    [TestMethod]
    public void AssignIdTest(){
        int baseAmount = App.Performances.Count();

        Console.WriteLine(App.performanceLogic.AddPerformance("Performance 4", 120, new List<string>(){"Genre 1", "Genre 2"}, false));
        
        Assert.AreEqual(1, App.Performances.Count() - baseAmount);
        Assert.IsTrue(App.Performances.ContainsKey($"ID{baseAmount}"));
        Assert.AreEqual("Performance 4", App.Performances[$"ID{baseAmount}"].Name);
    }
}
