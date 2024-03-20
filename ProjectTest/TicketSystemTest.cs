namespace ProjectTest.TicketSystemTest;
[TestClass]
public class TicketSystemTest{
    [TestMethod]
    public void TestMethodJsonUpdate(){
        UpdateTicketJson.JsonTestPath = "../../../Sources/tickets.json";
        Ticket testTicket = new Ticket("Movie", "01-02-2000", "12:00", "L");
        Assert.IsTrue(!string.IsNullOrEmpty(File.ReadAllText("../../../Sources/tickets.json")));
        UpdateTicketJson.JsonTestPath = null;
    }
}
