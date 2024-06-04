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
        Assert.IsTrue(App.AdminFeatures.PreviousMenu == App.FrontPage, "5");
    }
}
