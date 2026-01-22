using System.Collections.Generic;
using System.Linq;

// Strategy for grouping items by their assigned tags
public class TagGroupingStrategy : IGroupingStrategy
{
    public Dictionary<string, List<IComponent>> Group(List<IComponent> items)
    {
        var groups = new Dictionary<string, List<IComponent>>();

        if (items == null || !items.Any())
            return groups;

        foreach (var item in items)
        {
            var tags = new List<Tag>();

            // Retrieve tags depending on whether the item is a Task or a Note
            if (item is Task task)
                tags = task.Tags;
            else if (item is Note note)
                tags = note.Tags;

            // If the item has no tags, place it in the "No Tags" group
            if (tags == null || !tags.Any())
            {
                AddToGroup(groups, "No Tags", item);
                continue;
            }

            // If an item has multiple tags, add it to each corresponding group
            // (One item can appear in multiple tag groups)
            foreach (var tag in tags)
            {
                AddToGroup(groups, tag.Name, item);
            }
        }

        return groups;
    }

    // Helper method to handle dictionary initialization and item addition
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
