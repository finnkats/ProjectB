using System.Text.RegularExpressions;

namespace ProjectTest.MenuTests;

[TestClass]
public class MenuTest{
    private Menu menu = new("Test Menu");
    private static void Nothing() {}
    private static void Something() {}

    [TestInitialize]
    public void Setup(){
        menu = new("Test Menu");
    }

    [TestMethod]
    public void AllOptions(){
        // Check that duplicates wont get added
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

    [TestMethod]
    public void GetFunction(){
        menu.AddAllOption("Nothing", Nothing);
        menu.AddAllOption("Something", Something);
        menu.AddCurrentOption("Nothing");
        menu.AddCurrentOption("Something");

        // Function should be Nothing
        Action? func_nothing = menu.GetFunction(1);
    	Assert.AreSame((Action)Nothing, func_nothing);
        // Function should be Something
        Action? func_something = menu.GetFunction(2);
    	Assert.AreSame((Action)Something, func_something);

        // Should be null
        Action? func_zero = menu.GetFunction(0);
    	Assert.AreSame(null, func_zero);
        Action? func_high = menu.GetFunction(99999);
    	Assert.AreSame(null, func_high);
        Action? func_low = menu.GetFunction(-39);
    	Assert.AreSame(null, func_low);
    }
}
