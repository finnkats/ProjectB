public interface IEditable : IDataAccessItem {
    public string Name {get; set;}
}

// Custom attribute for editable properties
[AttributeUsage(AttributeTargets.Property)]
public class EditableAttribute : System.Attribute {
    public EditableAttribute(){}
}
