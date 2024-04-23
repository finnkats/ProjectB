namespace ProjectTest.LocationTest;

[TestClass]
public class LocationTest{
    [ClassInitialize]
    public static void Setup(TestContext testContext){
        if (!App.Locations.ContainsKey("TESTID1")) App.Locations.Add("TESTID1", new Location("Test Location 1", new List<string>(){"TESTHALL1", "TESTHALL2"}));
        if (!App.Locations.ContainsKey("TESTID2")) App.Locations.Add("TESTID2", new Location("Test Location 2", new List<string>()));
        if (!App.Halls.ContainsKey("TESTHALL1")) App.Halls.Add("TESTHALL1", new Hall("Test Hall 1", 500, "TESTID1"));
        if (!App.Halls.ContainsKey("TESTHALL2")) App.Halls.Add("TESTHALL2", new Hall("Test Hall 2", 500, "TESTID1"));
    }

    [TestMethod]
    public void AddHall(){
        Assert.IsFalse(HallLogic.AddHall("", 500, "TESTID1"));
        Assert.IsFalse(HallLogic.AddHall("New Name", -100, "TESTID1"));
        Assert.IsFalse(HallLogic.AddHall("Test Hall 1", 500, "TESTID1"));
        
        int before = App.Halls.Count;
        Assert.IsTrue(HallLogic.AddHall("Test Hall 3", 500, "TESTID1"));
        Assert.AreEqual(before + 1, App.Halls.Count);
    }

    [TestMethod]
    public void AddLocation(){
        Assert.IsFalse(LocationLogic.AddLocation("", new List<string>()));
        Assert.IsFalse(LocationLogic.AddLocation("Test Location 1", new List<string>()));
        
        int before = App.Locations.Count;
        Assert.IsTrue(LocationLogic.AddLocation("Test Location 3", new List<string>()));
        Assert.AreEqual(before + 1, App.Locations.Count);
    }

    [TestMethod]
    public void ChangeName(){
        Assert.IsFalse(HallLogic.ChangeName("TESTHALL1", "Test Hall 2"));
        Assert.IsTrue(HallLogic.ChangeName("TESTHALL1", "Test Hall 1 New"));
    
        Assert.IsFalse(LocationLogic.ChangeName("TESTID1", "Test Location 2"));
        Assert.IsTrue(LocationLogic.ChangeName("TESTID1", "Test Location 1 New"));

    }
}
