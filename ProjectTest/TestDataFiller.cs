using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTest
{
    public static class TestDataFiller
    {
        public static void FillApp()
        {
            DataAccess.FilePrefix = "../../../Sources/";

            File.WriteAllText("../../../Sources/accounts.json", File.ReadAllText("../../../Sources/accountsStatic.json"));
            App.Accounts = DataAccess.ReadItem<Account>();

            File.WriteAllText("../../../Sources/halls.json", File.ReadAllText("../../../Sources/hallsStatic.json"));
            App.Halls = DataAccess.ReadItem<Hall>();
            App.hallLogic.Dict = App.Halls;

            File.WriteAllText("../../../Sources/locations.json", File.ReadAllText("../../../Sources/locationsStatic.json"));
            App.Locations = DataAccess.ReadItem<Location>();
            App.locationLogic.Dict = App.Locations;

            File.WriteAllText("../../../Sources/performances.json", File.ReadAllText("../../../Sources/performancesStatic.json"));
            App.Performances = DataAccess.ReadItem<Performance>();
            App.performanceLogic.Dict = App.Performances;

            File.WriteAllText("../../../Sources/genres.json", File.ReadAllText("../../../Sources/genresStatic.json"));
            App.Genres = DataAccess.ReadItem<Genre>();
            App.genreLogic.Dict = App.Genres;
    
            File.WriteAllText("../../../Sources/plays.json", File.ReadAllText("../../../Sources/playsStatic.json"));
            App.Plays = DataAccess.ReadList<Play>();

            File.WriteAllText("../../../Sources/tickets.json", File.ReadAllText("../../../Sources/ticketsStatic.json"));
            App.Tickets = DataAccess.ReadList<Ticket>();
        }
    }
}
