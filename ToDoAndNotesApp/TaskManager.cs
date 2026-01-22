using System;
using System.Collections.Generic;
using System.Linq;

public class TaskManager
{
    private IComponent? _rootComponent;
    private ISortStrategy _sortStrategy;
    private IGroupingStrategy _groupingStrategy;
    private List<IDeadlineObserver> _observers;

    // Initializes the manager with a root component and default strategies.
    public TaskManager(IComponent root)
    {
        _rootComponent = root;
        _observers = new List<IDeadlineObserver>();
        _sortStrategy = new DateSortStrategy();
        _groupingStrategy = new PriorityGroupingStrategy();
    }

    // Adds a component to a specific parent or the root structure.
    public void AddComponent(IComponent component, IComponent? parent = null)
    {
        if (parent != null && parent is Category parentCategory)
        {
            parentCategory.Add(component);
        }
        else if (_rootComponent is Category list)
        {
            list.Add(component);
        }
        else if (_rootComponent != null)
        {
            var generalCategory = new Category("General");
            generalCategory.Add(_rootComponent);
            generalCategory.Add(component);
            _rootComponent = generalCategory;
        }
        else
        {
            _rootComponent = component;
        }
    }

    // Removes a component from the hierarchy.
    public void RemoveComponent(IComponent component)
    {
        if (_rootComponent == component)
        {
            _rootComponent = null;
        }
        else if (_rootComponent is Category list)
        {
            list.Remove(component);
        }
    }

    // Registers an observer for deadline notifications.
    public void Attach(IDeadlineObserver observer)
    {
        _observers.Add(observer);
    }

    // Unregisters a deadline observer.
    public void Detach(IDeadlineObserver observer)
    {
        _observers.Remove(observer);
    }

    // Checks for tasks approaching their deadline and notifies observers.
    public void CheckDeadlines()
    {
        CheckDeadlinesRecursive(_rootComponent);
    }

    private void CheckDeadlinesRecursive(IComponent component)
    {
        if (component == null)
            return;

        if (component is Task task)
        {
            if (!task.IsDone && task.DueDate <= DateTime.Now.AddDays(3))
            {
                NotifyObservers(task);
            }
        }
        else if (component is Category category)
        {
            foreach (var child in category.Children)
            {
                CheckDeadlinesRecursive(child);
            }
        }
    }

    private void NotifyObservers(Task task)
    {
        foreach (var observer in _observers)
        {
            observer.Update(task);
        }
    }

    // Sets the strategy for sorting components.
    public void SetSortStrategy(ISortStrategy strategy)
    {
        _sortStrategy = strategy;
    }

    // Sets the strategy for grouping components.
    public void SetGroupingStrategy(IGroupingStrategy strategy)
    {
        _groupingStrategy = strategy;
    }

    // Returns a list of components sorted according to the current strategy.
    public List<IComponent> GetSortedComponents()
    {
        if (_rootComponent is Category category)
        {
            return _sortStrategy.Sort(category.Children);
        }
        return new List<IComponent> { _rootComponent };
    }

    // Returns components grouped according to the current strategy.
    public Dictionary<string, List<IComponent>> GetGroupedComponents()
    {
        if (_rootComponent is Category category)
        {
            return _groupingStrategy.Group(category.Children);
        }
        return new Dictionary<string, List<IComponent>>();
    }

    // Accepts a visitor to perform operations on the component structure.
    public void RunVisitor(IVisitor visitor)
    {
        _rootComponent?.Accept(visitor);
    }

    // Searches for components matching the keyword recursively.
    public List<IComponent> Search(string keyword)
    {
        var results = new List<IComponent>();
        SearchRecursive(_rootComponent, keyword, results);
        return results;
    }

    private void SearchRecursive(IComponent component, string keyword, List<IComponent> results)
    {
        if (component == null)
            return;

        bool match = false;
        if (component is Task task)
        {
            if (
                task.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || (
                    task.Description != null
                    && task.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                )
            )
            {
                match = true;
            }
        }
        else if (component is Note note)
        {
            if (
                note.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                || (
                    note.Content != null
                    && note.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                )
            )
            {
                match = true;
            }
        }
        else if (component is Category cat)
        {
            if (cat.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                match = true;
            }
        }

        if (match)
        {
            results.Add(component);
        }

        if (component is Category category)
        {
            foreach (var child in category.Children)
            {
                SearchRecursive(child, keyword, results);
            }
        }
    }

    // Finds the parent category of a specific component.
    public IComponent? FindParent(IComponent child)
    {
        return FindParentRecursive(_rootComponent, child);
    }

    private IComponent? FindParentRecursive(IComponent? current, IComponent target)
    {
        if (current is Category category)
        {
            if (category.Children.Contains(target))
            {
                return category;
            }
            foreach (var c in category.Children)
            {
                var found = FindParentRecursive(c, target);
                if (found != null)
                    return found;
            }
        }
        return null;
    }

    // Retrieves all categories in the hierarchy.
    public List<Category> GetAllCategories()
    {
        var categories = new List<Category>();
        if (_rootComponent is Category rootCat)
        {
            categories.Add(rootCat);
            GetCategoriesRecursive(rootCat, categories);
        }
        return categories;
    }

    private void GetCategoriesRecursive(Category current, List<Category> categories)
    {
        foreach (var child in current.Children)
        {
            if (child is Category cat)
            {
                categories.Add(cat);
                GetCategoriesRecursive(cat, categories);
            }
        }
    }
}
