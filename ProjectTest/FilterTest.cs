namespace ProjectTest.FilterTest;

[TestClass]
public class FilterTest{
    [TestMethod]
    public void AllSameTest(){
        string tomorrow = DateTime.Now.Date.AddDays(1).ToString(@"dd\/MM\/yyyy");
        List<Play> Plays = new(){
            new Play("location", "00:00", tomorrow, "hall", "ID0"),
            new Play("location", "00:00", tomorrow, "hall", "ID0"),
            new Play("location", "00:00", tomorrow, "hall", "ID0")
        };

        var FilteredPlays = PlayLogic.OneMonthFilter(Plays);

        CollectionAssert.AreEqual(Plays, FilteredPlays);
    }

    [TestMethod]
    public void AllDifferentTest(){
        string tooLate = DateTime.Now.Date.AddDays(50).ToString(@"dd\/MM\/yyyy");
        List<Play> Plays = new(){
            new Play("location", "00:00", tooLate, "hall", "ID0"),
            new Play("location", "00:00", tooLate, "hall", "ID0"),
            new Play("location", "00:00", tooLate, "hall", "ID0")
        };

        var FilteredPlays = PlayLogic.OneMonthFilter(Plays);

        Assert.AreEqual(0, FilteredPlays.Count);
    }

    [TestMethod]
    public void VariationTest(){
        string tomorrow = DateTime.Now.Date.AddDays(1).ToString(@"dd\/MM\/yyyy");
        string tooLate = DateTime.Now.Date.AddDays(50).ToString(@"dd\/MM\/yyyy");

        Play correctPlay = new("location", "00:00", tomorrow, "hall", "ID0");

        List<Play> Plays = new(){
            correctPlay,
            correctPlay,
            new Play("location", "00:00", tooLate, "hall", "ID0"),
            correctPlay,
            new Play("location", "00:00", tooLate, "hall", "ID0")
        };

        List<Play> CorrectPlays = new(){
            correctPlay,
            correctPlay,
            correctPlay
        };
        var FilteredPlays = PlayLogic.OneMonthFilter(Plays);

        CollectionAssert.AreEqual(CorrectPlays, FilteredPlays);
    }
}