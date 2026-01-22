// Represents a composite node that groups tasks, notes, and sub-categories in a tree structure
public class Category : IComponent
{
    public string Name { get; set; }
    public List<IComponent> Children { get; }
    public DateTime CreationDate { get; }

    // Initializes the category acting as a container for hierarchical components
    public Category(string? name, List<IComponent>? children = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            name = "Untitled category";
        Name = name;
        Children = children ?? new List<IComponent>();
        CreationDate = DateTime.Now;
    }

    // Internal constructor used by the Clone method to ensure deep copy consistency
    private Category(string name, List<IComponent> children, DateTime creationDate)
    {
        Name = name;
        Children = children;
        CreationDate = creationDate;
    }

    public void Display()
    {
        Console.WriteLine($"CATEGORY: {Name}");

        if (!Children.Any())
            return;
        foreach (var component in Children)
        {
            component.Display();
        }
    }

    public void Add(IComponent component)
    {
        if (component == null)
            return;
        Children.Add(component);
    }

    public void Remove(IComponent component)
    {
        if (component == null)
            return;
        Children.Remove(component);
    }

    // Directs the visitor to process this category, usually triggering recursive traversal
    public void Accept(IVisitor visitor)
    {
        visitor.VisitCategory(this);
    }

    // Performs a deep copy of the tree branch, cloning all children recursively to avoid reference issues
    public IComponent Clone()
    {
        var clonedChildren = new List<IComponent>();
        foreach (var child in Children)
        {
            clonedChildren.Add(child.Clone());
        }
        return new Category(Name, clonedChildren, CreationDate);
    }
}
