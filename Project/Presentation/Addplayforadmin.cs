class AddPlayForAdmin
{
  public static void AdminTool()
      {
          Console.WriteLine("Enter a movie/play you want to add.");
          Console.WriteLine("\nFilmname: ");
          string MovieName = Console.ReadLine();
  
  
          List<string> Genres = new List<string>();
          bool LoopCheck = true;
          while (LoopCheck == true)
          {
              Console.WriteLine("Enter Q if you want to stop adding Genres");
              Console.WriteLine("Genre: ");
              string Genre = Console.ReadLine();
              
              switch(Genre)
              {
                  case "Q":
                      LoopCheck = false;
                      break;
  
                  case "q":
                      LoopCheck = false;
                      break;
  
                  default:
                      Genres.Add(Genre);
                      break;
  
              }
          }
              
  
          Console.WriteLine("Is the movie/play currently active?");
          Console.WriteLine("1. Yes");
          Console.WriteLine("2. No");
  
          string Active_input = Console.ReadLine();
          bool Active = true;
  
          if (Active_input == "2")
          {
              Active = false;
          }  
  
          Play Film = new Play(MovieName, Genres, Active);
          AddFilm.AddToJson(Film);
          Console.WriteLine("Movie succesfully added");
      }   
}
