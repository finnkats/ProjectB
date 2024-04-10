using System;
using System.Text.Json.Serialization;
using System.IO;

namespace ProjectTest.TicketSystemTest;
[TestClass]
public class TicketSystemTest{
    public static string JsonPath = @"DataSources/tickets.json";

    [ClassInitialize]
    public static void ClearPerformances(TestContext testContext){
        File.WriteAllText(@"DataSources/performances.json",
        "{\"ID0\": {\"Name\": \"Movie\",\"Genres\": [\"Genre\"],\"Active\": true}}");
    }


    [TestInitialize]
    public void ClearJsonFile(){
        File.WriteAllText(@"DataSources/tickets.json", String.Empty);
        App.Tickets.Clear();
    }

    [TestMethod]
    // Test if Ticket.cs can update the Json file
    public void TestMethodJsonUpdate(){
        Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Assert.IsTrue(!string.IsNullOrEmpty(File.ReadAllText(JsonPath)));
    }

    [TestMethod]
    // Test if the Ticket reader works and actually returns the list List<KeyValueClass>
    public void TestTicketReaderReturnsList(){
        Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Assert.IsTrue(App.Tickets != null && App.Tickets.Count > 0);
    }

    [TestMethod]
    // Test if the Tickets are able to be printed in the terminal
    public void TestPrintTicketInfo(){
        Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Ticket testTicket2 = new Ticket("ID0", "01-02-2001", "18:00", "A");
        testTicket2.UpdateData();
        Assert.AreEqual(App.Tickets[0].Ticket.TicketInfo(), "The play you booked: Movie. On 01-02-2000 at 12:00 | L");
        Assert.AreEqual(App.Tickets[1].Ticket.TicketInfo(), "The play you booked: Movie. On 01-02-2001 at 18:00 | A");
    }

    [TestMethod]
    public void TestMoreTickets(){
        Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L");
        testTicket.UpdateData();
        Ticket testTicket2 = new Ticket("ID0", "01-02-2001", "15:00", "A");
        testTicket2.UpdateData();
        Ticket testTicket3 = new Ticket("ID0", "01-02-2002", "18:00", "B");
        testTicket3.UpdateData();
        Assert.IsTrue(App.Tickets != null && App.Tickets.Count > 2);
    }

    [TestMethod]
    public void LoginCheckCorrectAdminTest(){
        MainTicketSystem.IsTesting = (true, "Admin123", "Password123");
        (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
        Assert.AreEqual(tupleLogin, (true,"Admin123","Password123"));
        MainTicketSystem.IsTesting = (false, "", "");
    }

    [TestMethod]
    public void LoginCheckIncorrectAdminTest(){
        MainTicketSystem.IsTesting = (true, "admin", "password");
        (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
        Assert.AreEqual(tupleLogin, (false,"admin","password"));
        MainTicketSystem.IsTesting = (false, "", "");
    }
}
