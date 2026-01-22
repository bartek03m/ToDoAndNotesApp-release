using System.Text;

// A visitor responsible for generating a detailed text report of the component hierarchy. Accumulates the report in a StringBuilder.
public class ReportVisitor : IVisitor
{
    private StringBuilder _reportBuffer = new();

    private int _currentDepth = 0;

    public ReportVisitor() { }

    // Generates indentation string based on the current recursion depth.
    private string GetIndent()
    {
        return new string(' ', _currentDepth * 4);
    }

    public void VisitTask(Task task)
    {
        string indent = GetIndent();
        _reportBuffer.AppendLine(
            $"{indent}[TASK] {task.Title} (Priority: {task.PriorityLevel}, Done: {task.IsDone})"
        );
        _reportBuffer.AppendLine($"{indent}   Description: {task.Description}");
        _reportBuffer.AppendLine($"{indent}   Due: {task.DueDate}");
    }

    public void VisitNote(Note note)
    {
        string indent = GetIndent();
        _reportBuffer.AppendLine($"{indent}[NOTE] {note.Title}");
        _reportBuffer.AppendLine($"{indent}   Content: {note.Content}");
    }

    // Appends category name to the report buffer and recursively visits children.
    public void VisitCategory(Category category)
    {
        string indent = GetIndent();
        _reportBuffer.AppendLine($"{indent}[CATEGORY] {category.Name}");
        _currentDepth++;
        foreach (var child in category.Children)
        {
            child.Accept(this);
        }
        _currentDepth--;
    }

    public string GetFullReport()
    {
        return _reportBuffer.ToString();
    }
}
