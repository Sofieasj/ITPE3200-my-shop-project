using System.ComponentModel.DataAnnotations; // namespace - provides basic classes and base classes

// namespaces are used to organize code in hierarchical structure
namespace MyShop.Models // signifies code belonging to Models-folder or component within MyShop-application
{
    // public class accessible from other classes and components
    // object blueprint - like Java
    public class Item
    {
        // to follow C# conventions - member variables must start with upper case
        // get; set; - auto-implements public getter and setter
        public int ItemId {get; set; }
        // alternative to ? - .Empty and default! (for clases) to signify optional
        public string Name {get; set; } = string.Empty; // ensures it is never null - avoiding null reference issues
        public decimal Price { get; set; }
        // ? means nullable as in optional - can hold string or null
        public string? Description {get; set; }
        public string? ImageUrl {get; set; }
        // navigation property - represents relationship between entities
        public virtual List<OrderItem>? OrderItems {get; set; }
    }
}