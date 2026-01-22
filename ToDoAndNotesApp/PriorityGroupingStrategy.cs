using System.Collections.Generic;
using System.Linq;

// Strategy for grouping items based on their priority level
public class PriorityGroupingStrategy : IGroupingStrategy
{
    public Dictionary<string, List<IComponent>> Group(List<IComponent> items)
    {
        var groups = new Dictionary<string, List<IComponent>>();

        if (items == null || !items.Any())
            return groups;

        foreach (var item in items)
        {
            string key;

            // Check if the item is a Task to access its PriorityLevel
            if (item is Task task)
            {
                key = task.PriorityLevel.ToString();
            }
            else
            {
                // Notes and Categories do not have priority, so they go to a separate group
                key = "No Priority";
            }

            // Ensure the group exists and add the item
            if (!groups.ContainsKey(key))
            {
                groups[key] = new List<IComponent>();
            }
            groups[key].Add(item);
        }

        return groups;
    }
}
