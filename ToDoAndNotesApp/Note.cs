// Represents an individual element in the composite structure containing text content
public class Note : IComponent
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Tag> Tags { get; set; }
    public DateTime CreationDate { get; }

    public Note(string? title, string? content = null, List<Tag>? tags = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            title = "Untitled note";
        Title = title;
        Content = content ?? "";
        Tags = tags ?? new List<Tag>();
        CreationDate = DateTime.Now;
    }

    // Internal constructor used by the Clone method to ensure deep copy consistency
    private Note(string title, string content, List<Tag> tags, DateTime creationDate)
    {
        Title = title;
        Content = content;
        Tags = tags;
        CreationDate = creationDate;
    }

    public void Display()
    {
        Console.WriteLine($"NOTE: {Title}");
        Console.WriteLine($"    Content: {Content}");

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

    // Dispatches the visitor to the specific VisitNote method
    public void Accept(IVisitor visitor)
    {
        visitor.VisitNote(this);
    }

    // Creates a deep copy of the note using the Prototype pattern
    public IComponent Clone()
    {
        var clonedTags = new List<Tag>(Tags);
        return new Note(Title, Content, clonedTags, CreationDate);
    }
}
