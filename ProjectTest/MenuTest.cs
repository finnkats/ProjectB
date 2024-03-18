using System.Text.RegularExpressions;

namespace ProjectTest.MenuTests;

[TestClass]
public class MenuTest{
    private Menu menu = new("Test Menu");
    private static void Nothing() {}

    [TestInitialize]
    public void Setup(){
        menu = new("Test Menu");
    }

    // Check that duplicates wont get added
    [TestMethod]
    public void AllOptions(){
        menu.AddAllOption("Nothing", Nothing);
        menu.AddAllOption("Nothing", Nothing);
    }

    [TestMethod]
    public void CurrentOptions(){
        menu.AddAllOption("Nothing", Nothing);
        menu.AddCurrentOption("Nothing");
        menu.AddCurrentOption("Nothing");
        menu.AddCurrentOption("Something");

        string MenuString = menu.MenuString();
        // Check if Nothing is in MenuString only
        Assert.IsTrue(MenuString.Contains("Nothing"));
        // Check if Nothing didn't get added twice
        Assert.AreEqual(1, Regex.Matches(MenuString, "Nothing").Count());
        // Check if Something didn't get added
        Assert.IsFalse(MenuString.Contains("Something"));
    }

}
