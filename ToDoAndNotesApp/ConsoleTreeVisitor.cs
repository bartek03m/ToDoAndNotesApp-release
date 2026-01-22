using System;

// A visitor that prints the component tree (Tasks, Notes, Categories) to the console with indentation representing the hierarchy depth.
public class ConsoleTreeVisitor : IVisitor
{
    private int _currentDepth = 0;

    // Generates indentation string based on the current depth in the hierarchy.
    private string GetIndent()
    {
        return new string(' ', _currentDepth * 4);
    }

    public void VisitTask(Task task)
    {
        string indent = GetIndent();
        Console.WriteLine(
            $"{indent}[TASK] {task.Title} (Priority: {task.PriorityLevel}, Done: {task.IsDone})"
        );
        Console.WriteLine($"{indent}   Description: {task.Description}");
        Console.WriteLine($"{indent}   Due: {task.DueDate:yyyy-MM-dd}");

        PrintTags(task.Tags, indent);
    }

    public void VisitNote(Note note)
    {
        string indent = GetIndent();
        Console.WriteLine($"{indent}[NOTE] {note.Title}");
        Console.WriteLine($"{indent}   Content: {note.Content}");

        PrintTags(note.Tags, indent);
    }

    // Visits a Category component, printing its Name and recursively visiting its children with increased indentation.
    public void VisitCategory(Category category)
    {
        string indent = GetIndent();
        Console.WriteLine($"{indent}[CATEGORY] {category.Name}");

        _currentDepth++;
        foreach (var child in category.Children)
        {
            child.Accept(this);
        }
        _currentDepth--;
    }

    // Helper method to print tags with their associated colors.
    private void PrintTags(List<Tag> tags, string indent)
    {
        if (tags == null || tags.Count == 0)
            return;

        Console.Write($"{indent}   Tags: ");
        foreach (var tag in tags)
        {
            if (Enum.TryParse(tag.Color, out ConsoleColor color))
            {
                Console.ForegroundColor = color;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.Write($"[{tag.Name}] ");
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}
