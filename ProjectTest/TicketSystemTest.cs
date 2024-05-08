// using System;
// using System.Text.Json.Serialization;
// using System.IO;

// namespace ProjectTest.TicketSystemTest;
// [TestClass]
// public class TicketSystemTest{
//     public static string JsonPath = @"DataSources/tickets.json";

//     //[ClassInitialize]
//     //public static void ClearPerformances(TestContext testContext){
//     //    File.WriteAllText(@"DataSources/performances.json",
//     //    "{\"ID0\": {\"Name\": \"Movie\",\"Genres\": [\"Genre\"],\"Active\": true}}");
//     //}


//     [TestInitialize]
//     public void ClearJsonFile(){
//         File.WriteAllText(@"DataSources/tickets.json", String.Empty);
//         App.Tickets.Clear();
//     }

//     [TestMethod]
//     // Test if Ticket.cs can update the Json file
//     public void TestMethodJsonUpdate(){
//         Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L", true);
//         testTicket.UpdateData();
//         Assert.IsTrue(!string.IsNullOrEmpty(File.ReadAllText(JsonPath)));
//     }

//     [TestMethod]
//     // Test if the Ticket reader works and actually returns the list List<KeyValueClass>
//     public void TestTicketReaderReturnsList(){
//         Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L", true);
//         testTicket.UpdateData();
//         Assert.IsTrue(App.Tickets.Count != 0 && App.Tickets.Count > 0);
//     }

//     [TestMethod]
//     // Test if the Tickets are able to be printed in the terminal
//     public void TestPrintTicketInfo(){
//         Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "ID0", true);
//         testTicket.UpdateData();
//         Ticket testTicket2 = new Ticket("ID0", "01-02-2001", "18:00", "ID0", true);
//         testTicket2.UpdateData();

//         string name = App.Performances["ID0"].Name;
//         string hall = App.Halls["ID0"].Name;
//         string location = App.Locations[App.Halls["ID0"].LocationId].Name;

//         Assert.AreEqual(App.Tickets[0].Ticket.TicketInfo(), $"The play you booked: {name}. On 01-02-2000 at 12:00 | {location} - {hall}.");
//         Assert.AreEqual(App.Tickets[1].Ticket.TicketInfo(), $"The play you booked: {name}. On 01-02-2001 at 18:00 | {location} - {hall}.");
//     }

//     [TestMethod]
//     public void TestMoreTickets(){
//         Ticket testTicket = new Ticket("ID0", "01-02-2000", "12:00", "L", true);
//         testTicket.UpdateData();
//         Ticket testTicket2 = new Ticket("ID0", "01-02-2001", "15:00", "A", true);
//         testTicket2.UpdateData();
//         Ticket testTicket3 = new Ticket("ID0", "01-02-2002", "18:00", "B", true);
//         testTicket3.UpdateData();
//         Assert.IsTrue(App.Tickets.Count != 0 && App.Tickets.Count > 2);
//     }

//     [TestMethod]
//     public void LoginCheckCorrectAdminTest(){
//         MainTicketSystem.IsTesting = (true, "Admin123", "Password123");
//         (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
//         Assert.AreEqual(tupleLogin, (true,"Admin123","Password123"));
//         MainTicketSystem.IsTesting = (false, "", "");
//     }

//     [TestMethod]
//     public void LoginCheckIncorrectAdminTest(){
//         MainTicketSystem.IsTesting = (true, "admin", "password");
//         (bool,string,string) tupleLogin = MainTicketSystem.LoginCheckAdmin();
//         Assert.AreEqual(tupleLogin, (false,"admin","password"));
//         MainTicketSystem.IsTesting = (false, "", "");
//     }

//     [TestMethod]
//     public void TestCheckOutdatedTickets(){
//         // Create some tickets with dates in the past and add them to App.Tickets
//         Ticket TestTicket1 = new Ticket("ID1", "22/04/2024", "12:00:00", "ID0", true);
//         Ticket TestTicket2 = new Ticket("ID1", "21/02/2024", "15:00:00", "ID0", true);
//         Ticket TestTicket3 = new Ticket("ID1", "01/06/2024", "01:00:00", "ID0", true);
//         UserTicket newUserTicket1 = new UserTicket("TestUser", TestTicket1);
//         UserTicket newUserTicket2 = new UserTicket("TestUser", TestTicket2);
//         UserTicket newUserTicket3 = new UserTicket("TestUser", TestTicket3);
//         App.Tickets.Add(newUserTicket1);
//         App.Tickets.Add(newUserTicket2);
//         App.Tickets.Add(newUserTicket3);

//         // Check outdated tickets
//         MainTicketSystem.CheckOutdatedTickets();

//         // Expected tickets
//         Ticket expectedTestTicket1 = new Ticket("ID1", "22/04/2024", "12:00:00", "ID0", false);
//         Ticket expectedTestTicket2 = new Ticket("ID1", "21/02/2024", "15:00:00", "ID0", false);
//         Ticket expectedTestTicket3 = new Ticket("ID1", "01/06/2024", "01:00:00", "ID0", true);

//         // Asserts that the IsActive property of the outdated tickets is false
//         UserTicket userTicketFound = App.Tickets.Find(userticket => userticket.Ticket == TestTicket1)!;
//         Assert.IsNotNull(userTicketFound);
//         // Assert.AreEqual(expectedTestTicket1, userTicketFound.Ticket);
//         Assert.IsTrue(expectedTestTicket1.Equals(userTicketFound.Ticket)); //Both Assert methods works
//         UserTicket userTicket2Found = App.Tickets.Find(userticket => userticket.Ticket == TestTicket2)!;
//         Assert.IsNotNull(userTicket2Found);
//         // Assert.AreEqual(expectedTestTicket2, userTicket2Found.Ticket);
//         Assert.IsTrue(expectedTestTicket2.Equals(userTicket2Found.Ticket)); //Both Assert methods works
//         UserTicket userTicketOutdatedFound = App.Tickets.Find(userticket => userticket.Ticket == TestTicket3)!;
//         Assert.IsNotNull(userTicketOutdatedFound);
//         // Assert.AreEqual(expectedTestTicket3, userTicketOutdatedFound.Ticket);
//         Assert.IsTrue(expectedTestTicket3.Equals(userTicketOutdatedFound.Ticket)); //Both Assert methods works

//         // Clean the test tickets for other tests and for main program
//         App.Tickets.Remove(newUserTicket1);
//         App.Tickets.Remove(newUserTicket2);
//         App.Tickets.Remove(newUserTicket3);
//         // TicketDataAccess.UpdateTickets();
//     }

//     [TestMethod]
//     public void TestCancellationIsNotOneDayBefore(){
//         // Create a ticket with a date more than one day in the future
//         Ticket TestTicket1 = new Ticket("ID1", "21/04/2024", "15:00:00", "ID0", true);
//         // Testing this for correct output. But this method won't get these types of tickets because it will be checked by CheckOutdatedTickets first
//         Ticket TestTicket2 = new Ticket("ID1", "21/03/2024", "15:00:00", "ID0", true);
//         Ticket TestTicket3 = new Ticket("ID1", "21/06/2024", "15:00:00", "ID0", true);
//         UserTicket newUserTicket1 = new UserTicket("TestUser", TestTicket1);
//         UserTicket newUserTicket2 = new UserTicket("TestUser", TestTicket2);
//         UserTicket newUserTicket3 = new UserTicket("TestUser", TestTicket3);
//         App.Tickets.Add(newUserTicket1);
//         App.Tickets.Add(newUserTicket2);
//         App.Tickets.Add(newUserTicket3);

//         // Cancel tickets
//         bool result1 = MainTicketSystem.CancellationIsNotOneDayBefore(newUserTicket1);
//         bool result2 = MainTicketSystem.CancellationIsNotOneDayBefore(newUserTicket2);
//         bool result3 = MainTicketSystem.CancellationIsNotOneDayBefore(newUserTicket3);

//         // Asserts if the method returns the correct boolean values
//         Assert.IsFalse(result1);
//         Assert.IsFalse(result2);
//         Assert.IsTrue(result3);
//     }

//     [TestMethod]
//     public void TestCancelTicketLogic(){
//         // Create a ticket and add it to App.Tickets
//         Ticket testTicketNormal = new Ticket("ID1", "01/05/2024", "15:00:00", "ID0", true);
//         Ticket testTicketNormal2 = new Ticket("ID1", "21/06/2024", "15:00:00", "ID0", true);
//         Ticket testTicketOutdated = new Ticket("ID1", "01/03/2024", "15:00:00", "ID0", true);
//         UserTicket newUserTicket = new UserTicket("TestUser", testTicketNormal);
//         UserTicket newUserTicket2 = new UserTicket("TestUser", testTicketNormal2);
//         UserTicket newUserTicketOutDated = new UserTicket("TestUser", testTicketOutdated);
//         App.Tickets.Add(newUserTicket);
//         App.Tickets.Add(newUserTicket2);
//         App.Tickets.Add(newUserTicketOutDated);

//         // Call CancelTicketLogic with the ticket
//         MainTicketSystem.CancelTicketLogic(newUserTicket);
//         MainTicketSystem.CancelTicketLogic(newUserTicket2);
//         MainTicketSystem.CancelTicketLogic(newUserTicketOutDated);

//         // Expected tickets after cancelling the tickets
//         Ticket expectedNormalTestTicket = new Ticket("ID1", "01/05/2024", "15:00:00", "ID0", false);
//         Ticket expectedNormalTestTicket2 = new Ticket("ID1", "21/06/2024", "15:00:00", "ID0", false);
//         Ticket expectedOutdatedTestTicket = new Ticket("ID1", "01/03/2024", "15:00:00", "ID0", false);

//         // Asserts that the ticket's IsActive property is now false
//         UserTicket userTicketFound = App.Tickets.Find(userticket => userticket.Ticket == testTicketNormal)!;
//         Assert.IsNotNull(userTicketFound);
//         Assert.AreEqual(expectedNormalTestTicket, userTicketFound.Ticket);
//         UserTicket userTicket2Found = App.Tickets.Find(userticket => userticket.Ticket == testTicketNormal2)!;
//         Assert.IsNotNull(userTicket2Found);
//         Assert.AreEqual(expectedNormalTestTicket2, userTicket2Found.Ticket);
//         UserTicket userTicketOutdatedFound = App.Tickets.Find(userticket => userticket.Ticket == testTicketOutdated)!;
//         Assert.IsNotNull(userTicketOutdatedFound);
//         Assert.AreEqual(expectedOutdatedTestTicket, userTicketOutdatedFound.Ticket);

//         // Clean the test tickets for other tests and for main program
//         App.Tickets.Remove(newUserTicket);
//         App.Tickets.Remove(newUserTicket2);
//         App.Tickets.Remove(newUserTicketOutDated);
//         TicketDataAccess.UpdateTickets();
//     }
// }
