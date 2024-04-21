namespace ProjectTest.CatalogueFilterTest;

[TestClass]
public class CatalogueFilterTest
{
    [TestMethod]
    public void CatalogueGetsFilteredTest()
    {   
        int index = 0;
        List<string> GenreIDList = new List<string>{"ID1"};
        List<string> PerformanceIDList = new List<string> {"ID13", "ID5"};
        var result = PerformanceLogic.FilteredPerformanceOptions(GenreIDList);

        foreach (var item in result)
        {
            Assert.AreEqual(item.Item1, PerformanceIDList[index]);
            index++;
        }
    }
    
    [TestMethod]
    public void EmptyGenreIDList()
    {
        int index = 0;
        List<string> GenreIDList = new List<string>{};
        List<string> PerformanceIDList = new List<string> {"ID11", "ID2", "ID1", "ID4", "ID12", "ID13", "ID8", "ID3", "ID0", "ID7", "ID9", "ID6", "ID5", "ID10"};
        var result = PerformanceLogic.FilteredPerformanceOptions(GenreIDList);

        foreach (var item in result)
        {
            Assert.AreEqual(item.Item1, PerformanceIDList[index]);
            index++;
        }
    }

    [TestMethod]
    public void CatalogueGetsFilteredTestWithMultipleGenreID()
    {
        int index = 0;
        List<string> GenreIDList = new List<string>{"ID1", "ID2"};
        List<string> PerformanceIDList = new List<string> {"ID1", "ID13", "ID8", "ID9", "ID5"};
        var result = PerformanceLogic.FilteredPerformanceOptions(GenreIDList);

        foreach (var item in result)
        {
            Assert.AreEqual(item.Item1, PerformanceIDList[index]);
            index++;
        }
    }
}
