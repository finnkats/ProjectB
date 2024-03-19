namespace ProjectTest.MenuTests.MenuLayout;

[TestClass]
public class MenuLayoutTest{
    [TestMethod]
    public void CheckStructure(){
        Assert.IsTrue(App.FrontPage.PreviousMenu == null, "1");
        Assert.IsTrue(App.HomePage.PreviousMenu == App.FrontPage, "2");
        Assert.IsTrue(App.SignInUp.PreviousMenu == App.FrontPage, "3");
        Assert.IsTrue(App.ExampleMenu1.PreviousMenu == App.FrontPage, "4");
        Assert.IsTrue(App.AdminFeatures.PreviousMenu == App.HomePage, "5");
        Assert.IsTrue(App.ModifyMovies.PreviousMenu == App.AdminFeatures, "6");
        Assert.IsTrue(App.ModifyCategories.PreviousMenu == App.AdminFeatures, "7");
        Assert.IsTrue(App.ModifyLocations.PreviousMenu == App.AdminFeatures, "8");
        Assert.IsTrue(App.CheckStatistics.PreviousMenu == App.AdminFeatures, "9");
        Assert.IsTrue(App.EditLocation.PreviousMenu == App.ModifyLocations, "10");
    }
}
