using System.Collections.Generic;
using System.Linq;

// Strategy implementation for sorting items by priority
public class PrioritySortStrategy : ISortStrategy
{
    // Sorts the list of components based on their priority level (descending order)
    public List<IComponent> Sort(List<IComponent> items)
    {
        // Validation: Ensure the list is not null or empty before processing
        if (items == null || !items.Any())
        {
            return new List<IComponent>();
        }

        return items
            .OrderByDescending(item =>
            {
                // Check if the item is a Task to access the PriorityLevel property
                if (item is Task task)
                {
                    // Cast priority to int to allow numeric comparison
                    return (int)task.PriorityLevel;
                }
                // Default value for non-Task items (effectively treats them as lowest priority)
                return -1;
            })
            .ToList(); // Convert the sorted result back to a List
    }
}
