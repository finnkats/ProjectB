public static class ChooseViewing{
    public static void Choose(string playID){
        var AllViewings = PlayReader.ReadMovieOptionsFromJson("DataSources/plays.json", playID);
        string ViewingLocation = PlaysPresentation.SelectLocation();
        string? ViewingDate = PlaysPresentation.PrintDates(ViewingLocation, AllViewings);
        if (ViewingDate == null) return;
        string? ViewingTime = PlaysPresentation.PrintTimes(ViewingLocation, ViewingDate, AllViewings);
        if (ViewingTime == null) return;

        string ViewingHall = "";
        foreach (var viewing in AllViewings){
            if (viewing.Date == ViewingDate && viewing.Time == ViewingTime){
                ViewingHall = viewing.Hall;
                break;
            }
        }

        Console.Clear();
        Console.WriteLine($"You have chosen {ViewingTime} on {ViewingDate} at {ViewingLocation} in hall {ViewingHall}");
    }
}
