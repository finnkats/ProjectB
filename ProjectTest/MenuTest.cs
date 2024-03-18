namespace ProjectTest.MenuTests;

[TestClass]
public class MenuTest{
    private Menu menu = new("Test Menu");
    private static void Nothing() {}

    [TestInitialize]
    public void Setup(){
        menu = new("Test Menu");
    }

    [TestMethod]
    public void AllOptions(){
        menu.AddAllOption("Nothing", Nothing);
        menu.AddAllOption("Nothing", Nothing);
    }

    /*[TestMethod]
    public void CurrentOptions(){
        menu.AddAllOption("Nothing", Nothing);
        menu.AddCurrentOption("Nothing");
        menu.AddCurrentOption("Something");
    }*/

}
