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
}
