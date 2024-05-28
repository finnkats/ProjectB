using System.Runtime.CompilerServices;

namespace ProjectTest.LocationTest;

[TestClass]
public class LocationTest{

    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void AddHall(){
        // TO DO Make this unit test better, because of new layout system
        Assert.IsFalse(App.hallLogic.AddHall("", new Layout(new int[5][], 20), "ID0"));
        //Assert.IsFalse(App.hallLogic.AddHall("New Name", new Layout(new int[5][], 20), "ID0"));
        Assert.IsFalse(App.hallLogic.AddHall("NAME3", new Layout(new int[5][], 20), "ID0"));
        
        int before = App.Halls.Count;
        Assert.IsTrue(App.hallLogic.AddHall("NAME20", new Layout(new int[5][], 20), "ID0"));
        Assert.AreEqual(before + 1, App.Halls.Count);
    }

    [TestMethod]
    public void AddLocation(){
        Assert.IsFalse(App.locationLogic.AddLocation("", new List<string>()));
        Assert.IsFalse(App.locationLogic.AddLocation("Location1", new List<string>()));
        
        int before = App.Locations.Count;
        Assert.IsTrue(App.locationLogic.AddLocation("Location3", new List<string>()));
        Assert.AreEqual(before + 1, App.Locations.Count);
    }

    [TestMethod]
    public void ChangeName(){
        Assert.IsFalse(App.hallLogic.ChangeName("ID0", "NAME1"));
        Assert.IsTrue(App.hallLogic.ChangeName("ID0", "NAME20"));
    
        Assert.IsFalse(App.locationLogic.ChangeName("ID0", "Location1"));
        Assert.IsTrue(App.locationLogic.ChangeName("ID0", "Location20"));

    }
}
