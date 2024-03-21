using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class MovieViewing {
        public string Location { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public string Zaal { get; set; }
    }
class MovieReservationLogic {
        // Functie om JSON-gegevens in te lezen
        public Dictionary<string, List<MovieViewing>> ReadMovieOptionsFromJson(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, List<MovieViewing>>>(jsonData);
        }

        // Functie om locatie te selecteren
        public string SelectLocation()
        {
            Console.WriteLine("Kies een locatie:");
            Console.WriteLine("1. Schouwburg");
            Console.WriteLine("2. Zuidplein");
            Console.Write("Selecteer een optie: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    return "Schouwburg";
                case "2":
                    return "Zuidplein";
                default:
                    Console.WriteLine("Ongeldige keuze.");
                    return SelectLocation();
            }
        }
    }

