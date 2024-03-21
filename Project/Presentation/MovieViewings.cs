public class DisplayMovieViewings{
public void Start()
         {}

private void DisplayMovieOptions(Dictionary<string, List<MovieViewing>> movieOptions, string chosenLocation, string chosenMovie)
         {
            Console.WriteLine($"\nBeschikbare vertoningen van {chosenMovie} op {chosenLocation}:");
            foreach (var option in movieOptions[chosenMovie])
            {
                if (option.Location.Equals(chosenLocation, StringComparison.OrdinalIgnoreCase))
                {
                     Console.WriteLine($"Datum: {option.Date}, Tijd: {option.Time}, Zaal: {option.Zaal}");
                }
            }
        }
}