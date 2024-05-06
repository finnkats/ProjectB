public class PresentationBase<T> where T : IEditable{
    // Reference to logic class from T
    private LogicBase<T> Logic;
    public PresentationBase(LogicBase<T> logic) => Logic = logic;
    
    public virtual void AddObject(){
        string? name = GetNameInput();
        if (name == null) return;
    }

    // Returns a string for name that was input
    public string? GetNameInput(){
        Console.Clear();
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
        }
        return inputName;
    }
}
