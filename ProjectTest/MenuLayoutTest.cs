namespace ProjectTest.MenuTests.MenuLayout;

[TestClass]
public class MenuLayoutTest{
    [TestInitialize]
    public void Initialize(){
        App.Start();
    }

    [TestMethod]
    public void CheckStructure(){
        Assert.IsTrue(App.FrontPage.PreviousMenu == null, "1");
        Assert.IsTrue(App.HomePage.PreviousMenu == App.FrontPage, "2");
        Assert.IsTrue(App.SignInUp.PreviousMenu == App.FrontPage, "3");
        Assert.IsTrue(App.ExampleMenu1.PreviousMenu == App.FrontPage, "4");
        Assert.IsTrue(App.AdminFeatures.PreviousMenu == App.HomePage, "5");
        Assert.IsTrue(App.ModifyPerformances.PreviousMenu == App.AdminFeatures, "6");
        Assert.IsTrue(App.ModifyCategories.PreviousMenu == App.AdminFeatures, "7");
        Assert.IsTrue(App.ModifyLocations.PreviousMenu == App.AdminFeatures, "8");
        Assert.IsTrue(App.EditLocation.PreviousMenu == App.ModifyLocations, "9");
    }
}
