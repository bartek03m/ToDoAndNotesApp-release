using System;
using System.Collections.Generic;
using System.Linq;

// Strategy implementation for sorting items by date
public class DateSortStrategy : ISortStrategy
{
    // Sorts the components from oldest to newest based on relevant dates
    public List<IComponent> Sort(List<IComponent> items)
    {
        // Validation: Check for null or empty input list
        if (items == null || !items.Any())
        {
            return new List<IComponent>();
        }

        return items
            .OrderBy(item =>
            {
                // Use DueDate for Tasks, fallback to CreationDate for other components (like Notes)
                if (item is Task task)
                {
                    return task.DueDate;
                }
                return item.CreationDate;
            })
            .ToList();
    }
}
