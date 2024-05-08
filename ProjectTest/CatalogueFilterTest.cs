namespace ProjectTest.CatalogueFilterTest;

[TestClass]
public class CatalogueFilterTest
{
    [TestMethod]
    public void CatalogueGetsFilteredTest()
    {   
        List<string> GenreIDList = new List<string>{"ID0"};
        List<string> PerformanceIDList = new List<string> {"ID0", "ID2"};
        var result = App.performanceLogic.FilteredPerformanceOptions(GenreIDList);

        List<string> resultIds = new();
        result.ForEach(performance => resultIds.Add(performance.Item1));
        resultIds.ForEach(x => Console.WriteLine(x));
        CollectionAssert.AreEqual(resultIds, PerformanceIDList);
    }
    
    [TestMethod]
    public void EmptyGenreIDList()
    {
        List<string> GenreIDList = new List<string>{};
        List<string> PerformanceIDList = new List<string> {"ID0", "ID1", "ID2"};
        var result = App.performanceLogic.FilteredPerformanceOptions(GenreIDList);

        List<string> resultIds = new();
        result.ForEach(performance => resultIds.Add(performance.Item1));
        CollectionAssert.AreEqual(resultIds, PerformanceIDList);
    }

    [TestMethod]
    public void CatalogueGetsFilteredTestWithMultipleGenreID()
    {
        List<string> GenreIDList = new List<string>{"ID0", "ID1"};
        List<string> PerformanceIDList = new List<string> {"ID0", "ID1", "ID2"};
        var result = App.performanceLogic.FilteredPerformanceOptions(GenreIDList);

        List<string> resultIds = new();
        result.ForEach(performance => resultIds.Add(performance.Item1));
        CollectionAssert.AreEqual(resultIds, PerformanceIDList);
    }
}
