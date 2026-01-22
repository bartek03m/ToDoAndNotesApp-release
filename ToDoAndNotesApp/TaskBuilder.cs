// Implements the Builder pattern to construct complex Task objects step-by-step.
public class TaskBuilder
{
    private Task _task;

    public TaskBuilder()
    {
        _task = new Task(null);
    }

    public TaskBuilder SetTitle(string title)
    {
        _task.Title = title;
        return this;
    }

    public TaskBuilder SetDescription(string desc)
    {
        _task.Description = desc;
        return this;
    }

    public TaskBuilder SetPriority(Priority priority)
    {
        _task.PriorityLevel = priority;
        return this;
    }

    public TaskBuilder SetDueDate(DateTime date)
    {
        _task.DueDate = date;
        return this;
    }

    public TaskBuilder AddTag(string tagName)
    {
        _task.Tags.Add(Tag.GetInstance(tagName, "Gray"));
        return this;
    }

    public Task Build()
    {
        return _task;
    }
}
