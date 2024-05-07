public class PresentationBase<T> where T : IEditable{
    // Reference to logic class from T
    protected LogicBase<T> Logic;
    public PresentationBase(LogicBase<T> logic) => Logic = logic;
    
    public virtual void AddObject(){
        string? name = GetNameInput();
        if (name == null) return;
    }

    // Returns a string for name that was input
    public string? GetNameInput(){
        //Console.Clear();
        string inputName = "";
        bool validName = false;
        while (!validName){
            Console.WriteLine($"Enter name for {typeof(T).Name.ToLower()}\n(Enter nothing to exit)");
            inputName = Console.ReadLine() ?? "";
            if (inputName == ""){
                Console.WriteLine("Exiting..");
                Thread.Sleep(1500);
                return null;
            }
            validName = Logic.ValidName(inputName);
            if (!validName){
                Console.WriteLine($"{typeof(T).Name} with name {inputName} already exists\n");
            }
        }
        return inputName;
    }

    public int EditObject(string objectId){
        // Get object
        if (objectId == "") return 0;
        T obj = Logic.Dict[objectId];

        // Get all properties of type T
        var properties = typeof(T).GetProperties().Where(property => Attribute.IsDefined(property, typeof(EditableAttribute)));

        while (true){
            int index = 1;
            Console.Clear();
            Console.WriteLine($"What to change for this {typeof(T).Name.ToLower()}");
            foreach (var property in properties) {
                var val = typeof(T).GetProperty(property.Name)?.GetValue(obj, null);
                string value = "";
                string name = "";

                // Don't know a better way of doing this without "hardcoding" the type check
                // Values get formatted here properly
                if (val is IList<string> valList){
                    List<string> names = new();
                    foreach (string id in valList){

                        if (typeof(T) == typeof(Location)){
                            names.Add(App.hallLogic.Dict[id].Name);
                        } else if (typeof(T) == typeof(Performance)){
                            names.Add(App.genreLogic.Dict[id].Name);
                        } else names.Add(Logic.Dict[id].Name);
                    }
                    string seperator = ", ";
                    value = $"[{String.Join(seperator, names)}]";
                } else value = $"'{val}'";

                if (property.Name == "LocationId" && typeof(T) == typeof(Hall)){
                    name = "Location";
                    value = $"'{App.locationLogic.Dict[App.hallLogic.Dict[objectId].LocationId].Name}'";
                } else name = property.Name;

                Console.WriteLine($"{index++}: Change {property.Name.PadRight(25)} {value}");
            }
            Console.WriteLine($"{index}: Exit");

            Int32.TryParse(Console.ReadLine(), out int choice);
            Console.WriteLine();
            if (choice == 0 || choice > index){
                Console.WriteLine("Not a valid choice");
                Thread.Sleep(2500);
            } else if (choice == index){
                Console.WriteLine("Exiting..");
                DataAccess.UpdateItem<T>();
                Thread.Sleep(1500);
                return 0;
            } else if (choice == 1) {       // Because Name is first property, it will always be 1;
                string? newName = GetNameInput();
                if (newName == null) continue;
                Console.WriteLine($"Changed {obj.Name} to {newName}");
                obj.Name = newName;
                Thread.Sleep(2500);
            } else return choice;
        }
    }

    public string GetItem(string question, string exit, string locationId = ""){
        List<(string, string)> itemsOrdered = new();
        // Don't know a better way of doing this
        if (typeof(T) == typeof(Hall) && locationId != ""){
            if (!App.Locations.ContainsKey(locationId)) return "null";
            App.Locations[locationId].Halls.ForEach(hallId => itemsOrdered.Add((hallId, Logic.Dict[hallId].Name)));
        } else {
            foreach (var itemPair in Logic.Dict){
                itemsOrdered.Add((itemPair.Key, itemPair.Value.Name));
            }
        }
        itemsOrdered = itemsOrdered.OrderBy(itemPair => itemPair.Item2).ToList();

        int index = 1;
        string menu = "";
        while (true){
            int choice = -1;
            Console.Clear();
            Console.WriteLine(question);
            
            foreach (var itemPair in itemsOrdered){
                menu += $"{index++}: {itemPair.Item2}";

                // Again can't think of a way to seperate this better
                // if this object is a hall, then add the location of the hall after it
                if (typeof(T) == typeof(Hall)){
                    string locationIdOfHall = App.Halls[itemPair.Item1].LocationId;
                    menu += (locationIdOfHall == "null") ? "\tNo location" : $"\t({App.Locations[locationIdOfHall].Name})";
                }

                menu += "\n";
            }
            menu += $"{index}: {exit}";
            
            Console.WriteLine(menu);
            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    continue;
                }
                return itemsOrdered[choice - 1].Item1;
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == itemsOrdered.Count) return "null";
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
            }
        }
    }


    // Only for Genres and Halls
    // I know it looks and uses similar code to GetItem, but I can't think of any way to incorporate that code here
    public List<string> GetItemList(string objectId = ""){
        // Don't know a better way of doing this

        // itemIds is a list of the Ids which will eventually be returned
        List<string> itemIds = new();
        if (typeof(T) == typeof(Genre)){
            if (App.Performances.ContainsKey(objectId)) itemIds = App.Performances[objectId].Genres;
        } else if (typeof(T) == typeof(Hall)){
            if (App.Locations.ContainsKey(objectId)) itemIds = App.Locations[objectId].Halls;
        } else return new List<string>();

        // itemsOrdered is the items with id and name, which is used for printing what options are available
        List<(string, string)> itemsOrdered = new();
        foreach (var itemPair in Logic.Dict){
            // Don't add the genres that are int itemIds already
            if (typeof(T) == typeof(Genre) && itemIds.Contains(itemPair.Key)) continue;
            // Don't add the halls which are already linked to a location
            if (itemPair.Value.GetType() == typeof(Hall) && App.Halls[itemPair.Key].LocationId == "null") continue;
            itemsOrdered.Add((itemPair.Key, itemPair.Value.Name));
        }
        itemsOrdered = itemsOrdered.OrderBy(itemPair => itemPair.Item2).ToList();
        
        string seperator = ", ";
        while (true){
            Console.Clear();
            int index = 1;
            int choice = -1;

            // currentItems is a list of names of currently selected items, used for printing
            List<string> currentItems = new();
            itemIds.ForEach(itemId => currentItems.Add(Logic.Dict[itemId].Name));
            currentItems.Sort();

            Console.WriteLine($"Current {typeof(T).Name.ToLower()}s: [{String.Join(seperator, currentItems)}]\n");
            Console.WriteLine($"Choose {typeof(T).Name.ToLower()}s:");

            string menu = "";
            foreach (var itemPair in itemsOrdered){
                menu += $"{index++} {itemPair.Item2}\n";
            }
            menu += $"\n{index}: Confirm";
            Console.WriteLine(menu);

            try {
                if (!Int32.TryParse(Console.ReadLine(), out choice)){
                    Console.WriteLine("\nInvalid input\n");
                    Thread.Sleep(2500);
                } else {
                    itemIds.Add(itemsOrdered[choice - 1].Item1);
                    itemsOrdered.RemoveAt(choice - 1);
                }
            } catch (ArgumentOutOfRangeException){
                if (choice - 1 == itemsOrdered.Count){
                    return itemIds;
                } else {
                    Console.WriteLine("Invalid choice");
                    Thread.Sleep(2500);
                }
            }
        }
    }
}