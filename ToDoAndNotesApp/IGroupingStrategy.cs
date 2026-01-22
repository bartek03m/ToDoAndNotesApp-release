public interface IGroupingStrategy
{
    // Defines a contract for grouping a list of components into a dictionary
    // Key = Group Name, Value = List of items in that group
    Dictionary<string, List<IComponent>> Group(List<IComponent> items);
}
