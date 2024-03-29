using System;
using System.Text.Json.Serialization;
using System.IO;

namespace ProjectTest.TicketSystemTest;
[TestClass]
public class TicketSystemTest{
    private string jsonTestPath = "../../../Sources/tickets.json";

    [TestInitialize]
    public void ClearJsonFile(){
        File.WriteAllText(jsonTestPath, String.Empty);
    }

    [TestMethod]
    // Test if Ticket.cs can update the Json file
    public void TestMethodJsonUpdate(){
        UpdateTicketJson.JsonTestPath = jsonTestPath;
        Ticket testTicket = new Ticket("Movie", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Assert.IsTrue(!string.IsNullOrEmpty(File.ReadAllText(jsonTestPath)));
        UpdateTicketJson.JsonTestPath = null;
    }

    [TestMethod]
    // Test if the Ticket reader works and actually returns the list List<KeyValueClass>
    public void TestTicketReaderReturnsList(){
        UpdateTicketJson.JsonTestPath = jsonTestPath;
        ReadTicketJson.JsonTestPath = jsonTestPath;
        Ticket testTicket = new Ticket("Movie", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        List<KeyValueClass>? resultList = ReadTicketJson.ReadTickets();
        Assert.IsTrue(resultList != null && resultList.Count > 0);
        ReadTicketJson.JsonTestPath = null;
        UpdateTicketJson.JsonTestPath = null;
    }

    [TestMethod]
    // Test if the Tickets are able to be printed in the terminal
    public void TestPrintTicketInfo(){
        UpdateTicketJson.JsonTestPath = jsonTestPath;
        ReadTicketJson.JsonTestPath = jsonTestPath;
        Ticket testTicket = new Ticket("Movie", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Ticket testTicket2 = new Ticket("Movie2", "01-02-2001", "18:00", "A");
        testTicket2.UpdateData();
        List<KeyValueClass> resultList = ReadTicketJson.ReadTickets()!;
        Assert.AreEqual(resultList[0].Ticket.TicketInfo(), "The play you booked: Movie. On 01-02-2000 at 12:00 | L");
        Assert.AreEqual(resultList[1].Ticket.TicketInfo(), "The play you booked: Movie2. On 01-02-2001 at 18:00 | A");
        ReadTicketJson.JsonTestPath = null;
        UpdateTicketJson.JsonTestPath = null;
    }

    [TestMethod]
    public void TestMoreTickets(){
        UpdateTicketJson.JsonTestPath = jsonTestPath;
        ReadTicketJson.JsonTestPath = jsonTestPath;
        Ticket testTicket = new Ticket("Movie", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Ticket testTicket2 = new Ticket("Movie2", "01-02-2001", "15:00", "A");
        testTicket2.UpdateData();
        Ticket testTicket3 = new Ticket("Movie3", "01-02-2002", "18:00", "B");
        testTicket3.UpdateData();
        List<KeyValueClass> resultList = ReadTicketJson.ReadTickets()!;
        Assert.IsTrue(resultList != null && resultList.Count > 2);
        ReadTicketJson.JsonTestPath = null;
        UpdateTicketJson.JsonTestPath = null;
    }
}