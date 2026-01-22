// A visitor that aggregates statistics about Tasks (total count and completed count).
public class StatisticsVisitor : IVisitor
{
    private int _taskCount;
    private int _doneCount;

    public StatisticsVisitor() { }

    // Visits a Task and updates the statistics counters.
    public void VisitTask(Task task)
    {
        _taskCount++;
        if (task.IsDone)
        {
            _doneCount++;
        }
    }

    public void VisitNote(Note note) { }

    // Visits a Category and recursively processes its children.
    public void VisitCategory(Category category)
    {
        foreach (var child in category.Children)
        {
            child.Accept(this);
        }
    }

    public string GetStatistics()
    {
        return $"Total Tasks: {_taskCount}, Completed: {_doneCount}";
    }
}
