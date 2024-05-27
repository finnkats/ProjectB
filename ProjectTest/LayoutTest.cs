namespace ProjectTest.LayoutTest;

[TestClass]
public class GenreTest
{
    [TestInitialize]
    public void Reset(){
        TestDataFiller.FillApp();
    }

    [TestMethod]
    public void OrderSeatsTest(){
        // Prepare user for who's order will be placed and what play it will be on
        App.LoggedInUsername = "User1";
        App.Plays["ID0"].Add(new Play("ID0", "12:00", "01/06/2024", "ID0", "ID0"));

        // One seat
        Assert.IsFalse(App.Plays["ID0"][0].Seats.Contains(1));
        MainTicketSystem.CreateBookTicket("ID0", "01/06/2024", "12:00", "ID0", new HashSet<int>(){1});
        Assert.IsTrue(App.Plays["ID0"][0].Seats.Contains(1));

        // Multiple seats
        Assert.IsFalse(App.Plays["ID0"][0].Seats.Contains(5) && App.Plays["ID0"][0].Seats.Contains(6) && App.Plays["ID0"][0].Seats.Contains(7));
        MainTicketSystem.CreateBookTicket("ID0", "01/06/2024", "12:00", "ID0", new HashSet<int>(){5, 6, 7});
        Assert.IsTrue(App.Plays["ID0"][0].Seats.Contains(5) && App.Plays["ID0"][0].Seats.Contains(6) && App.Plays["ID0"][0].Seats.Contains(7));
    }

    [TestMethod]
    public void RefundSeatsTest(){
        // Prepare user for who's order will be placed and what play it will be on
        App.LoggedInUsername = "User1";
        string performance = "ID0", date = "01/06/2024", time = "12:00", hall = "ID0";
        App.Plays["ID0"].Add(new Play("ID0", time, date, hall, performance));

        // One seat
        MainTicketSystem.CreateBookTicket(performance, date, time, hall, new HashSet<int>(){1});
        Assert.IsTrue(App.Plays["ID0"][0].Seats.Contains(1));
        PlayLogic.RemoveBooking(new Ticket(performance, date, time, hall, new int[]{1}, true, 1));
        Assert.IsFalse(App.Plays["ID0"][0].Seats.Contains(1));

        // Multiple seats
        MainTicketSystem.CreateBookTicket("ID0", "01/06/2024", "12:00", "ID0", new HashSet<int>(){5, 6, 7});
        Assert.IsTrue(App.Plays["ID0"][0].Seats.Contains(5) && App.Plays["ID0"][0].Seats.Contains(6) && App.Plays["ID0"][0].Seats.Contains(7));
        PlayLogic.RemoveBooking(new Ticket(performance, date, time, hall, new int[]{5, 6, 7}, true, 2));
        Assert.IsFalse(App.Plays["ID0"][0].Seats.Contains(5) && App.Plays["ID0"][0].Seats.Contains(6) && App.Plays["ID0"][0].Seats.Contains(7));
    }

}
