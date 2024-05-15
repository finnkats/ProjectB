using System;
using System.Text.Json.Serialization;
using System.IO;

namespace ProjectTest.TicketSystemTest;
[TestClass]
public class TicketSystemTest{
    [ClassInitialize]
    public static void Setup(TestContext testContext)
        {
            App.LoggedInUsername = "User1";
        }

    [TestInitialize]
    public void ClearJsonFile(){
        TestDataFiller.FillApp();
    }

    // [TestMethod]
    // // Test if the Tickets are able to be printed in the terminal
    // public void TestPrintTicketInfo(){
    //     Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "ID0", true);
    //     testTicket.UpdateData();
    //     Ticket testTicket2 = new Ticket("ID0", "01-02-2001", "18:00", "ID0", true);
    //     testTicket2.UpdateData();

    //     string name = App.Performances["ID0"].Name;
    //     string hall = App.Halls["ID0"].Name;
    //     string location = App.Locations[App.Halls["ID0"].LocationId].Name;

    //     Assert.AreEqual(App.Tickets[0].Ticket.TicketInfo(), $"The play you booked: {name}. On 01-02-2000 at 12:00 | {location} - {hall}.");
    //     Assert.AreEqual(App.Tickets[1].Ticket.TicketInfo(), $"The play you booked: {name}. On 01-02-2001 at 18:00 | {location} - {hall}.");
    // }

    // [TestMethod]
    // public void LoginCheckCorrectAdminTest(){
    //     MainTicketSystem.IsTesting = (true, "Admin123", "Password123");
    //     (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
    //     Assert.AreEqual(tupleLogin, (true,"Admin123","Password123"));
    //     MainTicketSystem.IsTesting = (false, "", "");
    // }

    // [TestMethod]
    // public void LoginCheckIncorrectAdminTest(){
    //     MainTicketSystem.IsTesting = (true, "admin", "password");
    //     (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
    //     Assert.AreEqual(tupleLogin, (false,"admin","password"));
    //     MainTicketSystem.IsTesting = (false, "", "");
    // }

    [TestMethod]
    public void TestCheckOutdatedTickets(){
        // Create some tickets with dates in the past and add them to App.Tickets
        string monthAgo = DateTime.Now.AddMonths(-1).ToString(@"dd\/MM\/yyyy");
        string yesterday = DateTime.Now.AddDays(-1).ToString(@"dd\/MM\/yyyy");
        string nextMonth = DateTime.Now.AddMonths(1).ToString(@"dd\/MM\/yyyy");
        MainTicketSystem.CreateBookTicket("ID1", monthAgo, "12:00:00", "ID0", true);
        MainTicketSystem.CreateBookTicket("ID1", yesterday, "15:00:00", "ID0", true);
        MainTicketSystem.CreateBookTicket("ID1", nextMonth, "01:00:00", "ID0", true);

        // Check outdated tickets
        MainTicketSystem.CheckOutdatedTickets();

        // Expected tickets
        Ticket expectedTestTicket1 = new Ticket("ID1", monthAgo, "12:00:00", "ID0", false);
        Ticket expectedTestTicket2 = new Ticket("ID1", yesterday, "15:00:00", "ID0", false);
        Ticket expectedTestTicket3 = new Ticket("ID1", nextMonth, "01:00:00", "ID0", true);

        // Asserts that the IsActive property of the outdated tickets is false
        //Ticket? TicketFound1 = App.Tickets["User1"].Find(userticket => userticket.Date == expectedTestTicket1.Date)!;
        Ticket? TicketFound1 = App.Tickets["User1"].Find(userticket => userticket.Date == expectedTestTicket1.Date)!;
        Assert.IsNotNull(TicketFound1);
        Assert.IsTrue(expectedTestTicket1.Equals(TicketFound1)); //Both Assert methods works

        Ticket? TicketFound2 = App.Tickets["User1"].Find(userticket => userticket.Date == expectedTestTicket2.Date)!;
        Assert.IsNotNull(TicketFound2);
        Assert.IsTrue(expectedTestTicket2.Equals(TicketFound2)); //Both Assert methods works

        Ticket? TicketFound3 = App.Tickets["User1"].Find(userticket => userticket.Date == expectedTestTicket3.Date)!;
        Assert.IsNotNull(TicketFound3);
        Assert.IsTrue(expectedTestTicket3.Equals(TicketFound3)); //Both Assert methods works
    }

    [TestMethod]
    public void TestCancellationIsNotOneDayBefore(){
        string yesterday = DateTime.Now.AddDays(-1).ToString(@"dd\/MM\/yyyy");
        string today = DateTime.Now.ToString(@"dd\/MM\/yyyy");
        string nextMonth = DateTime.Now.AddMonths(1).ToString(@"dd\/MM\/yyyy");
    
        Ticket Ticket1 = new Ticket("ID0", yesterday, "12:00", "ID0", true);
        Ticket Ticket2 = new Ticket("ID0", today, "12:00", "ID0", true);
        Ticket Ticket3 = new Ticket("ID0", nextMonth, "12:00", "ID0", true);

        App.Tickets["User1"].Add(Ticket1);
        App.Tickets["User1"].Add(Ticket2);
        App.Tickets["User1"].Add(Ticket3);

        // Cancel tickets
        bool result1 = MainTicketSystem.CancellationIsNotOneDayBefore(Ticket1);
        bool result2 = MainTicketSystem.CancellationIsNotOneDayBefore(Ticket2);
        bool result3 = MainTicketSystem.CancellationIsNotOneDayBefore(Ticket3);

        // Asserts if the method returns the correct boolean values
        Assert.IsFalse(result1);
        Assert.IsFalse(result2);
        Assert.IsTrue(result3);
    }
    [TestMethod]
    public void TestAddBooking()
    {
        string nextMonth = DateTime.Now.AddMonths(1).ToString(@"dd\/MM\/yyyy");

        Play Play1 = new Play("ID0", "18:00:00", nextMonth, "ID5", "ID0");

        Ticket Ticket1 = new Ticket("ID0", nextMonth, "18:00:00", "ID5", true);
        Ticket Ticket2 = new Ticket("ID0", nextMonth, "18:00:00", "ID5", true);
        Ticket Ticket3 = new Ticket("ID0", nextMonth, "18:00:00", "ID5", true);

        App.Plays["ID0"].Add(Play1);

        PlayLogic.AddBooking(Ticket1);
        Assert.AreEqual(1, App.Plays["ID0"][0].BookedSeats);
        PlayLogic.AddBooking(Ticket2);
        Assert.AreEqual(2, App.Plays["ID0"][0].BookedSeats);
        PlayLogic.AddBooking(Ticket3);
        Assert.AreEqual(3, App.Plays["ID0"][0].BookedSeats);
    }
}
