namespace ProjectTest.GenreTest;

[TestClass]
public class GenreTest
{
    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void AddGenreTest()
    {
        Assert.IsFalse(App.genreLogic.AddGenre("Genre 1", 0));
        Assert.IsTrue(App.genreLogic.AddGenre("Genre 5", 0));
    }

    [TestMethod]
    public void NewGenreTest()
    {
        int count = App.Genres.Count;
        App.genreLogic.AddGenre("Genre 5", 0);
        
        Assert.AreEqual(1, App.Genres.Count - count);
        Assert.AreEqual("Genre 5", App.Genres["ID4"].Name);
    }
}
