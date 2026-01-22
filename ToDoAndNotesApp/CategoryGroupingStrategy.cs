using System.Collections.Generic;
using System.Linq;

// Strategy for grouping items by their specific type (Category, Task, Note)
public class CategoryGroupingStrategy : IGroupingStrategy
{
    public Dictionary<string, List<IComponent>> Group(List<IComponent> items)
    {
        var groups = new Dictionary<string, List<IComponent>>();

        // Return empty dictionary if input is null or empty
        if (items == null || !items.Any())
        {
            return groups;
        }

        foreach (var item in items)
        {
            // Determine the group key based on the runtime type of the component
            string key = item switch
            {
                Category => "Sub-categories",
                Task => "Tasks",
                Note => "Notes",
                _ => "Other", // Fallback for unknown types
            };

            // Add the item to the determined group
            AddToGroup(groups, key, item);
        }

        return groups;
    }

    // Helper method to safely add an item to the dictionary, initializing the list if needed
    private void AddToGroup(
        Dictionary<string, List<IComponent>> groups,
        string key,
        IComponent item
    )
    {
        if (!groups.ContainsKey(key))
        {
            groups[key] = new List<IComponent>();
        }
        groups[key].Add(item);
    }
}
