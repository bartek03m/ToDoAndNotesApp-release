using System;
using System.Collections.Generic;

// Represents a specific unit of work with priority, status, and due date.
public class Task : IComponent
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public Priority PriorityLevel { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsDone { get; set; }
    public List<Tag> Tags { get; set; }
    public DateTime CreationDate { get; }

    public Task(
        string? title,
        string? description = null,
        Priority priority = Priority.Low,
        DateTime? dueDate = null,
        bool isDone = false,
        List<Tag>? tags = null
    )
    {
        if (string.IsNullOrWhiteSpace(title))
            title = "Untitled task";
        Title = title;
        Description = description ?? "";
        PriorityLevel = priority;
        DueDate = dueDate ?? DateTime.Now;
        IsDone = isDone;
        Tags = tags ?? new List<Tag>();
        CreationDate = DateTime.Now;
    }

    // Internal constructor used by the Clone method to ensure deep copy consistency
    private Task(
        string title,
        string description,
        Priority priority,
        DateTime dueDate,
        bool isDone,
        List<Tag> tags,
        DateTime creationDate
    )
    {
        Title = title;
        Description = description;
        PriorityLevel = priority;
        DueDate = dueDate;
        IsDone = isDone;
        Tags = tags;
        CreationDate = creationDate;
    }

    public void Display()
    {
        var status = IsDone ? "DONE" : "TO DO";

        Console.WriteLine($"{status} Task: {Title} (Priority: {PriorityLevel})");
        Console.WriteLine($"    Due: {DueDate:yyyy-MM-dd}");
        Console.WriteLine($"    Description: {Description}");

        if (!Tags.Any())
            return;
        Console.Write($"    Tags: ");
        foreach (var tag in Tags)
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
        Console.WriteLine();
    }

    public void MarkAsDone()
    {
        IsDone = true;
    }

    // Dispatches the visitor to the specific VisitTask method
    public void Accept(IVisitor visitor)
    {
        visitor.VisitTask(this);
    }

    // Creates a deep copy of the task using the Prototype pattern
    public IComponent Clone()
    {
        var clonedTags = new List<Tag>(Tags);
        return new Task(
            Title,
            Description ?? "",
            PriorityLevel,
            DueDate,
            IsDone,
            clonedTags,
            CreationDate
        );
    }
}
